using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon/Gun")]
public class WeaponData_Gun : WeaponData
{
    [Header("총 설정")]
    [SerializeField] private int _shotAtOnce; // 한번에 발사되는 총알 수.총알소모는 변수 관계없이 항상 1 
    [SerializeField] private float _spread; // 총알이 얼만큼 퍼져서 발싸되는지
    [SerializeField] private float _baseMOA; // MOA: 집탄률
    [SerializeField] private float _walkMOAModifier;
    [SerializeField] private float _runMOAModifier;
    [SerializeField] private float _sitMOAModifier;
    [SerializeField] private float _minVerticalRecoil;
    [SerializeField] private float _maxVerticalRecoil;
    [SerializeField] private float _minHorizontalRecoil;
    [SerializeField] private float _maxHorizontalRecoil;
    [SerializeField] private float _recoilDuration;
    [SerializeField] private float _sitRecoilModifer;
    [SerializeField] private AnimationCurve _recoilCurve;
    [SerializeField] GameObject _muzzleFlash;

    public int ShotAtOnce // 최소값 1
    {
        private set { _shotAtOnce = value < 1 ? 1 : value; }
        get { return _shotAtOnce; }
    }

    public float ShotMOA => _spread + CurrentMOA;
    public float Spread => _spread;
    public GameObject MuzzleFlash => _muzzleFlash;

    public float VerticalRecoilForce
    {
        get
        {
            float result = Random.Range(_minVerticalRecoil, _maxVerticalRecoil);
            if (MainScene.Instance.Player.IsSit)
                result *= _sitMOAModifier;
            return result;
        }
    }
    public float HorizontalRecoilForce
    {
        get
        {
            float result = Random.Range(_minHorizontalRecoil, _maxHorizontalRecoil);
            if (Random.Range(0, 2) == 0)
                result = -result;
            if (MainScene.Instance.Player.IsSit)
                result *= _sitMOAModifier;
            return result;
        }
    }
    public float RecoilDuration => _recoilDuration;
    public AnimationCurve RecoilCurve => _recoilCurve;
    public float CurrentMOA
    {
        get
        {
            var player = MainScene.Instance.Player;
            if (player.IsADS)
                return 0f;

            float result = _baseMOA;
            if (player.IsSit)
                result *= _sitMOAModifier;
            if (player.IsWalk)
                result *= _walkMOAModifier;
            if (player.IsRun)
                result *= _runMOAModifier;

            return result;
        }
    }
}
