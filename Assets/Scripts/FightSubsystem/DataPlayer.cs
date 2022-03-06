using System.Collections.Generic;

public class DataPlayer
{
    private string _titleData;

    private int _countMoney;
    private int _overallCountMoney;
    private int _countHealth;
    private int _countPower;
    private int _crimeLevel;

    private List<IUpdatable> _subscribers = new List<IUpdatable>();

    public DataPlayer(string titleData)
    {
        _titleData = titleData;
    }

    public string TitleData => _titleData;

    public int CountMoney 
    { 
        get => _countMoney;
        set
        {
            if (_countMoney != value)
            {
                _countMoney = value;
                Notifier(DataType.Money);
            }
        }
    }

    public int CountHealth
    {
        get => _countHealth;
        set
        {
            if (_countHealth != value)
            {
                _countHealth = value;
                Notifier(DataType.Health);
            }
        }
    }

    public int CountPower
    {
        get => _countPower;
        set
        {
            if (_countPower != value)
            {
                _countPower = value;
                Notifier(DataType.Power);
            }
        }
    }

    public int OverallMoneyCount
    {
        get => _overallCountMoney;
        set
        {
            if(value > 0 && _overallCountMoney != value)
            {
                _overallCountMoney = value;
                Notifier(DataType.OverallMoney);
            }
        }
    }

    public int CrimeLevel
    {
        get => _crimeLevel;
        set
        {
            if (_crimeLevel != value)
            {
                _crimeLevel = value;
                Notifier(DataType.CrimeLevel);
            }
        }
    }

    public void Attach(IUpdatable enemy)
    {
        _subscribers.Add(enemy);
    }

    public void Detach(IUpdatable enemy)
    {
        _subscribers.Remove(enemy);
    }

    private void Notifier(DataType dataType)
    {
        foreach(var enemy in _subscribers)
            enemy.Update(this, dataType);
    }
}

public class Money : DataPlayer, IMoneyData
{
    public Money(string titleData) : base(titleData)
    {
    }
}

public class OverallMoney : DataPlayer, IOverallMoneyData
{
    public OverallMoney(string titleData) : base(titleData)
    {
    }
}

public class Health : DataPlayer, IHealthData
{
    public Health(string titleData) : base(titleData)
    {
    }
}

public class Power : DataPlayer, IPowerData
{
    public Power(string titleData) : base(titleData)
    {
    }
}

public class CrimeLevel : DataPlayer, ICrimeLevelData
{
    public CrimeLevel(string titleData) : base(titleData)
    {
    }
}
