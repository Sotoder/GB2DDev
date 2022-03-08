using System;

namespace UnityEngine.AddressableAssets
{
    [Serializable]
    public class AssetReferenceItemSourceDataSource: AssetReferenceT<ItemsSource>
    {
        public AssetReferenceItemSourceDataSource(string guid) : base(guid) { }
        public override bool ValidateAsset(Object obj)
        {
            var type = obj.GetType();
            return typeof(ItemsSource).IsAssignableFrom(type);
        }
    }
}