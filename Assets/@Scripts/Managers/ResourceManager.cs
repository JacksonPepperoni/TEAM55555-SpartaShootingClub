using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, UnityEngine.Object> _resources = new();

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out var resource))
        {
            callback?.Invoke(resource as T);
            return;
        }

        var operation = Resources.LoadAsync<T>(key);
        operation.completed += op =>
        {
            _resources.TryAdd(operation.asset.name, operation.asset);
            callback?.Invoke(operation.asset as T);
        };
    }

    public void LoadAllAsync<T>(Action<string, int, int> callback = null) where T : UnityEngine.Object
    {
        string[] preloadKeys = new string[]
        {
            // 게임 시작 전 캐싱할 Object의 Resources 주소 입력
            @"Prefabs/Ground",
            @"Prefabs/PlayerCharacter",
            @"PlayerInputActions",
        };

        int loadCount = 0;
        int totalCount = preloadKeys.Length;

        // preloadKeys 배열의 주소들을 로드
        foreach (var key in preloadKeys)
        {
            LoadAsync<T>(key, obj =>
            {
                loadCount++;
                callback?.Invoke(key, loadCount, totalCount);
            });
        }
    }

    public T GetCache<T>(string key) where T : UnityEngine.Object
    {
        if (!_resources.TryGetValue(key, out var resource))
            return null;
        return resource as T;
    }

    public void Release(string key)
    {
        if (_resources.TryGetValue(key, out var resource))
        {
            Resources.UnloadAsset(resource);
            _resources.Remove(key);
        }
    }
}