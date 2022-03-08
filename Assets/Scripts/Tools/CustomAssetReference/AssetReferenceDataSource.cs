using System;

namespace UnityEngine.AddressableAssets
{

    [Serializable]

    public class AssetReferenceAbilityItemsSource : AssetReferenceT<AbilitiesItemsSource>
    {
        public AssetReferenceAbilityItemsSource(string guid) : base(guid) { }
        public override bool ValidateAsset(Object obj)
        {
            var type = obj.GetType();
            return typeof(AbilitiesItemsSource).IsAssignableFrom(type);
        }
    }
}
