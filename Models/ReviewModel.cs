namespace ARMENIACarShop.Models
{
	public class ReviewModel
	{
		public int Id { get; set; }
		public int Stars { get; set; }
		public string Content { get; set; }
		public CarModel Car { get; set; } = new CarModel();
		public BuyerModel Buyer { get; set; } = new BuyerModel();
        public string Description { get; set; }
    }
}
