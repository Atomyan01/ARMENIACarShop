namespace ARMENIACarShop.Models
{
	public class ReviewModel
	{
		public int Id { get; set; }
		public int Stars { get; set; }
		public SellerModel Seller { get; set; } = new SellerModel();
		public BuyerModel buyer { get; set; } = new BuyerModel();

	}
}
