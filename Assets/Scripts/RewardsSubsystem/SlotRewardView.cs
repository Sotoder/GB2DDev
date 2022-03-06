using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotRewardView : MonoBehaviour
{
    [SerializeField]
    private Image _rewardIcon;
    [SerializeField]
    private Image _highlightImage;
    [SerializeField]
    private Image _selectImage;
    [SerializeField]
    private TMP_Text _textDays;
    [SerializeField]
    private TMP_Text _countText;

    public void SetData(Reward reward, int dayNum, bool isGet, bool isSelect)
    {
        _rewardIcon.sprite = reward.Icon;
        _textDays.text = $"Day {dayNum}";
        _countText.text = reward.Count.ToString();
        _highlightImage.gameObject.SetActive(isGet);
        _selectImage.gameObject.SetActive(isSelect);
    }
}
