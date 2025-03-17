namespace ARMENIACarShop.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        // Վճարման մոդել, եթե այն վերաբերում է պատվերին
        public PaymentModel Payment { get; set; }

        // Պատվերի կարգավիճակը (status)
        public string Status { get; set; }

        // Օգտագործողի ID (միավորված User-ով)
        public string UserId { get; set; }  // Փոխել `User`-ը `UserId`-ով

        // Մեքենաների ցանկը, որոնք պատվիրել են
        public List<CarModel> Cars { get; set; }

        // Պատվերի ստեղծման ամսաթիվը
        public DateTime Date { get; set; }

        // Ընդհանուր վճարելիք գումար
        public decimal TotalPrice { get; set; }

        // Ընդհանուր քանի մեքենա է պատվիրել
        public int TotalCars { get; set; }
    }
}
