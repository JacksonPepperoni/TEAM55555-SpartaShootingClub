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
            @"Prefabs/Weapon",
            @"Prefabs/UI/UI_Training_Scene",
            @"Prefabs/UI/UI_Popup_GunManage",
            @"Prefabs/UI/UI_Popup_Options",
            @"Prefabs/UI/Panels/UI_Panel_Audio",
            @"Prefabs/UI/Panels/UI_Panel_Control",
            @"Prefabs/UI/Panels/UI_Panel_Graphic",

            @"Prefabs/Effect/SmallExplosion",
            @"Prefabs/Effect/Metal",
            @"Prefabs/Projectile/Bomb",

            // 사운드
            @"Audios/AudioMixer",
            @"Audios/SFX/Movement/S_CH_Loop_Walking",
            @"Audios/SFX/Movement/S_CH_Loop_Running",
            
            // 총기 프리팹
            @"Prefabs/Weapon/Gun_Handgun",
            @"Prefabs/Weapon/Gun_MachineGun",
            @"Prefabs/Weapon/Gun_Shotgun",
            @"Prefabs/Weapon/Gun_SniperRifle"
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



    // 풀생성 GameObject obj = ResourceManager.Instance.InstantiatePrefab(prefab name);
    public GameObject InstantiatePrefab(string key)
    {
        GameObject prefab = GetCache<GameObject>(key);

        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        return PoolManager.Instance.Pop(prefab);
    }

    // 오브젝트를 풀에 돌려놓거나 파괴한다. ResourceManager.Instance.Destroy(this.gameobject);
    public void Destroy(GameObject obj)
    {
        if (obj == null) return;
        if (PoolManager.Instance.Push(obj)) return;

        UnityEngine.Object.Destroy(obj);
    }
}