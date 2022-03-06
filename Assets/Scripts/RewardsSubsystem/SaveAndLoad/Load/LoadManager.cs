using System.Collections.Generic;

public class LoadManager
{
    private List<ILoadableRewardView> _loadebleRewardViews = new List<ILoadableRewardView>();
    private CurrencyWindow _currencyWindow;
    public LoadManager(List<ILoadableRewardView> loadableViews, CurrencyWindow currencyWindow)
    {
        _loadebleRewardViews = loadableViews;
        _currencyWindow = currencyWindow;
    }

    public void Load(GameMemento gameMemento)
    {
        foreach (var loadebleView in _loadebleRewardViews)
        {
            foreach (var memento in gameMemento.RewardViewMementos)
            {
                if (loadebleView.ID == memento.ViewID)
                {
                    loadebleView.Load(memento);
                    break;
                }
            }
        }

        _currencyWindow.Load(gameMemento.CurrencyMemento);
    }
}