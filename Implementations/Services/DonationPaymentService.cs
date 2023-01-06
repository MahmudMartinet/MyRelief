using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Repositories;
using Relief.Interfaces.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Relief.Implementations.Services
{
    public class DonationPaymentService : IDonationPaymentService
    {
        private readonly IDonationPaymentRepository _paymentRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IConfiguration _configuration;

        public DonationPaymentService(IDonationRepository donationRepository, IDonationPaymentRepository paymentRepository, IConfiguration configuration)
        {
            _donationRepository = donationRepository;
            _paymentRepository = paymentRepository;
            _configuration = configuration;
        }

        public async Task<PaystackResponse> MakePayment(DonationPaymentRequestModel model, int donationId)
        {
            var donation = await _donationRepository.GetDonation(donationId);
            if (donation == null)
            {
                return new PaystackResponse
                {
                    Message = "No donation attached",
                    Success = false
                };
            }
            if (model == null)
            {
                return new PaystackResponse
                {
                    Message = "Fields cannot be null",
                    Success = false
                };
            }

            //var pay = model.AmountPaid * apartment.Category.Rate;

            var generateRef = Guid.NewGuid().ToString().Substring(0, 10);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "https://api.paystack.co/transaction/initialize";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_test_da94d170eb3d32cfcc1369d248a752488b9ef1a7");
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                amount = model.Amount,
                email = donation.Donor.Email,
                referrenceNumber = generateRef

            }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var resString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonSerializer.Deserialize<PaystackResponse>(resString);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var payment = new DonationPayment
                {
                    Amount = model.Amount,
                    Donor = donation.Donor,
                    RequestDonation = donation.ReqeustDonation,
                    DonorId = donation.DonorId,
                    RequestId = donation.ReqeustDonationId,
                    Reference = generateRef,
                    DonationDate = DateTime.UtcNow,
                };

                var pay = await _paymentRepository.Register(payment);
                var donate = new Donation
                {
                    IsFund = true,
                    DonationPaymentId = pay.Id,
                    DonationPayment = pay,
                    Donor = donation.Donor,
                    DonorId = donation.DonorId,
                    ReqeustDonation = donation.ReqeustDonation,
                    ReqeustDonationId = donation.ReqeustDonationId,

                };

                await _donationRepository.Register(donate);
                return new PaystackResponse
                {
                    Message = "Donation made successfully",
                    Success = true,
                    Data = new PaystackData
                    {
                        Authorization_url = responseObj.Data.Authorization_url,
                        Reference = generateRef
                    }
                };
            }

            return new PaystackResponse
            {
                Message = "Donation not sent",
                Success = false
            };
        }

        public async Task<MakePayment> SendMoney(string reciept, decimal amount)
        {
            var getHttpClient = new HttpClient();
            getHttpClient.DefaultRequestHeaders.Accept.Clear();
            getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var baseUri = $"https://api.paystack.co/transfer";
            getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_test_da94d170eb3d32cfcc1369d248a752488b9ef1a7");
            var response = await getHttpClient.PostAsJsonAsync(baseUri, new
            {

                recipient = reciept,
                amount = amount * 100,
                currency = "NGN",
                source = "balance"
            });
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<MakePayment>(responseString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!responseObject.Success)
                {
                    return new MakePayment()
                    {
                        Success = false,
                        Message = responseObject.Message
                    };
                }
                return new MakePayment()
                {
                    Success = true,
                    Message = responseObject.Message,
                    Data = responseObject.Data
                };
            }
            return new MakePayment()
            {
                Success = false,
                Message = "Pls retry, payment is not successfull"
            };
        }

        public async Task<Receipt> GenerateReceipt(BankVerification verifyBank)
        {

            var getHttpClient = new HttpClient();
            getHttpClient.DefaultRequestHeaders.Accept.Clear();
            getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var baseUri = getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/transferrecipient");
            getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_test_da94d170eb3d32cfcc1369d248a752488b9ef1a7");
            var response = await getHttpClient.PostAsJsonAsync(baseUri, new
            {
                type = "nuban",
                name = verifyBank.Data.AccountName,
                account_number = verifyBank.Data.AccountNumber,
                bank_code = verifyBank.Data.BankCode,
                currency = "NGN",
            });
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Receipt>(responseString);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                if (!responseObject.Success)
                {
                    return new Receipt()
                    {
                        Success = false,
                        Message = responseObject.Message
                    };
                }
                return new Receipt()
                {
                    Success = true,
                    Message = "Recipient Generated",
                    Data = responseObject.Data
                };
            }

            return new Receipt()
            {
                Success = false,
                Message = responseObject.Message
            };
        }

        public async Task<BankVerification> VerifyAccountNumber(string acNumber, string bankCode, decimal amount)
        {
            var getHttpClient = new HttpClient();
            getHttpClient.DefaultRequestHeaders.Accept.Clear();
            getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var baseUri = getHttpClient.BaseAddress =
                new Uri($"https://api.paystack.co/bank/resolve?account_number={acNumber}&bank_code={bankCode}");

            getHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "sk_test_da94d170eb3d32cfcc1369d248a752488b9ef1a7");
            var response = await getHttpClient.GetAsync(baseUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<BankVerification>(responseString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!responseObject.Success)
                {
                    return new BankVerification()
                    {
                        Success = false,
                        Message = responseObject.Message
                    };
                }

                var splitName = responseObject.Data.AccountName;
                var splitNameArray = splitName.Split(' ');
                var generate = await GenerateReceipt(responseObject);
                if (!generate.Success)
                {
                    return new BankVerification()
                    {
                        Success = false,
                        Message = generate.Message
                    };
                }

                var makeTransfer = await SendMoney(generate.Data.RecipientCode, amount);
                if (!makeTransfer.Success)
                {
                    return new BankVerification()
                    {
                        Success = false,
                        Message = makeTransfer.Message
                    };
                }
                return new BankVerification()
                {
                    Success = true,
                    Message = makeTransfer.Message,
                    Data = new BankVerificationData()
                    {
                        Reason = generate.Data.Reason,
                        Reference = generate.Data.Reference,
                        RecipientCode = generate.Data.RecipientCode,
                        Amount = makeTransfer.Data.Amount,
                        Currency = makeTransfer.Data.Currency,
                        Success = makeTransfer.Data.Success,
                        TransferCode = makeTransfer.Data.TransferCode
                    }
                };
            }

            return new BankVerification()
            {
                Success= false,
                Message = "Cannot verify account number"
            };

        }

        public async Task<BaseResponse> VerifyPayment(string referrenceNumber)
        {
            var payment = await _paymentRepository.Get(x => x.Reference == referrenceNumber);
            if (payment == null)
            {
                return new BaseResponse
                {
                    Message = "Payment not found",
                    Success = false
                };
            }
            payment.IsVerified = true;
            var updatePayment = await _paymentRepository.Update(payment);
            return new BaseResponse
            {
                Success = true,
                Message = "Verification successfull"
            };
        }

    }
}
