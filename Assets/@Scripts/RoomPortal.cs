using UnityEngine;

public class RoomPortal : MonoBehaviour
{
    [SerializeField] private GameObject _targetRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            MainScene.Instance.EnterRoom(_targetRoom);
    }

    public void SetTargetRoom(GameObject room)
    {
        _targetRoom = room;
    }
}