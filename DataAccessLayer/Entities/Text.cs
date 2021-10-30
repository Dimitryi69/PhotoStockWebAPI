using System;

namespace DataAccessLayer.Entities
{
    public class Text
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Price { get; set; }
        public int Length { get; set; }
        public int PurchaseCount { get; set; }
        public float Rating { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
