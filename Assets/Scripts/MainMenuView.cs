using DG.Tweening;
using System;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonInventory;
    [SerializeField] private Button _buttonRewards;

    private UnityAction _startGame;
    private UnityAction _showInventory;
    private UnityAction _openRewardsWindow;

    private Tween _startButtonScaleTween;
    private bool _isGameStarted = false;

    public void Init(UnityAction startGame, UnityAction showInventory, UnityAction openRewardsWindow)
    {
        _startGame = startGame;
        _showInventory = showInventory;
        _openRewardsWindow = openRewardsWindow;

        _buttonStart.onClick.AddListener(StartGameButtonPressed);
        _buttonInventory.onClick.AddListener(InventoryButtonPressed);
        _buttonRewards.onClick.AddListener(RewardsButtonPressed);
    }

    private void RewardsButtonPressed()
    {
        _openRewardsWindow.Invoke();
    }

    private void InventoryButtonPressed()
    {
        _showInventory.Invoke();
    }

    private void StartGameButtonPressed()
    {
        if(!_isGameStarted)
        {
            _isGameStarted = true;
            _buttonInventory.gameObject.SetActive(false);
            _startButtonScaleTween = _buttonStart.transform?.DOScale(Vector3.one * 20, 1f)
                .OnComplete(() =>
                {
                    _startGame.Invoke();
                    _startButtonScaleTween = null;
                });
        }

    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonInventory.onClick.RemoveAllListeners();
        _startButtonScaleTween = null;
    }

    public void Show()
    {
        
    }

    public void Hide()
    {
        
    }
}