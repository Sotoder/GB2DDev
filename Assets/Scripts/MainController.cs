using Profile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
        IReadOnlyList<UpgradeItemConfig> upgradeItems,
        IReadOnlyList<AbilityItemConfig> abilityItems)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        
        _upgradeItems = upgradeItems;
        _abilityItems = abilityItems;

        var itemsSource =
            ResourceLoader.LoadDataSource<ItemConfig>(new ResourcePath()
                { PathResource = "Data/ItemsSource" });
        _itemsConfig = itemsSource.Content.ToList();

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);

        _inventoryController = new InventoryController(_itemsConfig, _upgradeItems, _placeForUi);
        AddController(_inventoryController);
    }

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private InventoryController _inventoryController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _inventoryController.SetOnGameSceneFlag(false);
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _itemsConfig, _upgradeItems, _inventoryController);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _inventoryController.SetInventoryViewPosition(_placeForUi);
                _inventoryController.SetOnGameSceneFlag(true);
                _gameController = new GameController(_profilePlayer, _abilityItems, _inventoryController, _placeForUi);
                _mainMenuController?.Dispose();
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }
}
