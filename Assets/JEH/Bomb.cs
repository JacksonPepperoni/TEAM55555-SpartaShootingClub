using UnityEngine;

public class Bomb : Weapon
{

    public float _speed = 5000;
    // 몇초후에 자동으로 폭발
    // 닿자마자 폭발. 안닿으면 몇초후에 자동으로 폭발

    private void OnEnable()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (_currentShotDelay <= 0)
                Throw();
        }

        _currentShotDelay -= Time.deltaTime;

    }
    void Throw()
    {
        if (_currentCartridge <= 0)
            return;

        _currentCartridge--;
        _currentShotDelay = _delayBetweenShots;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject rig = Instantiate(_projectilePrefab);
        rig.transform.position = gameObject.transform.position;

        rig.GetComponent<Rigidbody>().AddForce(ray.direction * _speed);







    }






}
