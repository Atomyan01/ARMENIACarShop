namespace ARMENIACarShop.Models
{
	public class SellerModel:PersonModel
	{
		public double AvarageScore { get; set; }
		public List<CarModel> Cars { get; set; } = new List<CarModel>();
	}
}
