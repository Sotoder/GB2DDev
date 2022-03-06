public class FightData
{
    private IMoneyData _money;
    private IHealthData _health;
    private IPowerData _power;
    private IOverallMoneyData _overallMoney;
    private ICrimeLevelData _crimeLevel;

    public int AllCountMoneyPlayer;
    public int AllCountHealthPlayer;
    public int AllCountPowerPlayer;
    public int AllCrimeLevel;

    public int WinsCount;

    public const int WIN_COUN_TO_UP_CRIME = 5;

    public IMoneyData Money { get => _money; }
    public IHealthData Health { get => _health; }
    public IPowerData Power { get => _power; }
    public IOverallMoneyData OverallMoney { get => _overallMoney; }
    public ICrimeLevelData CrimeLevel { get => _crimeLevel; }

    public FightData()
    {
        _money = new Money(nameof(Money));
        _health = new Health(nameof(Health));
        _power = new Power(nameof(Power));
        _overallMoney = new OverallMoney(nameof(OverallMoney));
        _crimeLevel = new CrimeLevel(nameof(CrimeLevel));
    }


}