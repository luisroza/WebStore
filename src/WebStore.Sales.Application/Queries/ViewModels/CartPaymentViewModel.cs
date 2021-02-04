namespace WebStore.Sales.Application.Queries.ViewModels
{
    public class CartPaymentViewModel
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardVerificationCode { get; set; }
    }
}
