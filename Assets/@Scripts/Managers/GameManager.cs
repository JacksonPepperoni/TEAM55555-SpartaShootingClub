using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Start()
    {
        base.Start();

        // 1. 리소스 로드
        ResourceLoad((key, count, total) =>
        {
            Debug.Log($"{key}, {count}, {total}");
            if (count == total)
            {
                // 2. 객체 생성, 초기화
                Debug.Log("리소스 로드 완료. 객체 생성, 초기화 시작");
            }
        });
    }

    private void ResourceLoad(Action<string, int, int> callback = null)
    {
        // Resource.LoadAllAsync가 없어서
        // LoadAsync()를 여러번 반복해서 필요한 리소스를 캐싱해야할 것 같습니다.

        ResourceManager.Instance.LoadAllAsync<UnityEngine.Object>(callback);
    }
}