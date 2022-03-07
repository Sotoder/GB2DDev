using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ResourceLoader
{
    public static GameObject LoadPrefab(ResourcePath path)
    {
        return Resources.Load<GameObject>(path.PathResource);
    }

    public static T LoadObject<T>(ResourcePath path) where T:Object
    {
        return Resources.Load<T>(path.PathResource);
    }

    public static T LoadAndInstantiateView<T>(ResourcePath path, Transform uiRoot) where T:Component, IView
    {
        var prefab = Resources.Load<GameObject>(path.PathResource);
        var go = GameObject.Instantiate(prefab, uiRoot);
        return go.GetComponent<T>();
    }

    public static (AsyncOperationHandle<GameObject> handle, T view) AdressableLoadAndInstantiateView<T>(AssetReferenceGameObject reference, Transform uiRoot) where T : Component, IView
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(reference);
        var result = handle.WaitForCompletion();
        var go = GameObject.Instantiate(result, uiRoot);
        return (handle, go.GetComponent<T>());
    }

    public static T LoadAndInstantiateObject<T>(ResourcePath path, Transform uiRoot) where T : Object
    {
        var prefab = Resources.Load<GameObject>(path.PathResource);
        var go = GameObject.Instantiate(prefab, uiRoot);
        return go.GetComponent<T>();
    }

    public static AsyncOperationHandle<T> LoadDataSource<T>(AssetReference sourse) where T : ScriptableObject
    {
        return Addressables.LoadAssetAsync<T>(sourse);
    }
} 
