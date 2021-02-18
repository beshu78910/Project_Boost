using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class Rocket : MonoBehaviour
{
     // AudioClip sound;
     Rigidbody rigidbody;

     private AudioSource audioSource;
     
     
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        


    }

    // Update is called once per frame
    void Update()
    {

        Thurst();
     Rotate();

     // audioSource.clip = sound;
     // sound = Resources.Load<AudioClip>("rrocket");
        
    }

 
      

void Rotate()
{
    rigidbody.freezeRotation = true;
    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) == false)
    {
        print("Rotating Left");
        transform.Rotate(Vector3.forward);
            
    } else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) == false)
    {
        print("Rotating Right");
        transform.Rotate(-Vector3.forward);
    }

}

    void Thurst()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Thurst");
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                
            } else if (!Input.GetKey(KeyCode.Space))
            {
                audioSource.Stop();
            }
        } 
        

    }

}
