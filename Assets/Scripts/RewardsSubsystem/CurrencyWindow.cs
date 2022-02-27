using TMPro;
using UnityEngine;

public class CurrencyWindow : MonoBehaviour
{
    private CurrencyData _currency;

    [SerializeField]
    private TextMeshProUGUI _diamondText;
    [SerializeField]
    private TextMeshProUGUI _woodText;

    public void SetProfile(CurrencyData currency)
    {
        _currency = currency;
        _currency.Wood.SubscribeOnChange(RefreshText);
        _currency.Diamond.SubscribeOnChange(RefreshText);
    }

    public void Load(CurrencyViewMemento currencyMemento)
    {
        _currency.Wood.Value = currencyMemento.Wood;
        _currency.Diamond.Value = currencyMemento.Diamond;
    }

    public void AddDiamond(int count)
    {
        _currency.Diamond.Value += count;
    }

    public void AddWood(int count)
    {
        _currency.Wood.Value += count;
    }

    public int GetWoodCount()
    {
        return _currency.Wood.Value;
    }

    public int GetDiamondCount()
    {
        return _currency.Diamond.Value;
    }

    private void RefreshText(int value)
    {
        if (_diamondText != null)
            _diamondText.text = _currency.Diamond.Value.ToString();
        if (_woodText != null)
            _woodText.text = _currency.Wood.Value.ToString();
    }

}
