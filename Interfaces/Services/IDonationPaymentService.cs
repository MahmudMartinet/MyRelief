using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface IDonationPaymentService
    {
        Task<PaystackResponse> MakePayment(DonationPaymentRequestModel model, int donationId);
        Task<MakePayment> SendMoney(string reciept, decimal amount);
        Task<Receipt> GenerateReceipt(BankVerification verifyBank);
        Task<BankVerification> VerifyAccountNumber(string acNumber, string bankCode, decimal amount);
    }
}
