namespace API.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public String Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}