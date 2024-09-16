using Profile;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ResourcePath _trailViewPath = new ResourcePath { PathResource = "Prefabs/trailView" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private readonly TrailView _trailView;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _trailView = CreateTrailView(placeForUi);
        _view.Init(StartGame);
        _trailView.Init();
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private TrailView CreateTrailView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_trailViewPath), placeForUi, false);
        AddGameObjects(objectView);
        objectView.transform.SetParent(_view.transform);

        return objectView.GetComponent<TrailView>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
}

