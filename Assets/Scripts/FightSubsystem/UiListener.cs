using TMPro;
using UnityEngine.UI;

public class UiListener: IUpdatable
{
    private TMP_Text _countMoneyText;
    private TMP_Text _countHealthText;
    private TMP_Text _countPowerText;
    private TMP_Text _crimeLevelText;
    private Button _runButton;

    public UiListener(TMP_Text countPower, TMP_Text countMoney, TMP_Text countHealth, TMP_Text crimeLevel, Button runButton)
    {
        _countPowerText = countPower;
        _countMoneyText = countMoney;
        _countHealthText = countHealth;
        _crimeLevelText = crimeLevel;
        _runButton = runButton;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _countMoneyText.text = $"Player Money: {dataPlayer.CountMoney}";
                break;

            case DataType.Health:
                _countHealthText.text = $"Player Health: {dataPlayer.CountHealth}";
                break;

            case DataType.Power:
                _countPowerText.text = $"Player Power: {dataPlayer.CountPower}";
                break;
            case DataType.CrimeLevel:
                _crimeLevelText.text = $"Crime Level: {dataPlayer.CrimeLevel}";
                if (dataPlayer.CrimeLevel > 2) _runButton.interactable = false;
                else _runButton.interactable = true;
                break;
        }
    }
}