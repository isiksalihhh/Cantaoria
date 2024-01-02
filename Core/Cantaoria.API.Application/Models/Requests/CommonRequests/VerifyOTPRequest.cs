namespace Cantaoria.Application.Models.Requests.CommonRequests
{
    public class VerifyOTPRequest
    {
        public string PhoneNumber { get; set; }
        public string OTP { get; set; }
    }
}
