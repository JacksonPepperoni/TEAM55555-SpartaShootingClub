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
                InitializeRoom();

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
                EnemyManager.Instance.Initialize();
            }
        });
    }

    private void InitializeRoom()
    {
        var entryRoom = ResourceManager.Instance.GetCache<GameObject>("EntryRoom");
        entryRoom = Instantiate(entryRoom);
        var room1 = ResourceManager.Instance.GetCache<GameObject>("ONE_LINE");
        room1 = Instantiate(room1, new(0, 20, 0), Quaternion.identity);
        var room2 = ResourceManager.Instance.GetCache<GameObject>("Wave_LINE");
        room2 = Instantiate(room2, new(0, 40, 0), Quaternion.identity);
        var room3 = ResourceManager.Instance.GetCache<GameObject>("Ground");
        room3 = Instantiate(room3, new(0, 60, 0), Quaternion.identity);

        var portals = entryRoom.GetComponentsInChildren<RoomPortal>();
        portals[0].SetTargetRoom(room1, 1);
        portals[1].SetTargetRoom(room2, 2);
        portals[2].SetTargetRoom(room3, 3);

        portals = room1.GetComponentsInChildren<RoomPortal>();
        portals[0].SetTargetRoom(entryRoom, 0);
        portals = room2.GetComponentsInChildren<RoomPortal>();
        portals[0].SetTargetRoom(entryRoom, 0);
        portals = room3.GetComponentsInChildren<RoomPortal>();
        portals[0].SetTargetRoom(entryRoom, 0);
    }

    public void EnterRoom(GameObject room, int level)
    {
        var enterPosition = room.transform.Find("PlayerEnterPosition").position;
        Player.gameObject.SetActive(false);
        Player.transform.position = enterPosition;
        Camera.main.transform.forward = Vector3.forward;
        Player.gameObject.SetActive(true);
        EnemyManager.Instance.level = level;
        EnemyManager.Instance.SetEnemy();
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
