using UnityEngine;

public class FightController: BaseController
{
    private FightWindowView _fightWindowView;
    private readonly ProfilePlayer _profilePlayer;

    private IEnemy _enemy;
    private UiListener _uiListener;

    public FightController(ProfilePlayer profilePlayer, Transform uiPosition)
    {
        _profilePlayer = profilePlayer;
        InitView(uiPosition);
        CreatePaticipants();
        AddController(this);
    }

    private void InitView(Transform uiPosition)
    {
        _fightWindowView = ResourceLoader.LoadAndInstantiateView<FightWindowView>(new ResourcePath {PathResource = "Prefabs/Fight Window View"}, uiPosition);
        AddGameObjects(_fightWindowView.gameObject);

        _fightWindowView.Init(ChangeDataWindow, Fight, Run, ChangeCrimeLevel, ChangeFightStateOnGun, ChangeFightStateOnKnife, Exit);
        _uiListener = new UiListener(_fightWindowView.CountPowerText, _fightWindowView.CountMoneyText, 
                                     _fightWindowView.CountHealthText, _fightWindowView.CrimeLevelText,
                                     _fightWindowView.RunButton);
        _profilePlayer.FightData.Money.Attach(_uiListener);
        _profilePlayer.FightData.Health.Attach(_uiListener);
        _profilePlayer.FightData.Power.Attach(_uiListener);
        _profilePlayer.FightData.CrimeLevel.Attach(_uiListener);
    }

    private void Exit()
    {
        _profilePlayer.CurrentState.Value = Profile.GameState.Game;
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                if (countChangeData < 0 && _profilePlayer.FightData.Money.CountMoney > 0)
                {
                    _profilePlayer.FightData.Money.CountMoney += countChangeData;
                } else if (countChangeData > 0)
                {
                    _profilePlayer.FightData.Money.CountMoney += countChangeData;
                }
                break;

            case DataType.Health:
                if (countChangeData > 0 && _profilePlayer.FightData.Money.CountMoney > 0)
                {
                    _profilePlayer.FightData.Health.CountHealth += countChangeData;
                    _profilePlayer.FightData.Money.CountMoney -= countChangeData;
                }
                else if (countChangeData < 0 && _profilePlayer.FightData.Health.CountHealth > 0)
                {
                    _profilePlayer.FightData.Health.CountHealth += countChangeData;
                }
                break;

            case DataType.Power:
                if (countChangeData > 0 && _profilePlayer.FightData.Money.CountMoney > 0)
                {
                    _profilePlayer.FightData.Power.CountPower += countChangeData;
                    _profilePlayer.FightData.Money.CountMoney -= countChangeData;
                } 
                else if (countChangeData < 0 && _profilePlayer.FightData.Power.CountPower > 0)
                {
                    _profilePlayer.FightData.Power.CountPower += countChangeData;
                }
                break;
            case DataType.OverallMoney:
                _profilePlayer.FightData.OverallMoney.OverallMoneyCount++;
                break;
            case DataType.CrimeLevel:
                _profilePlayer.FightData.CrimeLevel.CrimeLevel += countChangeData;
                break;
        }

        _fightWindowView.CountPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
    }

    private void ChangeDataWindow()
    {
        _fightWindowView.CountPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
    }

    private void ChangeFightStateOnKnife(bool isKnife)
    {
        if (isKnife)
        {
            _fightWindowView.GunToggle.isOn = false;
            _enemy.ChangeFightState(FightStates.Knife);
            ChangeDataWindow();
        }
        else
        {
            if (!_fightWindowView.GunToggle.isOn) _fightWindowView.KnifeToogle.isOn = true;
        }
    }

    private void ChangeFightStateOnGun(bool isGun)
    {
        if (isGun)
        {
            _fightWindowView.KnifeToogle.isOn = false;
            _enemy.ChangeFightState(FightStates.Gun);
            ChangeDataWindow();
        }
        else
        {
            if (!_fightWindowView.KnifeToogle.isOn) _fightWindowView.GunToggle.isOn = true;
        }
    }

    private void ChangeCrimeLevel(bool isAddCount, bool isBuy)
    {
        if (isAddCount)
        {
            if (_profilePlayer.FightData.CrimeLevel.CrimeLevel < 5)
                ChangeDataWindow(1, DataType.CrimeLevel);
        }
        else
        {
            if (_profilePlayer.FightData.CrimeLevel.CrimeLevel > 0)
            {
                if (isBuy)
                {
                    BuyCrimeLevel();
                }
                else ChangeDataWindow(-1, DataType.CrimeLevel);
            }
        }
    }

    private void BuyCrimeLevel()
    {
        var cost = 0;
        switch (_profilePlayer.FightData.CrimeLevel.CrimeLevel)
        {
            case 1:
            case 2:
                cost = 5;
                break;
            case 3:
            case 4:
            case 5:
                cost = 10;
                break;
        }

        if (_profilePlayer.FightData.Money.CountMoney < cost)
        {
            Debug.Log($"Not enought money, curent cost: {cost}");
        }
        else
        {
            ChangeDataWindow(-1, DataType.CrimeLevel);
            ChangeDataWindow(-cost, DataType.Money);
        }
    }

    private void CreatePaticipants()
    {
        _enemy = new Enemy("Flappy");

        _profilePlayer.FightData.Health.Attach(_enemy);
        _profilePlayer.FightData.Power.Attach(_enemy);
        _profilePlayer.FightData.OverallMoney.Attach(_enemy);
        _profilePlayer.FightData.CrimeLevel.Attach(_enemy);
    }

    private void Run()
    {
        ChangeCrimeLevel(true, false);
    }

    private void Fight()
    {
        if (_profilePlayer.FightData.Power.CountPower >= _enemy.Power)
        {
            Debug.Log("Win");
            _profilePlayer.FightData.WinsCount++;
            if (_profilePlayer.FightData.WinsCount % FightData.WIN_COUN_TO_UP_CRIME == 0)
            {
                ChangeCrimeLevel(true, false);
            }
        }
        else
        {
            Debug.Log("Lose");
            ChangeCrimeLevel(false, false);
        }
    }

    private new void OnDispose()
    {
        _profilePlayer.FightData.Health.Detach(_enemy);
        _profilePlayer.FightData.Power.Detach(_enemy);
        _profilePlayer.FightData.OverallMoney.Detach(_enemy);
        _profilePlayer.FightData.CrimeLevel.Detach(_enemy);

        _profilePlayer.FightData.Money.Detach(_uiListener);
        _profilePlayer.FightData.Health.Detach(_uiListener);
        _profilePlayer.FightData.Power.Detach(_uiListener);
        _profilePlayer.FightData.CrimeLevel.Detach(_uiListener);
    }
}
