using UnityEngine.AddressableAssets;

public interface IGameReferences
{
    AssetReferenceGameObject AbilitiesViewReference { get; }
    AssetReferenceGameObject BattleViewReference { get; }
    AssetReferenceGameObject CarViewReference { get; }
    AssetReferenceGameObject PathViewReference { get; }
}