using UnityEngine;

public class JEH_Aim : MonoBehaviour
{


    public GameObject TestPrefab;


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
                Debug.DrawRay(ray.origin, ray.direction * 200, Color.red, 1);


                GameObject bullet = Instantiate(TestPrefab);
                bullet.transform.position = hit.point; //�浹��ġ
                bullet.transform.forward = hit.normal; // �浹 ��ֹ���


            }
            else
            {
                Debug.Log("�浹ü�� ���ų� ���ϴ� �浹ü�� �ƴմϴ�.");
            }
        }


    }

}


/*
 * 
 * 
 * 
 *       // ���� ���̿� �ε��� ����� ���̾ "Enemy"��� ������ �Լ��� �����Ѵ�.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
 * 
 * 
 * 
                 int layerMask = 1 << LayerMask.NameToLayer("Water"); 
   if (Physics.Raycast(ray, out hit, layerMask)) // �浹ü�� ���̾ Water�϶��� true��. �ش� ���̾ �ƴϸ� �ڷ� ����ع���

Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))  // Max Distance �����ϸ� ȭ�� ������ �����µ�. �浹ü ������ true
            {
                Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);

                Debug.Log(hit.collider.name);
            }
            else
            {
                Debug.Log("�ٱ��̴�!!");
            }


 */