using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiotaker : MonoBehaviour
{
   
    public AudioSource _audioSource;
  
    


    private void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            var audioclip = Resources.Load<AudioClip>("enormal");
            _audioSource.clip = audioclip;
            _audioSource.Play();
           
            

        }

    }


}
