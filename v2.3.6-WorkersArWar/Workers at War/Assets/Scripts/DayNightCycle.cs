using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {


    private Vector3 rotation;
    private Vector3 position;

    private bool isDay = true ;
    public Color night;
    public Color day;
    public Color bgDay;
    Camera cam;
    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        rotation = new Vector3(0f, 0.1f, 0f);
        

        if (Time.time % 1 == 0)
        {
            if(isDay)
            {
                cam.GetComponent<Camera>().backgroundColor += Color.white / 2.0f * Time.deltaTime;
                gameObject.GetComponent<Light>().color += Color.white / 2.0F * Time.deltaTime;
            }
            else
            {
                cam.GetComponent<Camera>().backgroundColor -= Color.white / 2.0f * Time.deltaTime;
                gameObject.GetComponent<Light>().color -= Color.white / 2.0F * Time.deltaTime;
            }
            
        }
        if (Time.time % 60 == 0)
        {
            if (isDay)
            {
                
                isDay = false;
            }
            else
            {
                
                isDay = true;
            }

        }
    
        gameObject.transform.Rotate(rotation);

    }
}
