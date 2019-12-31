using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Animal : MonoBehaviour
{

    public Vector3 destination;
    private UnityEngine.AI.NavMeshAgent myAgent;
    public float dist;

    void Start()
    {
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        destination = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10.0f, 10.0f));
        destination += transform.position;
    }


    void Update()
    {
        dist = Vector3.Distance(transform.position, destination);
        if (destination != null && Vector3.Distance(transform.position, destination) > 2f)
        {
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
