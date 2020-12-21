using System;

namespace VendingMachine.DTO.Base
{
    public abstract class BaseDTO
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
