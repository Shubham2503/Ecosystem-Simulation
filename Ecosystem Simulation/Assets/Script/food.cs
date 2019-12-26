using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    private object collisionInfo;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "animal")
        {
            int k = PlayerPrefs.GetInt("food", 0) - 1;
            PlayerPrefs.SetInt("food", k);
            Destroy(gameObject);
        }
    }


    /*void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "animal")
        {
            int k = PlayerPrefs.GetInt("food", 0) - 1;
            PlayerPrefs.SetInt("food", k);
            Destroy(gameObject);
        }
    }
    */
}
