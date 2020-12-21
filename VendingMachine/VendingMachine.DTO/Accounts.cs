using VendingMachine.DTO.Base;

namespace VendingMachine.DTO
{
    public class Accounts : BaseDTO
    {
        public int GuestID { get; set; }
        public double Balance { get; set; }
        public virtual Guest Guest { get; set; }
       
    }
}
