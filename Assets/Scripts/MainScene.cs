using System;
using UnityEngine;

public class MainScene : Singleton<MainScene>
{
    private PlayerController _player;
    public PlayerController Player => _player;

    protected override void Start()
    {
        base.Start();

        // 1. 리소스 로드
        ResourceLoad((key, count, total) =>
        {
            if (count == total)
            {
                // 2. 객체 생성, 초기화

                var ground = ResourceManager.Instance.GetCache<GameObject>("Ground");
                Instantiate(ground);
                var player = ResourceManager.Instance.GetCache<GameObject>("PlayerCharacter");
                _player = Instantiate(player).GetComponent<PlayerController>();

                // 3. UI 생성
                UIManager.Instance.Initialize();
                var trainingScene = ResourceManager.Instance.GetCache<GameObject>("UI_Training_Scene");
                UIManager.Instance.ShowScene<UIScene>(trainingScene);

                SettingsManager.Instance.Initialize();
                CinemachineManager.Instance.Initialize();
                InputManager.Instance.Initialize();
                PoolManager.Instance.Initialize();
                AudioManager.Instance.Initialize();
                WeaponEquipManager.Instance.Initialize();
                JsonManager.Instance.Initialize();



            }
        });
    }

    private void ResourceLoad(Action<string, int, int> callback = null)
    {
        // Resource.LoadAllAsync가 없어서
        // LoadAsync()를 여러번 반복해서 필요한 리소스를 캐싱해야할 것 같습니다.
        // 따라서, LoadAllAsync 메서드 내부에 캐싱이 필요한 오브젝트의 주소를 등록하고,
        // 그 주소들을 반복문을 돌며 LoadAsnyc로 로드하도록 했습니다.

        ResourceManager.Instance.LoadAllAsync<UnityEngine.Object>(callback);
    }
}
