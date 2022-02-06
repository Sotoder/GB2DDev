using Model.Shop;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonByGoldPack;
    [SerializeField] private TextMeshProUGUI _goldCountLabel;

    private UnityAction<int> _buyProductAction;
    private ProfilePlayer _profilePlayer;


    public void Init(UnityAction startGame, UnityAction<int> buyProductAction, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _profilePlayer.CurentGold.SubscribeOnChange(UpdateGoldCount);
        _buttonStart.onClick.AddListener(startGame);
        _buttonByGoldPack.onClick.AddListener(BuyGoldPackIsPressed);
        _buyProductAction = buyProductAction;
    }

    private void UpdateGoldCount(int curentGold)
    {
        _goldCountLabel.text = String.Concat("Gold: ", curentGold.ToString());
    }

    private void BuyGoldPackIsPressed()
    {
        _buyProductAction.Invoke(2213);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonByGoldPack.onClick.RemoveAllListeners();
        _profilePlayer.CurentGold.UnSubscriptionOnChange(UpdateGoldCount);
    }
}