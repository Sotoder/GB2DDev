using DG.Tweening;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CarController : BaseController, IAbilityActivator
{ 
    private readonly CarView _carView;
    private AsyncOperationHandle<GameObject> _carOperationHandle;

    private SubscriptionProperty<float> _leftMoveDiff;
    private SubscriptionProperty<float> _rightMoveDiff;
    private SubscriptionProperty<bool> _isStay;

    private Tween _forwardWheelTween = null;
    private Tween _backWheelTween = null;
    private bool _isMovingForward = false;

    public CarController(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff, SubscriptionProperty<bool> isStay, AssetReferenceGameObject carViewReference)
    {
        _carView = LoadView(carViewReference);
        _isStay = isStay;
        _leftMoveDiff = leftMoveDiff;
        _rightMoveDiff = rightMoveDiff;

        _isStay.SubscribeOnChange(Stop);
        _leftMoveDiff.SubscribeOnChange(LeftMove);
        _rightMoveDiff.SubscribeOnChange(RightMove);
    }

    private void Stop(bool isStay)
    {
        if (!isStay) return;

        _forwardWheelTween.Complete(true);
        _backWheelTween.Complete(true);
    }

    private void RightMove(float rightDiff)
    {
        if(!_isMovingForward)
        {
            _isMovingForward = true;
            _forwardWheelTween?.Complete(true);
            _backWheelTween?.Complete(true);
        }

        //Unity 2019 can't this
        //_forwardWheelTween ??= RotateWheelTween(360, 1, 10, _carView.ForwardWheel).OnComplete(() => _forwardWheelTween = null);
        //_backWheelTween ??= RotateWheelTween(360, 1, 10, _carView.BackWheel).OnComplete(() => _backWheelTween = null);

        //Maybe this? Or it's the same?
        //_forwardWheelTween = _forwardWheelTween ?? RotateWheelTween(360, 1, 10, _carView.ForwardWheel).OnComplete(() => _forwardWheelTween = null);
        //_backWheelTween = _backWheelTween ?? RotateWheelTween(360, 1, 10, _carView.BackWheel).OnComplete(() => _backWheelTween = null);

        if (_forwardWheelTween == null) 
        {
            _forwardWheelTween = RotateWheelTween(-360, _carView.RotateSpeed, _carView.RotateLoopCount, _carView.ForwardWheel).OnComplete(() => _forwardWheelTween = null);
        }

        if (_backWheelTween == null)
        {
            _backWheelTween = RotateWheelTween(-360, _carView.RotateSpeed, _carView.RotateLoopCount, _carView.BackWheel).OnComplete(() => _backWheelTween = null);
        }
    }

    private void LeftMove(float leftDiff)
    {
        if (_isMovingForward)
        {
            _isMovingForward = false;
            _forwardWheelTween.Complete(true);
            _backWheelTween.Complete(true);
        }

        if (_forwardWheelTween == null)
        {
            _forwardWheelTween = RotateWheelTween(360, _carView.RotateSpeed, _carView.RotateLoopCount, _carView.ForwardWheel).OnComplete(() => _forwardWheelTween = null);
        }

        if (_backWheelTween == null)
        {
            _backWheelTween = RotateWheelTween(360, _carView.RotateSpeed, _carView.RotateLoopCount, _carView.BackWheel).OnComplete(() => _backWheelTween = null);
        }
    }

    private Tween RotateWheelTween(float degreeOfRotation, float rotationTime, int loopCount, GameObject wheel)
    {
        var tween = wheel.transform.DORotate(new Vector3(0, 0, degreeOfRotation), rotationTime, RotateMode.FastBeyond360)
            .SetLoops(loopCount)
            .SetEase(Ease.Linear);
        return tween;  
    }

    private CarView LoadView(AssetReferenceGameObject carViewReference)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(carViewReference);
        var result = handle.WaitForCompletion();
        var go = GameObject.Instantiate(result);

        _carOperationHandle = handle;
        AddGameObjects(go);

        return go.GetComponent<CarView>();
    }

    public GameObject GetViewObject()
    {
        return _carView.gameObject;
    }

    protected override void OnDispose()
    {
        if (_carOperationHandle.IsValid())
        {
            Addressables.Release(_carOperationHandle);
        }
    }
}

