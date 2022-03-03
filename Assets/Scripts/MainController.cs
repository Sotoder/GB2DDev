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

        _inventoryModel = new InventoryModel();
    }

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private InventoryModel _inventoryModel;
    private FightController _fightController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;
    private RewardMenuController _rewardMenuController;

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
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _itemsConfig, _upgradeItems, _inventoryModel);
                _gameController?.Dispose();
                _rewardMenuController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _abilityItems, _itemsConfig, _upgradeItems, _inventoryModel, _placeForUi);
                _mainMenuController?.Dispose();
                _fightController?.Dispose();
                break;
            case GameState.Rewards:
                _rewardMenuController = new RewardMenuController(_profilePlayer, _placeForUi);
                _mainMenuController?.Dispose();
                break;
            case GameState.Fight:
                _gameController?.Dispose();
                _fightController = new FightController(_profilePlayer, _placeForUi);
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _rewardMenuController?.Dispose();
        _fightController?.Dispose();
    }
}
