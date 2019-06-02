using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
   Material m;
   Vector2 offs;

   [SerializeField] float slow = 1000;
    // Start is called before the first frame update
    void Start()
    {
        m  = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offs= m.mainTextureOffset;
        offs += new Vector2(Mathf.Sin(Time.time)/slow,Mathf.Cos(Time.time)/slow);
       m.mainTextureOffset = offs;
    }
}
