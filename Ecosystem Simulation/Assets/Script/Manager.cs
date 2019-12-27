using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Manager : MonoBehaviour
{

    public int animal;
    public int food;

    private int k;
    public List<int> list;
    public List<int> list2;

    public int numberOfNodeInGraph = 20;

    public Window_Graph g;
    public Window_Graph g2;

    void Start()
    {
        animal = PlayerPrefs.GetInt("animal", 0);
        food = PlayerPrefs.GetInt("food", 0);
        list = new List<int>();
        list2 = new List<int>();
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

    }
}
