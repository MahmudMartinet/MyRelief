namespace Relief.Entities
{
    public class CreateUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string RoleName { get; set; }
    }
}
