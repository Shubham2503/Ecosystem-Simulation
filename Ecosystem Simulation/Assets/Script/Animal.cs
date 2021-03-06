﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Animal : MonoBehaviour
{
    public double hunger = 0;

    public Vector3 destination;

    public LayerMask groundLayer;
    public LayerMask animalLayer;
    public int senceRadius = 100;
    public GameObject obj;
    public double reproduceErge = 0;
    public double age;


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

    private int rotationDir;

    void Start()
    {
        myAgent = GetComponent <NavMeshAgent> ();
        rotationDir = Random.Range(0, 10);
        Debug.Log(rotationDir);
        age = Random.Range(0, 20);
    }

    void Update()
    {
        age += 0.1;
        hunger += 0.5;
        reproduceErge += 0.5;


        if (hunger >= finalVal)
        {
            kill();
        }
        else if (hunger >= criticleVal && hunger < finalVal)
        {
            FindFood();
        }
        else
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
            FindToReprodce();
        }

    }

    void Move()
    {
        Vector3 position = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-1.0f, 1.0f));
        transform.Translate(position, Space.Self);
    }

    void kill()
    {
        int k = PlayerPrefs.GetInt("animal", 0) - 1;
        PlayerPrefs.SetInt("animal", k);
        Destroy(gameObject);
    }

    void FindFood()
    {
        objs = Physics.OverlapSphere(transform.position + Vector3.up, 10, groundLayer);
        if (objs.Length > 0)
        {
            destination = objs[0].transform.position;
            myAgent.SetDestination(destination);
            Vector3 targetDir = destination - transform.position;
            Debug.DrawRay(transform.position, targetDir, Color.red);
        }
    }

    void FindToReprodce()
    {
        objs2 = Physics.OverlapSphere(transform.position + Vector3.up, 10, animalLayer);
        if (objs2.Length > 1)
        {
            exit = true;
            destination = objs2[1].transform.position;
            myAgent.SetDestination(destination);

            Vector3 targetDir = destination - transform.position;
            Debug.DrawRay(transform.position, targetDir, Color.blue);
        }
    }

    void OnTriggerExit(Collider other)
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "food")
        {
            hunger -= 100;
        }
    }
}
