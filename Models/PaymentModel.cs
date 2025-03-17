namespace ARMENIACarShop.Models
{
	public class PaymentModel
	{

		public int Id { get; set; }
		public string BuyerEmail { get; set; }
		public string SellerEmail { get; set; }
        public CarModel Car { get; set; }
		public string Method { get; set; }
		public bool IsCredit { get; set; }
		
    }
}
