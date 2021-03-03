using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
     // AudioClip sound;
     Rigidbody rigidbody;
     [SerializeField] float RotationSpeed = 100f;
     [SerializeField] float MainThurst = 100f;
     private bool controlEnable = true;
     

     enum State
     {
         Alive,
         Transcending,
         Dying
         
     };

     private State state = State.Alive;

     private AudioSource audioSource;
     
     
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        


    }
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadScene", 1f);
                break;
            default:
                controlEnable = false;
                print("Dead");
                state = State.Dying;
                audioSource.Stop();
                Invoke("LoadSceneN0", 2f);
                break;
            
        }
    }

     private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

     private void LoadSceneN0()
     {
         SceneManager.LoadScene(0);
     }

     // Update is called once per frame
    void Update()
    {
        if (state == State.Alive) 
        {
            Thurst();
            Rotate();

        }
       
     // audioSource.clip = sound;
     // sound = Resources.Load<AudioClip>("rrocket");
        
    }
    
      

void Rotate()
{
    rigidbody.freezeRotation = true;
   
    float RotationTime = RotationSpeed * Time.deltaTime;

    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) == false)
    {
        print("Rotating Left");
        
        transform.Rotate(Vector3.forward * RotationTime);
        
        
        
            
    } else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) == false)
    {
        print("Rotating Right");
        transform.Rotate(-Vector3.forward * RotationTime);
    }

    rigidbody.freezeRotation = false;

}



    void Thurst()
    {
        
        float ThurstSpeed = MainThurst * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            
            print("Thurst");
            rigidbody.AddRelativeForce(Vector3.up * ThurstSpeed);
            
            
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
