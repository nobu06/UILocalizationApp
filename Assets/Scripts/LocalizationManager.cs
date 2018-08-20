using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 


public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance; // doing Singleton

    private Dictionary<string, string> localizedText;
    private bool isReady = false;       // indicates whether the program finished loading the localized text 


	private void Awake()
	{
        if (instance == null)       
        {
            instance = this;
        }
        else if (instance != this)      // if already exists, then destroy the present  gameObject
        {
            Destroy(gameObject);
        }

        // to make it persist through the life cycle of the game
        DontDestroyOnLoad(gameObject);
	}

	public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);   // deserialize

            // assign the data from loadedData into the localizedText dictionary
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains " + localizedText.Count + " entries");

        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }


    public bool GetIsReady()
    {
        return isReady;
    }

}