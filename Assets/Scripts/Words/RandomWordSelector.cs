using UnityEngine;

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Events;

public class RandomWordSelector : MonoBehaviour
{
    private readonly string PATH = Path.Combine(Application.streamingAssetsPath, "word_sets.json");

    private Dictionary<string, List<string>> wordSets;

    public UnityAction OnWordsLoadFinished;

    void Start()
    {
        StartCoroutine(LoadWordSets(PATH));
    }

    IEnumerator LoadWordSets(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    wordSets = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(webRequest.downloadHandler.text);
                    OnWordsLoadFinished?.Invoke();

                    Debug.Log("Received Json: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public List<string> GetRandomWordSet()
    {
        if (wordSets != null && wordSets.Count > 0)
        {
            List<string> keys = new List<string>(wordSets.Keys);
            string randomKey = keys[Random.Range(0, keys.Count)];

            Debug.Log($"Selected theme: {randomKey}");
            return wordSets[randomKey];
        }
        else
        {
            Debug.LogError("Dictionaries are empty or are not loaded yet.");
            return null;
        }
    }
}

