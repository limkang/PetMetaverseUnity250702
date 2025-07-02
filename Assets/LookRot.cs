using UnityEngine;

public class LookRot : MonoBehaviour
{


    public GameObject target;

    void Update()
    {
            //Vector3 lookPos = target.transform.position;
            //lookPos.y = transform.position.y; // y값 고정(수평만)
            transform.LookAt(target.transform.position);

        if(Vector3.Distance(transform.position, target.transform.position)  > 2f)

       this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.5f * Time.deltaTime);
    }


}
