using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class ElevenLabs : MonoBehaviour
{
    const string baseDir = @".\"; // Base Directory of output file
    const string baseURL = "https://api.elevenlabs.io/v1/text-to-speech/"; // Base URL of HTTP request
                                                                           //   public string API_KEY { get; } // Eleven Labs API key
    [SerializeField]
    private string _apiKey;

    public bool requesting = false;
      public ElevenLabs(string key) {
        _apiKey = key; 
    }
   

    // Requests MPEG file containing AI Voice saying the prompt and outputs the directory to said file
    public async Task<string> RequestAudio(string prompt, string voice, string fileName)
    {

        string url = baseURL + voice; // Concatenate Voice ID to end of URL
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Add("xi-api-key", _apiKey); // Add API Key header
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("audio/mpeg")); // Add accepted file extension header

        var data = new
        {
            text = prompt,
            model_id = "eleven_monolingual_v1",
            voice_settings = new
            {
                stability = 0.5f,
                similarity_boost = 0.5f
            }
        }; // Set-up Data

        // Convert Data to JSON
        string json = JsonConvert.SerializeObject(data);
        StringContent httpContent = new StringContent(json, System.Text.Encoding.Default, "application/json");

        // Request MPEG
        var response = await client.PostAsync(url, httpContent);
        requesting = false;

        // Output Response to local MPEG file in the respective directory
        if (response != null)
        {

            int fileNameExtension = 0;
            int retries = 0;
            bool fileNameValid = false;

            while (!fileNameValid)
            {
                try
                {
                    // Stream response as binary data into a file
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (FileStream fileStream = File.Create(@".\Audio\" + fileName + fileNameExtension.ToString() + ".mp3"))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    fileNameValid = true;

                }
                catch (Exception ex)
                {
                    retries++;
                    if (retries >= 50)
                        break;
                    fileNameExtension++;
                }
            }

            if (fileNameValid)
                return @".\" + fileName + fileNameExtension.ToString() + ".mp3";
        }

        // Return Directory to file
        return null;

    }


}
