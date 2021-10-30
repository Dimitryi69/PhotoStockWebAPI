using System;

namespace PhotoStockWebAPI.Models
{
    public class TextViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Price { get; set; }
        public int Length { get; set; }
        public int PurchaseCount { get; set; }
        public float Rating { get; set; }
        public string AuthorName { get; set; }
        public string AuthorNickname { get; set; }
    }
}
