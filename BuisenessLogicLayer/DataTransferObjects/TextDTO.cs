using System;

namespace BuisnessLogicLayer.DataTransferObjects
{
    public class TextDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Price { get; set; }
        public int Length { get; set; }
        public int PurchaseCount { get; set; }
        public float Rating { get; set; }
        public AuthorDto Author { get; set; }
    }
}
