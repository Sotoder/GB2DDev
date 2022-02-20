using System.Collections.Generic;
using System.Linq;
using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;
    [SerializeField] private float _carBaseSpeed;

    [SerializeField] private UnityAdsTools _ads;
    [SerializeField] private UpgradeItemConfigDataSource _upgradeSource;
    [SerializeField] private List<AbilityItemConfig> _abilityItems;

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    private void Awake()
    {
        _analyticsTools = new UnityAnalyticTools();
        var profilePlayer = new ProfilePlayer(_carBaseSpeed, _ads, _analyticsTools);
        _mainController = new MainController(_placeForUi, profilePlayer, _upgradeSource.ItemConfigs.ToList(), _abilityItems.AsReadOnly());
        profilePlayer.CurrentState.Value = GameState.Start;
        var timerController = new TimerController();
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
