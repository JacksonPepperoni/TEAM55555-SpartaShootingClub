using UnityEngine;

public class ReturnPool : MonoBehaviour
{
    void OnEnable()
    {
        Destroy(this.gameObject, 4);
    }

}
