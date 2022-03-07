using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "PrefabsCatalog", menuName = "PrefabsCatalog")]
public class PrefabsAssetReferences : ScriptableObject, IMainMenuReferences, IGameReferences
{
    [SerializeField] private AssetReferenceGameObject _mainMenuReference;
    [SerializeField] private AssetReferenceGameObject _abilitiesViewReference;
    [SerializeField] private AssetReferenceGameObject _battleViewReference;
    [SerializeField] private AssetReferenceGameObject _carViewReference;
    [SerializeField] private AssetReferenceGameObject _pathViewReference;
    [SerializeField] private AssetReferenceGameObject _inventoryViewReference;

    public AssetReferenceGameObject MainMenuReference { get => _mainMenuReference; }
    public AssetReferenceGameObject AbilitiesViewReference { get => _abilitiesViewReference; }
    public AssetReferenceGameObject BattleViewReference { get => _battleViewReference; }
    public AssetReferenceGameObject CarViewReference { get => _carViewReference; }
    public AssetReferenceGameObject PathViewReference { get => _pathViewReference; }
    public AssetReferenceGameObject InventoryViewReference { get => _inventoryViewReference; }
}