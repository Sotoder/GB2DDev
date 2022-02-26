using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour, IInventoryView
{
    [SerializeField] private Dropdown _transmissionDropDown;
    [SerializeField] private Dropdown _tiresDropDown;
    [SerializeField] private Dropdown _windowDropDown;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Text _saveButtonText;

    public UnityAction<List<UpgradeItemConfig>> UpgradeSaved { get; set; }

    private IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private List<UpgradeItemConfig> _selectedUpgradeItems = new List<UpgradeItemConfig>();
    private bool _isOnGameScene;
    private Tween _scaleTween;
    private Tween _rotationTween;
    private Sequence _seq;

    private void Start()
    {
        this.transform.localScale = Vector3.zero;
    }

    public void Display(IReadOnlyList<IItem> items)
    {
        foreach (var item in items)
            Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
    }

    public void Init(IReadOnlyList<UpgradeItemConfig> upgradeItems)
    {
        _upgradeItems = upgradeItems;

        List<string> transmissionOptions = new List<string>();
        List<string> tireOptions = new List<string>();
        List<string> windowOptions = new List<string>();
        foreach (var item in upgradeItems)
        {
            switch (item.Id)
            {
                case 0:
                    transmissionOptions.Add(item.name);
                    break;
                case 1:
                    tireOptions.Add(item.name);
                    break;
                case 10:
                    windowOptions.Add(item.name);
                    break;
            }
        }

        _transmissionDropDown.AddOptions(transmissionOptions);
        _tiresDropDown.AddOptions(tireOptions);
        _windowDropDown.AddOptions(windowOptions);

        _saveButton.onClick.AddListener(SaveUpgrades);
    }

    public void Show()
    {
        if (_isOnGameScene)
        {
            SetDropdownInteractable(false);
            _saveButtonText.text = "Exit";
        } else
        {
            SetDropdownInteractable(true);
            _saveButtonText.text = "Save & Exit";
        }

        _scaleTween?.Complete(true);
        _rotationTween?.Complete(true);
        _seq?.Complete(true);

        _scaleTween = transform.DOScale(Vector3.one, 0.5f);
        _rotationTween = transform.DOPunchRotation(new Vector3(0, 0, 360), 0.5f);

        _seq = DOTween.Sequence();
        _seq.Append(_scaleTween.OnComplete(() => _scaleTween = null));
        _seq.Join(_rotationTween.OnComplete(() => _rotationTween = null));
        _seq.OnComplete(() => _seq = null);
    }

    private void SetDropdownInteractable(bool flag)
    {
        _transmissionDropDown.interactable = flag;
        _tiresDropDown.interactable = flag;
        _windowDropDown.interactable = flag;
    }

    public void SaveUpgrades()
    {
        if (_isOnGameScene)
        {
            Hide();
            return;
        }

        _selectedUpgradeItems.Clear();
        
        if (_transmissionDropDown.value != 0)
        {
            AddSelectedUpgrade(_transmissionDropDown.options[_transmissionDropDown.value].text);
        }

        if (_tiresDropDown.value != 0)
        {
            AddSelectedUpgrade(_tiresDropDown.options[_tiresDropDown.value].text);
        }

        if (_windowDropDown.value != 0)
        {
            AddSelectedUpgrade(_windowDropDown.options[_windowDropDown.value].text);
        }

        UpgradeSaved?.Invoke(_selectedUpgradeItems);

        Hide();
    }

    private void AddSelectedUpgrade(string upgradeItemName)
    {
        var item = _upgradeItems.FirstOrDefault(upgrade => upgrade.name == upgradeItemName);
        _selectedUpgradeItems.Add(item);
    }

    public void SetOnGameSceneFlag(bool isOnScene)
    {
        _isOnGameScene = isOnScene;
    }

    public void Hide()
    {
        _scaleTween?.Complete(true);
        _rotationTween?.Complete(true);
        _seq?.Complete(true);

        _scaleTween = transform.DOScale(Vector3.zero, 0.5f);
        _rotationTween = transform.DOPunchRotation(new Vector3(0, 0, -360), 0.5f);

        _seq = DOTween.Sequence();
        _seq.Append(_scaleTween.OnComplete(() => _scaleTween = null));
        _seq.Join(_rotationTween.OnComplete(() => _rotationTween = null));
        _seq.OnComplete(() => _seq = null);
    }

    public void OnDestroy()
    {
        _saveButton.onClick.RemoveAllListeners();
    }
}
