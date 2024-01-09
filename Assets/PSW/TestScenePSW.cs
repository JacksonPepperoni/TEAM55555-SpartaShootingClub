using System;
using UnityEngine;

public class TestScenePSW : Singleton<TestScenePSW>
{
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
                Instantiate(player);
                var weapon = ResourceManager.Instance.GetCache<GameObject>("Weapon");
                Instantiate(weapon, Camera.main.transform.GetChild(0));

                InputManager.Instance.Initialize();

                // 3. UI 초기화, 생성
                SettingsManager.Instance.Initialize();
                UIManager.Instance.Initialize();

                var trainingScene = ResourceManager.Instance.GetCache<GameObject>("UI_Training_Scene");
                UIManager.Instance.ShowScene<UIScene>(trainingScene);
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
