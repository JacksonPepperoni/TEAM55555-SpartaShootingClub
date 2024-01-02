using UnityEngine;
using UnityEngine.InputSystem.HID;

public class JEH_Aim : MonoBehaviour
{

    [SerializeField] private GameObject TestPrefab;
    [SerializeField] private GameObject TestPrefab22;

    [SerializeField] private GameObject rigTest;

    private int _layerMask;


    private void Awake()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Water"); // Water 레이어만 잡힘
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                //  Debug.DrawRay(ray.origin, ray.direction * 500, Color.red, 1);

                GameObject wallPiece = Instantiate(TestPrefab);
                wallPiece.transform.position = hit.point;
                wallPiece.transform.forward = hit.normal;

                GameObject dddd = Instantiate(TestPrefab22);
                dddd.transform.position = hit.point;
                dddd.transform.forward = hit.normal;


                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("바닥");
                }
                else if (hit.collider.CompareTag("Wall"))
                {
                    Debug.Log("벽");
                }

            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            GameObject rig = Instantiate(rigTest);
            rig.transform.position = gameObject.transform.position;
            rig.transform.forward = ray.direction;

        }

    }
}