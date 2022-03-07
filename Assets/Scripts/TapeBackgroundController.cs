using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TapeBackgroundController : BaseController
{
    public TapeBackgroundController(IReadOnlySubscriptionProperty<float> leftMove, 
        IReadOnlySubscriptionProperty<float> rightMove,
        Car currentCar,
        AssetReferenceGameObject pathViewReference)
    {
        _currentCar = currentCar;
        _view = LoadView(pathViewReference);
        _diff = new SubscriptionProperty<float>();
        
        _leftMove = leftMove;
        _rightMove = rightMove;
        
        _view.Init(_diff);
        
        _leftMove.SubscribeOnChange(Move);
        _rightMove.SubscribeOnChange(Move);
    }
    
    private TapeBackgroundView _view;
    private AsyncOperationHandle<GameObject> _pathOperationHandle;
    private readonly SubscriptionProperty<float> _diff;
    private readonly IReadOnlySubscriptionProperty<float> _leftMove;
    private readonly IReadOnlySubscriptionProperty<float> _rightMove;
    private readonly Car _currentCar;

    protected override void OnDispose()
    {
        _leftMove.UnSubscriptionOnChange(Move);
        _rightMove.UnSubscriptionOnChange(Move);
        
        if (_pathOperationHandle.IsValid())
        {
            Addressables.Release(_pathOperationHandle);
        }

        base.OnDispose();
    }

    private TapeBackgroundView LoadView(AssetReferenceGameObject pathViewReference)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(pathViewReference);
        var result = handle.WaitForCompletion();
        var go = GameObject.Instantiate(result);

        _pathOperationHandle = handle;
        AddGameObjects(go);

        return go.GetComponent<TapeBackgroundView>();
    }

    private void Move(float value)
    {
        if (value < 0)
        _diff.Value = value - _currentCar.Speed;
        else
        _diff.Value = value + _currentCar.Speed;
    }
}

