using Tools;
namespace Model.Shop
{
    internal interface IShop
    {
        void Buy(string id);
        string GetCost(string productID);
        void RestorePurchase();
        IReadOnlySubscriptionAction<string> OnSuccessPurchase { get; }
        IReadOnlySubscriptionAction OnFailedPurchase { get; }
    }
}