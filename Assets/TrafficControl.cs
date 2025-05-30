using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficControl : MonoBehaviour
{

    public Transform t1;
    public Transform t2;
    public Transform t3;
    public GameObject t1green;
    public GameObject t1red;
    public GameObject t2green;
    public GameObject t2red;
    public GameObject t3green;
    public GameObject t3red;
    public float stateTimer;
    public int state;

    public bool light1;
    public bool light2;
    public bool light3;

    // Start is called before the first frame update
    void Start()
    {
        // find all gameobjects
        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");
        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;

        // set initial timer and state
        stateTimer = 10.0f;
        SetState(1);
    }

    // Update is called once per frame
    void Update()
    {
        // update state timer
        stateTimer -= Time.deltaTime;

        // if state timer is 0, reset and swap state
        if (stateTimer < 0)
        {
            stateTimer = 10.0f;

            if (state == 1)
            {
                SetState(2);
            }
            else
            {
                SetState(1);
            }
        }

        /*Debug.Log("Light 1 - " + light1);
        Debug.Log("Light 2 - " + light2);
        Debug.Log("Light 3 - " + light3);*/
    }

    void SetState(int c)
    {
        // en/disable lights based on state
        state = c;
        if (c == 1)
        {
            t1green.SetActive(true);
            t1red.SetActive(false);
            t2green.SetActive(false);
            t2red.SetActive(true);
            t3green.SetActive(false);
            t3red.SetActive(true);

            light1 = false;
            light2 = true;
            light3 = true;
        }
        else
        {
            t1green.SetActive(false);
            t1red.SetActive(true);
            t2green.SetActive(true);
            t2red.SetActive(false);
            t3green.SetActive(true);
            t3red.SetActive(false);

            light1 = true;
            light2 = false;
            light3 = false;
        }
    }   
}
