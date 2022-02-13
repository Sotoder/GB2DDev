using System.Collections.Generic;
using Profile;
using UnityEngine;
using UnityEngine.Advertisements;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private readonly ShedController _shedController;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, List<ItemConfig> itemsConfig, 
                              IReadOnlyList<UpgradeItemConfig> upgradeItems, InventoryController inventoryController)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _shedController = new ShedController(upgradeItems, itemsConfig, _profilePlayer.CurrentCar, _view.transform, inventoryController);
        AddGameObjects(_view.gameObject);
        _view.Init(StartGame, _shedController.Enter);
    }
    
    private MainMenuView LoadView(Transform placeForUi)
    {
        return ResourceLoader.LoadAndInstantiateView<MainMenuView>(_viewPath, placeForUi);
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;

        _profilePlayer.AnalyticTools.SendMessage("start_game",
            new Dictionary<string, object>() { {"time", Time.realtimeSinceStartup }
            });
    }
}

