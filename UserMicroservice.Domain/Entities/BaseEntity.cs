namespace UserMicroservice.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? DataAlteracao { get; set; } = DateTime.Now;
    }
}
