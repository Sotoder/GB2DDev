using System;
using System.Collections.Generic;
using System.Linq;
using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;
    [SerializeField] private float _carBaseSpeed;

    [SerializeField] private UnityAdsTools _ads;
    [SerializeField] private AppDataSourses _appDataSourses;
    [SerializeField] private PrefabsAssetReferences _prefabsAssetReferences;

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    private void Awake()
    {
        _analyticsTools = new UnityAnalyticTools();
        var profilePlayer = new ProfilePlayer(_carBaseSpeed, _ads, _analyticsTools);
        _mainController = new MainController(_placeForUi, profilePlayer, _appDataSourses, _prefabsAssetReferences);
        profilePlayer.CurrentState.Value = GameState.Start;
        var timerController = new TimerController();
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
