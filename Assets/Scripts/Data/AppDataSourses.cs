using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "DataSourses", menuName = "AppDataSoursesConfig")]
public class AppDataSourses : ScriptableObject
{
    [SerializeField] private AssetReferenceUpgradeItemsSource _loadUpgradeSource;
    [SerializeField] private AssetReferenceItemSourceDataSource _loadItemSource;
    [SerializeField] private AssetReferenceAbilityItemsSource _loadAbilitySource;

    public AssetReferenceUpgradeItemsSource LoadUpgradeSource { get => _loadUpgradeSource; }
    public AssetReferenceItemSourceDataSource LoadItemSource { get => _loadItemSource; }
    public AssetReferenceAbilityItemsSource LoadAbilitySource { get => _loadAbilitySource; }
}