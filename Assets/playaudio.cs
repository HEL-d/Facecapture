using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class playaudio : MonoBehaviour
{
    public UnityEngine.UI.Button sendButton;
    public InputField nputField;
    public ElevenlabsAPI tts;

    void Start()
    {
        // Add the PlayClip handler to the ElevenLabsAPI script
     //   tts.AudioReceived.AddListener(PlayClip);

        // Add the Button's onClick handler 
        sendButton.onClick.AddListener(() => {
            tts.GetAudio(nputField.text);
            nputField.text = "";
           
        });
    }
   /* public void PlayClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }*/



    // Update is called once per frame

}
