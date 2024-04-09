using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatemask : MonoBehaviour
{
  
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKey("w"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
           

     

        }

        if (Input.GetKey("e"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
  

            


            



        }





    }
}
