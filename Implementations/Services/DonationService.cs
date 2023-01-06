using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Services;
using Relief.Email;
using Relief.Interfaces.Repositories;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace Relief.Implementations.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly IRequestDonationRepository _requestRepository;
        private readonly IConfiguration _configuration;
        private readonly IDonationPaymentRepository _paymentRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmailSender _email;
        public DonationService(IDonationRepository donationRepository, IDonorRepository donorRepository, IRequestDonationRepository requestRepository, IConfiguration configuration, IDonationPaymentRepository paymentRepository, IAppointmentRepository appointmentRepository, IEmailSender email)
        {
            _donationRepository = donationRepository;
            _donorRepository = donorRepository;
            _requestRepository = requestRepository;
            _configuration = configuration;
            _paymentRepository = paymentRepository;
            _appointmentRepository = appointmentRepository;
            _email = email;
        }

        public async Task<DonationsResponseModel> GetAll()
        {
            var donations = await _donationRepository.GetAll();
            if (donations == null)
            {
                return new DonationsResponseModel
                {
                    Message = "No donation found",
                    Success = false
                };
            }
            var donationDto = donations.Where(x => x.IsDeleted == false).Select(x => new DonationDTO
            {
                RequestDetail = x.ReqeustDonation.Details,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                IsApproved = x.IsApproved,
                DonorId = x.DonorId,
                RequestDonationId = x.ReqeustDonationId,
                Id = x.Id
            }).ToList().ToHashSet();
            if (donationDto == null)
            {
                return new DonationsResponseModel
                {
                    Message = "No available donation",
                    Success = false
                };
            }
            return new DonationsResponseModel
            {
                Data = donationDto,
                Success = true,
                Message = "List of donations"
            };
        }

        public async Task<DonationsResponseModel> GetByDonorId(int id)
        {
            var donations = await _donationRepository.GetByDonorId(id);
            if(donations == null)
            {
                return new DonationsResponseModel
                {
                    Message = "No donation found",
                    Success = false
                };
            }
            var donationsByDonor = donations.Where(x => x.IsDeleted == false).Select(x => new DonationDTO
            {
                RequestDetail = x.ReqeustDonation.Details,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                IsApproved = x.IsApproved,
                DonorId = x.DonorId,
                RequestDonationId = x.ReqeustDonationId,
                Id = x.Id
            }).ToList().ToHashSet();
            if(donationsByDonor.Count == 0)
            {
                return new DonationsResponseModel
                {
                    Message = "Donation list is empty",
                    Success = false
                };
            }
            return new DonationsResponseModel
            {
                Success = true,
                Data = donationsByDonor,
                Message = "List of Donations"
            };
        }

        public async Task<DonationsResponseModel> GetByRequestId(int id)
        {
            var donations = await _donationRepository.GetByRequestId(id);
            if (donations == null)
            {
                return new DonationsResponseModel
                {
                    Message = "Donation list is empty",
                    Success = false
                };
            }
            var dto = donations.Where(x => x.IsDeleted == false ).Select(x => new DonationDTO
            {
                RequestDetail = x.ReqeustDonation.Details,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                IsApproved = x.IsApproved,
                DonorId = x.DonorId,
                RequestDonationId = x.ReqeustDonationId,
                Id = x.Id
            }).ToList().ToHashSet();

            if (dto.Count == 0)
            {
                return new DonationsResponseModel
                {
                    Message = "No donation made yet",
                    Success = false
                };
            }
            return new DonationsResponseModel
            {
                Message = "List of Donations",
                Success = true,
                Data = dto
            };
        }

        public async Task<DonationResponseModel> GetDonationById(int id)
        {
            var donation = await _donationRepository.GetDonation(id);
            if (donation == null || donation.IsDeleted == true)
            {
                return new DonationResponseModel
                {
                    Message = "Donation not found",
                    Success = false
                };
            }
            var dto = new DonationDTO
            {
                DonorName = $"{donation.Donor.FirstName} {donation.Donor.LastName}",
                DonorId = donation.DonorId,
                RequestDetail = donation.ReqeustDonation.Details,
                RequestDonationId = donation.ReqeustDonationId,
                Id = donation.Id,
                IsApproved = donation.IsApproved,
            };
            return new DonationResponseModel
            {
                Message = "",
                Data = dto,
                Success = true
            };
        }

       

        public async Task<BaseResponse> MakeDonation(CreateDonationRequestModel model, int donorId, int requestId)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Information cannot be null",
                    Success = false
                };
            }

            var donor = await _donorRepository.Get(x => x.Id == donorId);

            if(donor.IsBan == true)
            {
                return new BaseResponse
                {
                    Message = "Your account is currently under restriction...",
                    Success = false
                };
            }

            var request = await _requestRepository.GetRequest(requestId);

            if( model.Amount != null && model.Amount != 0 && model.Amount > 0)
            {
                if(request.Group.Name.ToLower() == "both" || request.Group.Name.ToLower() == "money only")
                {
                    var generateRef = Guid.NewGuid().ToString().Substring(0, 10);
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var url = "https://api.paystack.co/transaction/initialize";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["PayStack:ApiKey"]);
                    var content = new StringContent(JsonSerializer.Serialize(new
                    {
                        amount = model.Amount,
                        email = donor.Email,
                        referrenceNumber = generateRef

                    }), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    var resString = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonSerializer.Deserialize<PaystackResponseModel>(resString);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        

                        var donate = new DonationPayment
                        {
                            Amount = model.Amount,
                            DonationDate = DateTime.UtcNow,
                            Reference = generateRef,
                            DonorId = donorId,
                            Donor = donor,
                            RequestDonation = request,
                            RequestId = requestId,
                        };

                        var pay = await _paymentRepository.Register(donate);

                        request.AmountGathered += model.Amount;
                        request.QuantityGathered += (int)(model.Amount / request.PricePerOne);
                        request.Progress = (decimal)(model.Amount / request.RequestInMoney * 100);
                        await _requestRepository.Update(request);

                        var donatio = new Donation
                        {
                            IsApproved = true,
                            IsFund = true,
                            DonorId = donorId,
                            ReqeustDonationId = requestId,
                            Donor = await _donorRepository.GetDonor(donorId),
                            ReqeustDonation = request,
                            DonationPaymentId = pay.Id,
                        };

                        await _donationRepository.Register(donatio);
                        var mail = new EmailRequestModel
                        {
                            ReceiverEmail = donor.Email,
                            ReceiverName = $"{donor.FirstName} {donor.LastName}",
                            Message = $"Dear {donor.FirstName}, your donation of {model.Amount} Naira   has been recieved successfully.\nRelif Administration wish to appreciate    your kind gesture on behalf of the beneficiaries.\nThanks",
                            Subject = "Relief-CMS Donation Aknowledgement",
                        };
                        await _email.SendEmail(mail);
                        return new BaseResponse
                        {
                            Success = true,
                            Message = "Donation made successfully"
                        };

                    }

                }
            }
            else if(model.Venue != null && model.Venue != "")
            {
                
               

                var appoint = new DonationAppointment
                {
                    Venue = model.Venue,
                    Time = model.Time,
                    DonorId = donor.Id,
                    NgoId = request.NgoId,
                    RequestId = requestId,
                    Donor = donor,
                    NGO = request.NGO,
                    RequestDonation = request,
                    Quantity = model.Quantity,
                };
                await _appointmentRepository.Register(appoint);
                var mail = new EmailRequestModel
                {
                    ReceiverEmail = donor.Email,
                    ReceiverName = $"{donor.FirstName} {donor.LastName}",
                    Message = $"Dear {donor.FirstName}, you have set an appointment with {request.NGO.Name} on {appoint.Time} at {appoint.Venue}.\nWe would be grateful if this appointment is accomplished.\nThanks",
                    Subject = "Relief-CMS Appointment Notification",
                };
                await _email.SendEmail(mail);

                return new BaseResponse
                {
                    Success = true,
                    Message = "Appointment has been created, your donation will only be recorded when this appointment has been marked as accomplished"
                };
            }
            
            return new BaseResponse
            {
                Success = false,
                Message = "Donation not sent!"
            };
        }

        public async Task<BaseResponse> UpdateDonation(UpdateDonationRequestModel model, int id)
        {
            var donation = await _donationRepository.GetDonation(id);
            if (donation == null || donation.IsDeleted == true)
            {
                return new BaseResponse
                {
                    Message = "Donation not found",
                    Success = false
                };
            }
           
            donation.IsApproved = model.IsApproved;
            donation.IsFund = model.IsFund;

            await _donationRepository.Update(donation);
            return new BaseResponse
            {
                Message = "Donation updated successfully",
                Success = true
            };
        }
    }
}
