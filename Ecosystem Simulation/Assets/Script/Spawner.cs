using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject terrain;
    public int numberOfObjects1; // number of objects to place
    public int numberOfObjects2; // number of objects to place
    private int currentObjects1; // number of placed objects
    private int currentObjects2; // number of placed objects
    public GameObject objectToPlace1; // GameObject to place
    public GameObject objectToPlace2; // GameObject to place
    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z

    private static GameObject obj;

    private int y = 0;

    void Start()
    {
        obj = objectToPlace2;
        // terrain size x
        terrainWidth = (int)200/2;
        // terrain size z
        terrainLength = (int)200/2;
        // terrain x position
        terrainPosX = (int)terrain.transform.position.x;
        // terrain z position
        terrainPosZ = (int)terrain.transform.position.z;

        spawnInitialPopulation(numberOfObjects1, objectToPlace1);
        PlayerPrefs.SetInt("food", numberOfObjects1);
        spawnInitialPopulation(numberOfObjects2, objectToPlace2);
        PlayerPrefs.SetInt("animal", numberOfObjects2);

    }
    void Update()
    {
        y++;
        if(y%20 == 0)
        {
            spawnInitialPopulation(1, objectToPlace1);
            int x = PlayerPrefs.GetInt("food", 0) + 1;
            PlayerPrefs.SetInt("food", x);
        }
    }

    void spawnInitialPopulation(int num,GameObject obj)
    {
        int curObj = 0;
        for(int i=0;i<num;i++)
        {
            // generate random x position
            int posx = Random.Range(-terrainPosX - terrainWidth, terrainPosX + terrainWidth);
            // generate random z position
            int posz = Random.Range(-terrainPosZ - terrainLength, terrainPosZ + terrainLength);
            // get the terrain height at the random position
            //float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));
            int posy = 1;
            // create new gameObject on random position
            GameObject newObject = (GameObject)Instantiate(obj, new Vector3(posx, posy, posz), Quaternion.identity);
            curObj += 1;
        }
    }

    public static void Spawn(float x,float z)
    {
        int k = PlayerPrefs.GetInt("animal", 0) + 1;
        PlayerPrefs.SetInt("animal", k);
        GameObject newObject = (GameObject)Instantiate(obj, new Vector3(x + 1, 3, z + 1), Quaternion.identity);
    }
}
