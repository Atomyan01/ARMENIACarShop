namespace ARMENIACarShop.Models
{
	public class SellerModel:PersonModel
	{
		public int AvarageScore { get; set; } = 0;

        public List<CarModel> Cars { get; set; } = new List<CarModel>();
	}
}
