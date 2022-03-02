using System;
using System.Collections.Generic;

public class SaveLoadDataController: IDisposable
{
    private MementoSaver _mementoSaver;
    private List<IViewWithSaveAndLoadButton> _viewsWhithButton = new List<IViewWithSaveAndLoadButton>();
    private SaveAndLoadRepository _repository;

    public SaveLoadDataController(MementoSaver mementoSaver, List<IViewWithSaveAndLoadButton> viewsWithButtonList, LoadManager loadManager)
    {
        _mementoSaver = mementoSaver;
        _viewsWhithButton = viewsWithButtonList;
        _repository = new SaveAndLoadRepository(loadManager);
        SignOnEvents();
    }

    private void SignOnEvents()
    {
        foreach (var view in _viewsWhithButton)
        {
            view.SaveButton.onClick.AddListener(Save);
            view.LoadButton.onClick.AddListener(Load);
        }
    }

    private void Save()
    {
        var dataForSave = _mementoSaver.GetLastMementoForSave();
        if (dataForSave is null) return;
        _repository.Save(dataForSave);
    }

    public void Load()
    {
        _repository.Load();
    }

    public void Dispose()
    {
        foreach (var view in _viewsWhithButton)
        {
            view.SaveButton.onClick.RemoveListener(Save);
            view.LoadButton.onClick.RemoveListener(Load);
        }
    }
}