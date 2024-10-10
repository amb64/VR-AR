using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarRoute : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;
    public float dist = 0;
    public Rigidbody rb;
    public bool go = false;
    public float initialDelay;
    public Vector3 velocity;
    public bool stop = false;

    public GameObject crossing;
    public bool isUnsafe = false;

    public GameObject frontCar;
    public GameObject backCar;
    public CarFrontControl front;
    public CarBackControl back;

    // Start is called before the first frame update
    void Start()
    {
        wps = new List<Transform>();
        GameObject wp;

        rb = GetComponent<Rigidbody>();

        front = frontCar.GetComponent<CarFrontControl>();
        back = backCar.GetComponent<CarBackControl>();

        // finds waypoints
        wp = GameObject.Find("RP1");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP2");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP3");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP4");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP5");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP6");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP7");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP8");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP9");
        wps.Add(wp.transform);
        wp = GameObject.Find("RP10");
        wps.Add(wp.transform);

        //safe = crossing.GetComponent<CrossingControl>().isPedestrian;
        
        //SetRoute();

        // sets a delay so that all cars move at different times
        initialDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // counts down the delay and sets a route when ready
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                //Debug.Log("Let's go!! " + gameObject.name);
                SetRoute();
            }
            else return;
        }

        if (back.isUnsafe || front.isUnsafe)
        {
            isUnsafe = true;
        }
        else
        {
            isUnsafe = false;
        }

        if (back.stop || front.stop)
        {
            stop = true;
        }
        else
        {
            stop = false;
        }

        if (!stop && !isUnsafe)
        {
            // calculate the distance to the next target waypoint
            Vector3 displacement = route[targetWP].position - transform.position;
            displacement.y = 0;
            dist = displacement.magnitude;

            // check when the pedestrian has reached a waypoint, and calculate a new route when the current one has finished
            if (dist < 0.1f && targetWP > 0)
            {
                targetWP++;

                if (targetWP >= route.Count)
                {
                    //Debug.Log("New route! " + gameObject.name);
                    StartCoroutine(RouteDelay());
                    SetRoute();
                    return;
                }
            }

            //calculate velocity for this frame
            velocity = displacement;
            velocity.Normalize();
            velocity *= 10.0f;

            //apply velocity
            Vector3 newPosition = transform.position;
            newPosition += velocity * Time.deltaTime;
            rb.MovePosition(newPosition);

            //align to velocity
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
            10.0f * Time.deltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(desiredForward);
            rb.MoveRotation(rotation);
        }
        else
        {
            //Debug.Log("Cant move");
        }
        
    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 5);

        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[2],wps[5] };
        else if (routeNumber == 1) route = new List<Transform> { wps[4], wps[3],wps[1] };
        else if (routeNumber == 2) route = new List<Transform> { wps[0], wps[2],wps[3], wps[6] };
        else if (routeNumber == 3) route = new List<Transform> { wps[4], wps[3],wps[6] };
        else if (routeNumber == 4) route = new List<Transform> { wps[7], wps[8],wps[9], wps[5] };

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.5f, route[0].position.z);
        targetWP = 1;

    }

    /*void OnTriggerEnter(Collider coll)
    {
        // checks in front of the car for pedestrians
        if (coll.gameObject.tag == "Pedestrian" || coll.gameObject.tag == "Player") 
        {
            stop = true;
            Debug.Log("Collided with " + coll.gameObject.name);
        }
        else if (coll.gameObject.name == "Back")
        {
            stop = true;
            Debug.Log("Car hit the back of another car, stopping!");
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

            /*if (isUnsafe)
            {
                Debug.Log("Can't drive! It's unsafe!");
            }
            else
            {
                Debug.Log("Should be safe to drive now!");
            }
        }
    }*/

    /*void OnTriggerExit(Collider coll)
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

    }*/

    /*void OnCollisionEnter(Collision coll)
    {
        // NEED A 2ND COLLIDER THAT IS NOT A TRIGGER FOR THIS TO WORK - maybe make one thats for the front and back for car collision, and just the front for pedestrian collision?
        if (coll.gameObject.tag == "Car")
        {
            stop = true;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Car")
        {
            StartCoroutine(ExitDelay());
            stop = false;
        }
    }*/

    /*IEnumerator ExitDelay()
    {
        //Debug.Log("Waiting to be safe!");
        yield return new WaitForSeconds(1.0f);
    }*/

    IEnumerator RouteDelay()
    {
        float delay = Random.Range(0.0f, 5.0f);
        yield return new WaitForSeconds(delay);
    }
}
