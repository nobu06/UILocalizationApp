using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;   // needs to be public b/c we're going to serialize it
     
    [MenuItem("Window/Localized Text Editor")]
    static void init()
    {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();    
    }


    private void OnGUI()
    {
        // check if localization data is null or not before allowing edit or save
        if (localizationData != null)
        {
            // create a serialized obj
            SerializedObject serializedObject = new SerializedObject(this); // this -- this window
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData"); // get the serialized property of this obj. We do this b/c this is the obj we want to display and edit

            // to display the editor itself and for the user to edit the property like in the inspector
            EditorGUILayout.PropertyField(serializedProperty, true);    // true --> to fold out and edit the children

            // update the serialized obj with any changes the user makes with the inspector
            serializedObject.ApplyModifiedProperties();

            // allow the user to save. Notice will only be shown if data is either loaded or created
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }

        // for load and create

        if (GUILayout.Button("Load data"))
        {
            LoadGameData();   
        }

        if (GUILayout.Button("Create data"))
        {
            CreateNewData();
        }

    }

    private void LoadGameData()
    {
        string filePath = EditorUtility.OpenFilePanel("Select localization data file",
                                                      Application.streamingAssetsPath,
                                                      "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save localization data file",
                                                      Application.streamingAssetsPath,
                                                      "",
                                                      "json");
        // make sure the string is not empty before we try to save the data
        if (!string.IsNullOrEmpty(filePath))
        {
            // serialize the localizatationdata obj, storing it as json string
            string dataAsJson = JsonUtility.ToJson(localizationData);

            // write that text onto the file
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }



}
