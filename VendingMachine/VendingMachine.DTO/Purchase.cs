using VendingMachine.DTO.Base;

namespace VendingMachine.DTO
{
    public class Purchase : BaseDTO
    {
        public int AccountID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public bool IsCheckOut { get; set; }
        public int Amount { get; set; }
        public virtual Accounts Account { get; set; }
        public virtual Products Product { get; set; }

    }
}
