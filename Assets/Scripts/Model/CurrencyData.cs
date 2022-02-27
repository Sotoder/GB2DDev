using Tools;

public class CurrencyData
{
    public SubscriptionProperty<int> Wood;
    public SubscriptionProperty<int> Diamond;

    public CurrencyData()
    {
        Wood = new SubscriptionProperty<int>();
        Diamond = new SubscriptionProperty<int>();
    }
}