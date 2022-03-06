using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour, ISavebleRewardView, IViewWithSaveAndLoadButton, ILoadableRewardView, ISwitchableRewardView
{
    #region InspectorFields
    [SerializeField]
    private string _name;
    [Space]
    [Header("Time settings")]
    [SerializeField]
    public int TimeCooldown = 86400;
    [SerializeField]
    public int TimeDeadline = 172800;
    [Space]
    [Header("RewardSettings")]
    public List<Reward> Rewards;
    [Header("UI")]
    [SerializeField]
    private GameObject _uiContainer;
    [SerializeField]
    public TMP_Text RewardTimer;
    [SerializeField]
    public Transform SlotsParent;
    [SerializeField]
    public Slider ProgressBar;
    [SerializeField]
    public SlotRewardView SlotPrefab;
    [SerializeField]
    public Button ResetButton;
    [SerializeField]
    private Button _saveButton;
    [SerializeField]
    private Button _loadButton;
    [SerializeField]
    public Button GetRewardButton;
    #endregion

    public List<SlotRewardView> Slots;
    public bool RewardReceived = false;
    private RewardData _rewardData;

    private int _id;

    public int ID => _id;
    public Action UserGetReward { get; set; }
    public Action UserResetView { get; set; }

    public Button SaveButton { get => _saveButton; }
    public Button LoadButton { get => _loadButton; }
    public string Name { get => _name; }
    public GameObject UIContainer { get => _uiContainer; }

    private void Awake()
    {
        _id = Animator.StringToHash(Name);
    }

    public RewardView ConfigurateRewardView(RewardData rewardData)
    {
        _rewardData = rewardData;
        return this;
    }
    public void Load(RewardViewMemento rewardViewMemento)
    {
        _rewardData.CurrentActiveSlot.Value = rewardViewMemento.CurrentActiveSlot;
        if (rewardViewMemento.LastRewardTime == "") _rewardData.LastRewardTime.Value = new DateTime();
        else _rewardData.LastRewardTime.Value = DateTime.Parse(rewardViewMemento.LastRewardTime);
    }

    public int CurrentActiveSlot 
    { 
        get => _rewardData.CurrentActiveSlot.Value;
        set => _rewardData.CurrentActiveSlot.Value = value;
    }

    public DateTime? LastRewardTime
    {
        get => _rewardData.LastRewardTime.Value;
        set => _rewardData.LastRewardTime.Value = value;
    }

    private void OnDestroy()
    {
        GetRewardButton.onClick.RemoveAllListeners();
        ResetButton.onClick.RemoveAllListeners();
        SaveButton.onClick.RemoveAllListeners();
        LoadButton.onClick.RemoveAllListeners();
    }
}
