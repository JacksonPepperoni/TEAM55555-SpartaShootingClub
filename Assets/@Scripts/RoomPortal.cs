using UnityEngine;

public class RoomPortal : MonoBehaviour
{
    private GameObject _targetRoom;
    private int _level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            MainScene.Instance.EnterRoom(_targetRoom, _level);
    }

    public void SetTargetRoom(GameObject room, int level)
    {
        _targetRoom = room;
        _level = level;
    }
}