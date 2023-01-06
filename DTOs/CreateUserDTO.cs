namespace Relief.DTOs
{
    public class CreateUserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}
