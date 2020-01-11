using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Animal : MonoBehaviour
{

    public Vector3 destination;
    private UnityEngine.AI.NavMeshAgent myAgent;
    public float dist;
    public float angle;
    public bool x;
    void Start()
    {
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        destination = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10.0f, 10.0f));
        destination += transform.position;
    }


    void Update()
    {

        //to get angle...
        Vector3 targetDir = destination - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);

        dist = Vector3.Distance(transform.position, destination);
        Debug.DrawRay(transform.position, targetDir, Color.red);


        if (angle > 2 && Vector3.Distance(transform.position, destination) > 10f)
        {
            x = true;
            myAgent.isStopped = true;
            myAgent.transform.rotation = Quaternion.Slerp(myAgent.transform.rotation, Quaternion.LookRotation(targetDir), 1f * Time.deltaTime);
            myAgent.transform.Translate(0, 0, Time.deltaTime * 3.0f);
        }
        else
        {
            x = false;
            if (destination != null && Vector3.Distance(transform.position, destination) > 1f)
            {
                if (myAgent.isStopped == true)
                { 
                    myAgent.ResetPath();
                    myAgent.isStopped = false;
                }
                //transform.LookAt(destination);
                //transform.Translate(Vector3.forward * Time.deltaTime * 5.0f);
                myAgent.SetDestination(destination);
            }
            else
            {

                destination = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10.0f, 10.0f));
                destination += transform.position;
                //transform.Translate(position, Space.Self);
            }
        }
    }
}
