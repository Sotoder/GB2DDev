using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonInventory;

    public void Init(UnityAction startGame, UnityAction showInventory)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buttonInventory.onClick.AddListener(showInventory);
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