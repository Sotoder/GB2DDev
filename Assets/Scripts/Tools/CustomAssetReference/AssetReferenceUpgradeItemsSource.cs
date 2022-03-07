using System;

namespace UnityEngine.AddressableAssets
{

    [Serializable]
    public class AssetReferenceUpgradeItemsSource : AssetReferenceT<UpgradeItemsSource>
    {
        public AssetReferenceUpgradeItemsSource(string guid) : base(guid) { }
        public override bool ValidateAsset(Object obj)
        {
            var type = obj.GetType();
            return typeof(UpgradeItemsSource).IsAssignableFrom(type);
        }

    }
}