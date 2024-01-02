using UnityEngine;

public class JEH_Aim : MonoBehaviour
{


    public GameObject TestPrefab;
    public GameObject TestPrefab22;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

             int layerMask = 1 << LayerMask.NameToLayer("Water"); 

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) 
            {
                Debug.Log(hit.collider.name);
                Debug.DrawRay(ray.origin, ray.direction * 500, Color.red, 1);

                GameObject wallPiece = Instantiate(TestPrefab);
                wallPiece.transform.position = hit.point; // 충돌위치
                wallPiece.transform.forward = hit.normal; // 노멀방향

                GameObject dddd = Instantiate(TestPrefab22);
                dddd.transform.position = hit.point; 
                dddd.transform.forward = hit.normal;





            }
            else
            {
               // Debug.Log("충돌체가 없거나 원하는 충돌체가 아닙니다.");
            }
        }


    }

}


/*
 * 
 * 
 * 
 *       // 만일 레이에 부딪힌 대상의 레이어가 "Enemy"라면 데미지 함수를 실행한다.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
 * 
 * 
 * 
                 int layerMask = 1 << LayerMask.NameToLayer("Water"); 
   if (Physics.Raycast(ray, out hit, layerMask)) // 충돌체의 레이어가 Water일때만 true됨. 해당 레이어가 아니면 뒤로 통과해버림

Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))  // Max Distance 안정하면 화면 끝까지 쏴지는듯. 충돌체 있으면 true
            {
                Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);

                Debug.Log(hit.collider.name);
            }
            else
            {
                Debug.Log("바깥이다!!");
            }


 */