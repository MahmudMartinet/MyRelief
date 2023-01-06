namespace Relief.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<NgoDTO> NGOs { get; set; }
    }
}
