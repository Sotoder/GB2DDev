using System;
using System.Collections.Generic;

public class MementoSaver: IDisposable
{
    private List<ISavebleRewardView> _savebleViews = new List<ISavebleRewardView>();
    private List<GameMemento> _gameMementos = new List<GameMemento>(8);
    private CurrencyWindow _currencyWindow;

    public MementoSaver(List<ISavebleRewardView> rewardViews, CurrencyWindow currencyWindow)
    {
        _savebleViews = rewardViews;
        _currencyWindow = currencyWindow;

        foreach (var view in _savebleViews)
        {
            view.UserGetReward += SaveMemento;
            view.UserResetView += SaveMemento;
        }
    }

    private void SaveMemento()
    {
        List<RewardViewMemento> viewMementos = new List<RewardViewMemento>();

        foreach (var view in _savebleViews)
        {
            viewMementos.Add(
                new RewardViewMemento (
                    view.ID,
                    view.CurrentActiveSlot,
                    view.LastRewardTime.ToString())
                );
        }

        CurrencyViewMemento currency = new CurrencyViewMemento(_currencyWindow.GetWoodCount(), _currencyWindow.GetDiamondCount()); 

        if (_gameMementos.Count > 7)
        {
            _gameMementos.RemoveAt(0);
        }

        _gameMementos.Add(new GameMemento(viewMementos, currency));
    }

    public GameMemento GetLastMementoForSave()
    {
        if (_gameMementos.Count == 0) return null;

        return _gameMementos[_gameMementos.Count - 1];
    }

    public void Dispose()
    {
        foreach (var view in _savebleViews)
        {
            view.UserGetReward -= SaveMemento;
            view.UserResetView -= SaveMemento;
        }
    }
}