using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
      /*  [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;*/
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "you are here to only understand context, just by whatever user talking about,and if you are not sure about emotion, just choose neutral as animation, you only need to choose animation from following animation list, base on context  {neutral,  sad , laugh,  happy,  shocked, sleepy , angry , dirty,  blush, Agree } and answer in one words";
        private int count = 0;
        public AudioSource _audioSource;
        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                SendReply(inputField.text.Length);


            });
        }

        private async void AppendMessage(int length)
        {


            if(length > 10)
            {
              AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>("Assets/enormal.mp3");
                await handle.Task;
                if(handle.Status == AsyncOperationStatus.Succeeded)
                {
                    AudioClip clip = handle.Result;
                    _audioSource.clip = clip;
                    _audioSource.Play();
                }




            }




           /* scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;*/
            
        }







        private async void SendReply(int length)
        {

            
          //  Debug.Log(length);

            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
                
            };
             
          //  AppendMessage(length);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0125",
                Messages = messages
                
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                
               message.Content = message.Content.Trim();
                Debug.Log(message.Content);
               


            messages.Add(message);
               AppendMessage(length);

               if(message.Content == "sad")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("issad",true);
                   
                }  else if(message.Content == "neutral" && count%4 == 0)
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("islaugh", true);
                    count++;

                } 
               else if (message.Content == "neutral" && count % 4 != 0)
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("isangry", true);
                    count++;
                }
                else if (message.Content == "laugh")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("islaugh", true);

                }
                else if (message.Content == "happy")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("ishappy", true);

                }
                else if (message.Content == "shocked")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("isshocked", true);

                }
                else if (message.Content == "sleepy")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("istired", true);

                }
                else if (message.Content == "angry")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("isangry2", true);

                }
                else if (message.Content == "dirty")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("isdirty", true);

                }
                else if (message.Content == "blush")
                {
                    var tc = GameObject.Find("64ef1bfa1db75f90dcfb105b");
                    var animator = tc.transform.GetComponent<Animator>();
                    animator.SetBool("isblush", true);

                }
               
                   }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }


      




    }
}
