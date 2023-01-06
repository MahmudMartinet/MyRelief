namespace Relief.Entities
{
    public class RegistrationPayment
    {
        public int Id { get; set; }
        public int NgoId { get; set; }
        public NGO NGO { get; set; }
        public DateTime Expiry { get; set; }
    }
}
