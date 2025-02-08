namespace ARMENIACarShop.Models
{
	public class CarModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Model { get; set; }
		public int Year { get; set; }
		public string Type {  get; set; }
		public bool Damaged { get; set; }
		public bool UnderWater { get; set; }
		public int EngineCapacity { get; set; }
		public int NumberOfDoors { get; set; }
		public int CarMileage { get ; set; }
		public bool RunAndDrive { get; set; }
		public decimal Price {  get; set; }
		public bool Electric {  get; set; }
	}
}
