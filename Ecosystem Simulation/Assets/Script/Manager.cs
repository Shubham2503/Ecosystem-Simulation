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
    private int k;
    private List<int> list;
    private List<int> list2;


    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;
    public Window_Graph g;
    public Window_Graph g2;

    void Start()
    {
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();
        animal = PlayerPrefs.GetInt("animal", 0);
        food = PlayerPrefs.GetInt("food", 0);
        list = new List<int>();
        list2 = new List<int>();

        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
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
            g.ShowGraph(list, numberOfNodeInGraph, (int _i) => "Day " + (_i + 1), (float _f) => "Rabbits " + Mathf.RoundToInt(_f));
            g2.ShowGraph(list2, numberOfNodeInGraph, (int _i) => "Day " + (_i + 1), (float _f) => "food " + Mathf.RoundToInt(_f));
            k = 0;
        }

        switchCamera();
    }
    //Change Camera Keyboard
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            cameraChangeCounter();
        }
    }

    //Camera Counter
    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    //Camera change Logic
    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        //Set camera position 1
        if (camPosition == 0)
        {
            cameraOne.SetActive(true);
            cameraOneAudioLis.enabled = true;

            cameraTwoAudioLis.enabled = false;
            cameraTwo.SetActive(false);
        }

        //Set camera position 2
        if (camPosition == 1)
        {
            cameraTwo.SetActive(true);
            cameraTwoAudioLis.enabled = true;

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);
        }

    }
}
