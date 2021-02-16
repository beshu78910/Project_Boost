using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
     Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
     ProcessInput();
        
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Thurst");
            rigidbody.AddRelativeForce(Vector3.up);
            
            
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) == false)
        {
            print("Rotating Left");
            rigidbody.AddRelativeForce(Vector3.left);
            
        } else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) == false)
        {
            print("Rotating Right");
            rigidbody.AddRelativeForce(Vector3.right);
        }
        

    }
}
