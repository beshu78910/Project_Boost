using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVec;

    [Range(0, 1)] [SerializeField] float movementFac;
    private float period = 2f;
    
    Vector3 startingPos;
    GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
       

    }

    // Update is called once per frame
    void Update()
    {
        float cyles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSineWave = Mathf.Sin(cyles * tau);
        
        Vector3 offset = movementVec * movementFac;
        transform.position = startingPos + offset;
        movementFac = rawSineWave / 2f + 0.5f;


    }



}
