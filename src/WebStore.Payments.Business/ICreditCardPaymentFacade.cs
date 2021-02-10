namespace WebStore.Payments.Business
{
    public interface ICreditCardPaymentFacade
    {
        Transaction CheckOut(Order order, Payment payment);
    }
}
