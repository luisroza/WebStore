namespace WebStore.Payments.AntiCorruption
{
    public interface IPayPalGateway
    {
        string GetPayPalServiceKey(string apiKey, string encriptionKey);
        string GetCardHashKey(string serviceKey, string creditCard);
        bool CommitTransaction(string cardHashKey, string orderId, decimal amount);
    }
}
