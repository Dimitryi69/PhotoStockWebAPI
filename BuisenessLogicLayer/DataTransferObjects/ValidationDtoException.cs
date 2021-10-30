using System;

namespace BuisnessLogicLayer.DataTransferObjects
{
    public class ValidationDtoException : Exception
    {
        public string Property { get; protected set; }
        public ValidationDtoException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
