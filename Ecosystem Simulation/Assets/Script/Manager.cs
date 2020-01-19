using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Manager : MonoBehaviour
{

    public int animal;
    public int food;
    public int numberOfNodeInGraph = 20;
    public GameObject cameraOne;
    public GameObject cameraTwo;
    public GameObject cameraThree;
    private int k;
    private List<int> list;
    private List<int> list2;


    AudioListener cameraOneAudioLis;
    public Window_Graph g;
    public Window_Graph g2;

    void Start()
    {
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        animal = PlayerPrefs.GetInt("animal", 0);
        food = PlayerPrefs.GetInt("food", 0);
        list = new List<int>();
        list2 = new List<int>();

        cameraPositionChange(0);
    }

    void Update()
    {
        animal = PlayerPrefs.GetInt("animal", 0);
        food = PlayerPrefs.GetInt("food", 0);
        k++;
        if(k%120 == 0)
        {
            list.Add(animal);
            list2.Add(food);
            if(PlayerPrefs.GetInt("CameraPosition") == 1)
            g.ShowGraph(list, numberOfNodeInGraph, (int _i) => "Day " + (_i + 1), (float _f) => "Rabbits " + Mathf.RoundToInt(_f));
            else if(PlayerPrefs.GetInt("CameraPosition") == 2)
            g2.ShowGraph(list2, numberOfNodeInGraph, (int _i) => "Day " + (_i + 1), (float _f) => "food " + Mathf.RoundToInt(_f));
            k = 0;
        }

        switchCamera();
    }
    //Change Camera Keyboard
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cameraPositionChange(0);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            cameraPositionChange(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            cameraPositionChange(2);
        }
    }

    //Camera Counter
    void cameraChangeCounter(int val)
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    //Camera change Logic
    void cameraPositionChange(int camPosition)
    {
        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        if (camPosition == 0)
        {
            cameraOne.SetActive(true);
            cameraOneAudioLis.enabled = true;

            cameraTwo.SetActive(false);
            cameraThree.SetActive(false);
        }
        else if (camPosition == 1)
        {
            cameraTwo.SetActive(true);

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);
            cameraThree.SetActive(false);
        }
        else
        {
            cameraThree.SetActive(true);

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);
            cameraTwo.SetActive(false);

        }
    }
}
