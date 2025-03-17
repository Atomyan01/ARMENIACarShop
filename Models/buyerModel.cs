namespace ARMENIACarShop.Models
{
	public class BuyerModel : PersonModel
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public int Age { get; set; }
		public List<OrderModel> Orders { get; set; } = new List<OrderModel>();
		public string? CreditCard { get; set; }
		public List<CarModel> WaitList { get; set; } = new List<CarModel>();


	}
}
