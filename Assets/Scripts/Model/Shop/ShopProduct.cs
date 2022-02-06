using System;
using UnityEngine.Purchasing;

namespace Model.Shop
{
    [Serializable]
    public class ShopProduct
    {
        public int InternalID;
        public string ShopId;
        public ProductType CurrentProductType;
    }
}