using VendingMachine.DTO.Base;

namespace VendingMachine.DTO
{
    public class Products : BaseDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
