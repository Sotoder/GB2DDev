using System;
using System.Linq;
using Tools;
using UnityObject = UnityEngine.Object;

public class InputGameController : BaseController
{
    public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car, InputConfig inputConfig)
    {
        _inputConfig = inputConfig;
        _view = LoadView();
        _view.Init(leftMove, rightMove, car.Speed);
    }

    private InputConfig _inputConfig;
    private BaseInputView _view;

    private BaseInputView LoadView()
    {
        if(_inputConfig.InputViewMatchings.IsHaveDuplicatesInInputTypes())
        {
            throw new Exception("InputConfig -> Input View Matchings have duplicates in InputType fields");
        }

        var inputView = _inputConfig.InputViewMatchings.FirstOrDefault(view => view.InputType == _inputConfig.SelectedInputType).InputView;

        var objView = UnityObject.Instantiate(inputView.gameObject);
        AddGameObjects(objView);

        return inputView;
    }
}

