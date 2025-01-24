using UnityEngine;

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class RandomWordSelector : MonoBehaviour
{
    private string jsonFilePath = "Assets/Levels/word_sets.json"; // ���� � JSON �����

    private Dictionary<string, List<string>> wordSets;

    void Start()
    {
        LoadWordSets();
    }

    private void LoadWordSets()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            wordSets = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonContent);
        }
        else
        {
            Debug.LogError($"JSON ���� �� ������ �� ����: {jsonFilePath}");
        }
    }

    public List<string> GetRandomWordSet()
    {
        if (wordSets != null && wordSets.Count > 0)
        {
            // �������� ��������� ���� �� �������
            List<string> keys = new List<string>(wordSets.Keys);
            string randomKey = keys[Random.Range(0, keys.Count)];

            Debug.Log($"��������� ����: {randomKey}");
            return wordSets[randomKey];
        }
        else
        {
            Debug.LogError("������� ����� ��� �� ���������.");
            return null;
        }
    }
}

