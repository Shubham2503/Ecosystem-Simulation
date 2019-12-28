using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_script : MonoBehaviour
{

    public AnimationControl anm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anm.SetAnimation("isWalking");
            
        }
        
    }
}
