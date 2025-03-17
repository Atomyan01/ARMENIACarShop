namespace ARMENIACarShop.Models
{
	public class CarModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Model { get; set; }
		public int Year { get; set; }
		public string Type {  get; set; }
		public bool IsDamaged { get; set; }
		public bool IsUnderWater { get; set; }
		public int EngineCapacity { get; set; }
		public int NumberOfDoors { get; set; }
		public int CarMileage { get ; set; }
		public bool IsRunAndDrive { get; set; }
		public decimal Price {  get; set; }
		public bool IsElectric {  get; set; }
		public bool IsUsed { get; set; }
		public bool IsCredit { get; set; }
        public decimal? Percentage { get; set; }
    }
}
