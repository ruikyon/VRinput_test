using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform child;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("return"))
        {
            Debug.Log("-----enter-----");
            Debug.Log("mypos: " + transform.position);
            Debug.Log("chpos: " + child.position);
            Debug.Log("lcpos: " + child.localPosition);

            Debug.Log("-----move-----");

            transform.position += Vector3.up;

            Debug.Log("mypos: " + transform.position);
            Debug.Log("chpos: " + child.position);
            Debug.Log("lcpos: " + child.localPosition);
        }
    }
}
