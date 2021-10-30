using System;

namespace BuisnessLogicLayer.DataTransferObjects
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentURI { get; set; }
        public DateTime CreationDate { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Price { get; set; }
        public int PurchaseCount { get; set; }
        public float Rating { get; set; }
        public AuthorDto Author { get; set; }
    }
}
