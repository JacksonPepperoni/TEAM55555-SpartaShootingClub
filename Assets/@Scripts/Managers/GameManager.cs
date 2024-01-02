using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Start()
    {
        base.Start();

        // 1. ���ҽ� �ε�
        ResourceLoad((key, count, total) =>
        {
            Debug.Log($"{key}, {count}, {total}");
            if (count == total)
            {
                // 2. ��ü ����, �ʱ�ȭ
                Debug.Log("���ҽ� �ε� �Ϸ�. ��ü ����, �ʱ�ȭ ����");
            }
        });
    }

    private void ResourceLoad(Action<string, int, int> callback = null)
    {
        // Resource.LoadAllAsync�� ���
        // LoadAsync()�� ������ �ݺ��ؼ� �ʿ��� ���ҽ��� ĳ���ؾ��� �� �����ϴ�.

        ResourceManager.Instance.LoadAllAsync<UnityEngine.Object>(callback);
    }
}