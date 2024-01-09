using UnityEngine;

public class ReturnPool : MonoBehaviour
{
    [SerializeField] private float _returnTime;
    private void OnEnable()
    {
        Invoke("Destroy", _returnTime);
    }

   private void Destroy()
    {
        ResourceManager.Instance.Destroy(this.gameObject);
    }

}
