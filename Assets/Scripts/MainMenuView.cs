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

    private UnityAction _startGame;
    private UnityAction _showInventory;

    private Tween _startButtonScaleTween;
    private bool _isGameStarted = false;

    public void Init(UnityAction startGame, UnityAction showInventory)
    {
        _startGame = startGame;
        _showInventory = showInventory;

        _buttonStart.onClick.AddListener(StartGameButtonPressed);
        _buttonInventory.onClick.AddListener(InventoryButtonPressed);
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
            _startButtonScaleTween = _buttonStart.transform.DOScale(Vector3.one * 20, 1f)
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
    }

    public void Show()
    {
        
    }

    public void Hide()
    {
        
    }
}