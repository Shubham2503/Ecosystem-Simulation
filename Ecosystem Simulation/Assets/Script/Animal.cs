using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Animal : MonoBehaviour
{
    public double hunger = 0;

    //values to interact
    public Vector3 pos;
    public bool foodFound = false;
    public bool goingTowardsMate = false;



    public LayerMask groundLayer;
    public LayerMask animalLayer;
    private int senceRadius = 10;
    public GameObject obj;
    public double reproduceErge = 0;
    public double age;   // change to public later



    public GameObject nearestPlanet1 = null;
    public GameObject nearestPlanet2 = null;


    //Settings:
    private float criticleVal = 100;
    private int finalVal = 500;
    private float yAngle = 1f;
    private NavMeshAgent myAgent;
    private bool exit = false;
    private int reproduceErgeCriticleval = 150;
    private float localScaleVal = 0.1f;
    private double size = 1f;


    public Collider[] objs;
    public Collider[] objs2;


    

    void Start()
    {
        myAgent = GetComponent <NavMeshAgent> ();
        age = Random.Range(0, 20);
    }

    void Update()
    {
        age += 0.1;
        hunger += 0.5;
        reproduceErge += 0.5;


        if (hunger >= finalVal && false)
        {
            kill();
        }
        else if(hunger >= criticleVal + 150 && !foodFound)
        {
            //FindFood();
        }
        else if (hunger >= criticleVal && !foodFound && !goingTowardsMate)
        {
            //FindFood();
        }
        //else if(age > 70 && reproduceErge > reproduceErgeCriticleval && !goingTowardsMate)
        else if(age > 20  && !goingTowardsMate)
        {
            FindToReprodce();
        }
        else////////////////////////////////change///////////////////////////////.............................................................................
        {
            //Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            //transform.Translate(position, Space.Self);
        }

        if(age > 50)
        {
            if(size <= 1.5f)
            {
                transform.localScale += new Vector3(localScaleVal, localScaleVal, localScaleVal);
                size += 0.1f;
            }
        }


        if(age > 70 && reproduceErge > reproduceErgeCriticleval && hunger <= 250)
        {
            //FindToReprodce();
        }


        if (nearestPlanet2 != null && goingTowardsMate)
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestPlanet2.transform.position, 1.0f * Time.deltaTime);

        }


        if (objs.Length > 0)
        {
            if (objs[0] == null)
            {
                foodFound = false;
            }
            else
            {
                foodFound = true;
            }

        }
        else
        {
            foodFound = false;
        }

    }

    /*void Move()
    {
        Vector3 position = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-1.0f, 1.0f));
        transform.Translate(position, Space.Self);
    }*/

    void kill()
    {
        int k = PlayerPrefs.GetInt("animal", 0) - 1;
        PlayerPrefs.SetInt("animal", k);
        Destroy(gameObject);
    }

    void FindFood()
    {
        /*RaycastHit hit;
        if (UnityEngine.Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, senceRadius, groundLayer))
        {
            myAgent.SetDestination(hit.point);
        }
        else
        {
            if (rotationDir > 5)
                transform.Rotate(0, yAngle, 0, Space.Self);
            else
                transform.Rotate(0, -yAngle, 0, Space.Self);
        }*/

        //Collider[] objs
        objs = Physics.OverlapSphere(transform.position + Vector3.up, senceRadius, groundLayer);
        pos = transform.position;
        if (objs.Length > 0)
        {
            pos = objs[0].transform.position;
        }
        myAgent.SetDestination(pos);
    }

    void FindToReprodce()
    {
        /*RaycastHit hit;
        if (UnityEngine.Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, senceRadius, animalLayer))
        {
            exit = true;
            myAgent.SetDestination(hit.point);
        }
        else
        {
            if (rotationDir > 5)
                transform.Rotate(0, yAngle, 0, Space.Self);
            else
                transform.Rotate(0, -yAngle, 0, Space.Self);
        }
        */

        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);


        objs2 = Physics.OverlapSphere(transform.position, 100, animalLayer);

        if (objs2.Length > 1)
        {
            float nearestDistance1 = float.MaxValue, nearestDistance2 = float.MaxValue;
            float distance;

            foreach (Collider planet in objs2)
            {
                distance = dist(transform.position, planet.transform.position);
                Debug.Log(distance);
                if (distance < nearestDistance1)
                {

                    nearestDistance2 = nearestDistance1;
                    nearestDistance1 = distance;
                    nearestPlanet2 = nearestPlanet1;
                    nearestPlanet1 = planet.gameObject;

                }
                else if (distance < nearestDistance2 && distance != nearestDistance1)
                {
                    nearestDistance2 = distance;
                    nearestPlanet2 = planet.gameObject;
                }
            }

            exit = true;
            if (nearestPlanet2 != null)
            {
                pos = nearestPlanet2.transform.position;
                goingTowardsMate = true;
                //myAgent.SetDestination(pos);
            }
        }
    }

    /*void OnTriggerExit(Collider other)
    {
        if (other.tag == "animal")
        {
            ////////////////////////////////////////// REPRODUCE //////////////////////////////////////////////////
            if (exit && reproduceErge > reproduceErgeCriticleval && hunger <= 250) 
            {
                Spawner.Spawn(transform.position.x,transform.position.z);
                reproduceErge = 0;
                exit = false;
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "food")
        {
            hunger -= 100;
        }
    }

    float dist(Vector3 a,Vector3 b)
    {
        float distance;
        distance = ( ((a.x - b.x) * (a.x - b.x)) + ((a.y - b.y) * (a.y - b.y)) + ((a.z - b.z) * (a.z - b.z)) );
        return distance;
    }
}
