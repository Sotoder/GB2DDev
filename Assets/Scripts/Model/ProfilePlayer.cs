using Model.Analytic;
using Profile;
using System.Collections.Generic;
using Tools;
using Tools.Ads;

public class ProfilePlayer
{
    private CurrencyData _currency;
    private List<RewardData> _rewardsData;
    private FightData _fightData;

    public IAdsShower AdsShower { get; }

    public IAnalyticTools AnalyticTools { get; }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public Car CurrentCar { get; }
    public CurrencyData Currency { get => _currency; }
    public List<RewardData> RewardsData { get => _rewardsData; }
    public FightData FightData { get => _fightData; }

    public ProfilePlayer(float speedCar, IAdsShower adsShower, IAnalyticTools analyticTools)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        _currency = new CurrencyData();
        _rewardsData = new List<RewardData>();
        _fightData = new FightData();
        AdsShower = adsShower;
        AnalyticTools = analyticTools;
    }
}

