using System;

namespace DataAccessLayer.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
