using System;
using UnityEngine;

public class WJY_TestSceneManager : Singleton<WJY_TestSceneManager>
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

                var ground = ResourceManager.Instance.GetCache<GameObject>("Ground");
                Instantiate(ground);
                var player = ResourceManager.Instance.GetCache<GameObject>("PlayerCharacter");
                Instantiate(player);

                InputManager.Instance.Initialize();
            }
        });
    }

    private void ResourceLoad(Action<string, int, int> callback = null)
    {
        // Resource.LoadAllAsync�� ���
        // LoadAsync()�� ������ �ݺ��ؼ� �ʿ��� ���ҽ��� ĳ���ؾ��� �� �����ϴ�.
        // ����, LoadAllAsync �޼��� ���ο� ĳ���� �ʿ��� ������Ʈ�� �ּҸ� ����ϰ�,
        // �� �ּҵ��� �ݺ����� ���� LoadAsnyc�� �ε��ϵ��� �߽��ϴ�.

        ResourceManager.Instance.LoadAllAsync<UnityEngine.Object>(callback);
    }
}