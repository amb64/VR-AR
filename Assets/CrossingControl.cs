using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingControl : MonoBehaviour
{
    public bool isPedestrian = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Pedestrian" || coll.gameObject.tag == "Player")
        {
            isPedestrian = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Pedestrian" || coll.gameObject.tag == "Player")
        {
            StartCoroutine(ExitDelay());
            isPedestrian = false;
        }
    }

    IEnumerator ExitDelay()
    {
        Debug.Log("Waiting to be safe!");
        yield return new WaitForSeconds(2.0f);
    }
}
