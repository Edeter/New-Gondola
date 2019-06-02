using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
  //  [SerializeField] LayerMask mask;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 destiny;
    [SerializeField] float v0x;
    [SerializeField] float maxdistance;
    bool isfishing= false;
    
      void OnEnable() {
     GameControler.ic +=   Go;

    agent = GetComponent<NavMeshAgent>();
    if (!prefab)
    {
        Debug.Log("missing prefab");
    }
    }

    void OnDisable() {
     GameControler.ic -=   Go;

    }

    void Go()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Debug.Log("1");
        if (Physics.Raycast(r,out hit))
        {
            switch( hit.collider.gameObject.tag)
            {
                case "land":
                agent.SetDestination(hit.point);
                Debug.DrawLine(gameObject.transform.position,hit.point,Color.black);
                break;

                case "water":
                if (Vector3.Magnitude(gameObject.transform.position - hit.point) < maxdistance)
                {
                    Fish(hit.point);
                }
                
                break;
        }
    }

    }
    void Fish(Vector3 where)
    {
        isfishing = true;
        GameObject tak = Instantiate(prefab,new Vector3(where.x,7.45f,where.z),Quaternion.identity);
        IEnumerator cour = throwing(tak);
        StartCoroutine(cour);
        
        
    }

    void Throw(Rigidbody rb,Vector3 dest)
    {
       /*  float z = Vector3.Magnitude( rb.transform.position - dest);
        float v = (z * Physics.gravity.y)/Mathf.Sin(Mathf.PI/2);
        Vector3 vel= new Vector3(0,Mathf.Sin(Mathf.PI/4),v*Mathf.Cos(Mathf.PI/4));
        //vel = Quaternion.Euler(0,Mathf.Atan(( rb.transform.position - dest).x/( rb.transform.position - dest).z),0) * vel;
        rb.velocity = vel;*/
        Vector3 pom = new Vector3(rb.transform.position.x- dest.x,0,rb.position.z-dest.z);
        float z = pom.magnitude;
        float t= z/v0x;
        float v0y= (t * -Physics.gravity.y)/2;
        Vector3 v = Quaternion.Euler(0,Vector3.SignedAngle(new Vector3(1,0,0),pom,Vector3.up),0) * new Vector3(v0x,v0y,0);
        v.z = -v.z;
        v.x = -v.x;
        rb.AddForce(v,ForceMode.VelocityChange);
    }

    IEnumerator throwing(GameObject obj)
    {
        Rigidbody ryb = obj.GetComponent<Rigidbody>();
        yield return new WaitForSeconds(3);

        ryb.AddForce(new Vector3(0,10,0),ForceMode.VelocityChange);
        Debug.Log("11111");

        yield return new WaitForSeconds(1f);
           ryb.velocity = new Vector3(0,0,0);
            Throw(ryb,destiny);
        
        
    }

}
