public class OnlineCart
{
    public void CheckOut(PaymentType paymentType)
    {
        switch (paymentType)
        {
            case PaymentType.CreditCard:
                ProcessCreditCardPayment();
                break;
            case PaymentType.Paypal:
                ProcessPaypalPayment();
                break;
            case PaymentType.GoogleCheckout:
                ProcessGooglePayment();
                break;
            case PaymentType.AmazonPayments:
                ProcessAmazonPayment();
                break;
        }
    }
    private void ProcessCreditCardPayment()
    {
        Console.WriteLine("Credit card payment chosen");
    }
    private void ProcessPaypalPayment()
    {
        Console.WriteLine("Paypal payment chosen");
    }
    private void ProcessGooglePayment()
    {
        Console.WriteLine("Google payment chosen");
    }
    private void ProcessAmazonPayment()
    {
        Console.WriteLine("Amazon payment chosen");
    }
}

