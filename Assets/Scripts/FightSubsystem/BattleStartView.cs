using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleStartView: MonoBehaviour, IView
{
    [SerializeField]
    private Button _startFightButton;

    public void Init(UnityAction startBattle)
    {
        _startFightButton.onClick.AddListener(startBattle);
    }

    public void Hide()
    {
        gameObject.SetActive(true);
    }

    public void Show()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _startFightButton.onClick.RemoveAllListeners();
    }
}

