using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBackControl : MonoBehaviour
{
    public bool stop = false;
    public bool isUnsafe = false;

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
        if (coll.gameObject.tag == "Pedestrian" || coll.gameObject.tag == "Player")
        {
            // if the back of the car is past a person just continue
            stop = false;
            Debug.Log("Collided with " + coll.gameObject.name);
        }
        else if (coll.gameObject.name == "Back")
        {
            stop = false;
            Debug.Log("Car back hit the back of another car, keep moving");
        }
        else if (coll.gameObject.name == "Front")
        {
            stop = false;
            Debug.Log("Hit the front, keep moving");
        }
    }

    void OnTriggerStay(Collider coll)
    {
        // checks the pedestrian crossing for pedestrians
        if (coll.gameObject.tag == "Crossing")
        {
            isUnsafe = coll.gameObject.GetComponent<CrossingControl>().isPedestrian;

            // if the back of the car is on the crossing and its unsafe, just keep going because you're already on the crossing!
            if (isUnsafe)
            {
                isUnsafe = false;
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Pedestrian" || coll.gameObject.tag == "Player")
        {
            StartCoroutine(ExitDelay());
            stop = false;
            Debug.Log(coll.gameObject.name + " left car boundary");
        }
        else if (coll.gameObject.name == "Back" || coll.gameObject.name == "Front")
        {
            StartCoroutine(ExitDelay());
            stop = false;
            Debug.Log(coll.gameObject.name + " left car boundary");
        }

    }

    IEnumerator ExitDelay()
    {
        //Debug.Log("Waiting to be safe!");
        yield return new WaitForSeconds(1.0f);
    }
}
