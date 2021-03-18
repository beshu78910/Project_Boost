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
     [SerializeField] AudioClip MainEngine;
     [SerializeField] AudioClip DeathEngine;
     [SerializeField] AudioClip LevelSwitching;
     
     [SerializeField] ParticleSystem RocketParticle;
     [SerializeField] ParticleSystem successParticle;
     [SerializeField] ParticleSystem deathParticle;

     enum State
     {
         Alive,
         Transcending,
         Dying,
         NoCollision

     };

     private State state = State.Alive;

     private AudioSource audioSource;
     private ParticleSystem particleSource;
     
     
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        


    }
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || state == State.NoCollision)
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
              
                audioSource.PlayOneShot(LevelSwitching);
                successParticle.Play();
                Invoke("LoadScene", 1f);
              
                break;
            
            default:
              startDeath();
               
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

     private void delayDeathSound()
     {
         audioSource.PlayOneShot(LevelSwitching);
     }

     private void startDeath()
     {
         controlEnable = false;
         state = State.Dying;
         audioSource.Stop();
         audioSource.PlayOneShot(DeathEngine);
         RocketParticle.Stop();
         deathParticle.Play();
         Invoke("delayDeathSound", 2f);
         Invoke("LoadSceneN0", 3f);
         
     }

     
     
     
     // Update is called once per frame
    void Update()
    {
        if (state == State.Alive || state == State.NoCollision) 
        {
            ResponeToThurst();
            Rotate();
          

        }

        if (state == State.NoCollision && Input.GetKey(KeyCode.C))
        {
            state = State.Alive;
        } else if (Input.GetKey(KeyCode.C))
        {
            state = State.NoCollision;
        }

       
        ManualLevelSwitch();
   
       
     // audioSource.clip = sound;
     // sound = Resources.Load<AudioClip>("rrocket");
        
    }

    void ManualLevelSwitch()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadSceneN0();
        }
    }

    // void CollisionToggle(Collision State)
    // {
    //     if (Input.GetKey(KeyCode.C))
    //     {
    //         State.gameObject.tag = "Friendly";
    //     }
    // }

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



    void ResponeToThurst()
    {
        
        
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThurst();
            RocketParticle.Play();
        


        } 
        else 
        {
            audioSource.Stop();
            RocketParticle.Stop();
           
        }
        
        
    }

    void ApplyThurst()
    {
        float ThurstSpeed = MainThurst * Time.deltaTime;
        rigidbody.AddRelativeForce(Vector3.up * ThurstSpeed);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine);
                
        }
       
    } 

}
