using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFrontControl : MonoBehaviour
{
    public bool stop = false;
    public bool isUnsafe = false;

    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        
    }

    void OnTriggerStay(Collider coll)
    {
        //Debug.Log(this.transform.parent.gameObject.name + " collided with " + coll.gameObject.name);

        // checks the pedestrian crossing for pedestrians
        if (coll.gameObject.tag == "Crossing")
        {
            isUnsafe = coll.gameObject.GetComponent<CrossingControl>().isPedestrian;

            /*if (isUnsafe)
            {
                Debug.Log("Can't drive! It's unsafe!");
            }
            else
            {
                Debug.Log("Should be safe to drive now!");
            }*/
        }
        
        if(coll.gameObject.name == "Boundary")
        {
            GameObject light = coll.transform.parent.gameObject;
            string lightName = light.name;
            light = light.transform.parent.gameObject;

            //Debug.Log(lightName);

            if(lightName == "TL1")
            {
                //Debug.Log("Traffic light variable L1 - " + light.gameObject.GetComponent<TrafficControl>().light1);
                isUnsafe = light.gameObject.GetComponent<TrafficControl>().light1;
            }
            else if(lightName == "TL2")
            {
                //Debug.Log("Traffic light variable L2 - " + light.gameObject.GetComponent<TrafficControl>().light2);
                isUnsafe = light.gameObject.GetComponent<TrafficControl>().light2;
            }
            else if(lightName == "TL3")
            {
                //Debug.Log("Traffic light variable L3 - " + light.gameObject.GetComponent<TrafficControl>().light3);
                isUnsafe = light.gameObject.GetComponent<TrafficControl>().light3;
            }

            //Debug.Log("Is unsafe? = " + isUnsafe);
        }

        // checks in front of the car for pedestrians
        if (coll.gameObject.tag == "Pedestrian" || coll.gameObject.tag == "Player")
        {
            stop = true;
            //Debug.Log("Collided with " + coll.gameObject.name);
        }
        else if (coll.gameObject.name == "Back")
        {
            stop = true;
            //Debug.Log("Car hit the back of another car, stopping!");
        }
        else if (coll.gameObject.name == "Front")
        {
            stop = false;
            //Debug.Log("Hit the front, keep moving");
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Pedestrian" || coll.gameObject.tag == "Player")
        {
            StartCoroutine(ExitDelay());
            stop = false;
            //Debug.Log(coll.gameObject.name + " left car boundary");
        }
        else if (coll.gameObject.name == "Back" || coll.gameObject.name == "Front")
        {
            StartCoroutine(ExitDelay());
            stop = false;
            //Debug.Log(coll.gameObject.name + " left car boundary");
        }

    }

    IEnumerator ExitDelay()
    {
        //Debug.Log("Waiting to be safe!");
        yield return new WaitForSeconds(1.0f);
    }
}
