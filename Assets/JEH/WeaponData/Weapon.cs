using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected bool _isReloading;
    protected bool _isLeftPress;
    protected int _currentAmmo;
    protected int _layerMask;
    protected float lastFireTime;

    protected Coroutine _reloadCoroutine;


    protected virtual void Initialize()
    {
        transform.localPosition = Vector3.zero;
        _isReloading = false;
        _isLeftPress = false;

        _reloadCoroutine = null;
        lastFireTime = -100;

    }
    protected abstract void Shoot();


    protected virtual void CartridgeEmpty()
    {
        Debug.Log("총알이 부족해요");
    }

    protected virtual float DamageCalculation(Vector3 target, float damage, float range)
    {
        float per = (Vector3.Distance(transform.position, target) / range * 100); // 피격위치가 사정거리의 몇퍼센트인지
        return damage * (1 - per / 100); // 떨어진 거리만큼 퍼센트로 데미지 감소
    }

}
