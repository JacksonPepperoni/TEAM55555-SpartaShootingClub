using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected bool _isReloading;
    protected bool _isLeftPress;
    protected int _currentAmmo;
    protected int _layerMask;
    protected float lastFireTime;

    protected Coroutine _reloadCoroutine;

    protected abstract void Initialize();
    protected abstract void Shoot();

    protected virtual void CartridgeEmpty()
    {
        Debug.Log("총알이 부족해요");
    }

}
