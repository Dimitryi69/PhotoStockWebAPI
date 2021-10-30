using System;

namespace BuisnessLogicLayer.DataTransferObjects
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
