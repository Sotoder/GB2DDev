using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UI;

public class FightWindowView : MonoBehaviour, IView
{
    [SerializeField]
    public TMP_Text CountMoneyText;

    [SerializeField]
    public TMP_Text CountHealthText;

    [SerializeField]
    public TMP_Text CountPowerText;

    [SerializeField]
    public TMP_Text CrimeLevelText;

    [SerializeField]
    public TMP_Text CountPowerEnemyText;


    [SerializeField]
    private Button _addMoneyButton;

    [SerializeField]
    private Button _minusMoneyButton;


    [SerializeField]
    private Button _addHealthButton;

    [SerializeField]
    private Button _minusHealthButton;


    [SerializeField]
    private Button _addPowerButton;

    [SerializeField]
    private Button _minusPowerButton;

    [SerializeField]
    private Button _minusCrimeLevelButton;

    [SerializeField]
    private Button _fightButton;

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    public Button RunButton;

    [SerializeField]
    public Toggle GunToggle;

    [SerializeField]
    public Toggle KnifeToogle;

    public void Init(UnityAction<int, DataType> changeDataWindow, UnityAction fight,
                     UnityAction run, UnityAction<bool, bool> changeCrimeLevel,
                     UnityAction<bool> changeStateOnGun, UnityAction<bool> changeStateOnKnife,
                     UnityAction exit)
    {
        _addMoneyButton.onClick.AddListener(() => {
            changeDataWindow(1, DataType.Money);
            changeDataWindow(1, DataType.OverallMoney);
            });
        _minusMoneyButton.onClick.AddListener(() => changeDataWindow(-1, DataType.Money));

        _addHealthButton.onClick.AddListener(() => changeDataWindow(1, DataType.Health));
        _minusHealthButton.onClick.AddListener(() => changeDataWindow(-1, DataType.Health));

        _addPowerButton.onClick.AddListener(() => changeDataWindow(1, DataType.Power));
        _minusPowerButton.onClick.AddListener(() => changeDataWindow(-1, DataType.Power));

        _minusCrimeLevelButton.onClick.AddListener(() => changeCrimeLevel(false, true));

        GunToggle.onValueChanged.AddListener(changeStateOnGun);
        KnifeToogle.onValueChanged.AddListener(changeStateOnKnife);

        _fightButton.onClick.AddListener(fight);
        RunButton.onClick.AddListener(run);
        _exitButton.onClick.AddListener(exit);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _minusCrimeLevelButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();
        RunButton.onClick.RemoveAllListeners();

        GunToggle.onValueChanged.RemoveAllListeners();
        KnifeToogle.onValueChanged.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
