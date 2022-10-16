using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OneStory.Configs
{
    [CreateAssetMenu(fileName = "SettingsConfig", menuName = "OneStory/Configs/Settings config", order = 2)]
    public sealed class SettingsConfig : ScriptableObject
    {
        private string _savePath;

        public SettingsVariables SettingsVariables = new SettingsVariables();

        private void OnEnable()
        {
            _savePath = $"{Application.persistentDataPath}/Settings.gsave";
            LoadSettings();
        }

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(SettingsVariables);
            WriteJsonStringInFile(json);
        }

        public void LoadSettings()
        {
            if (File.Exists(_savePath))
            {
                string json = ReadJsonStringInFile();
                SettingsVariables = JsonUtility.FromJson<SettingsVariables>(json);
            }
        }

        private void WriteJsonStringInFile(string jsonString)
        {
            using (var sw = new StreamWriter(_savePath, false))
            {
                sw.WriteLine(jsonString);
            }

            Debug.Log($"[INFO] Setting save in: {_savePath}");
        }

        private string ReadJsonStringInFile()
        {
            string jsonString = "";

            using (var sr = new StreamReader(_savePath))
            {
                jsonString = sr.ReadLine();
            }

            return jsonString;
        }
    }

    [System.Serializable]
    public sealed class SettingsVariables
    {
        [Header("Sounds")]
        [Range(0, 100)] public int Sounds = 50;
        [Range(0, 100)] public int Music = 50;
        [Range(0, 100)] public int Sensitivity = 50;
        [Space(20)]
        [Header("Control")]
        public ControllersKeys ControllersKeys = new ControllersKeys();
    }

    [System.Serializable]
    public sealed class ControllersKeys
    {
        public ControlKey Forward = new ControlKey("Forward", KeyCode.W);
        public ControlKey Left = new ControlKey("Left", KeyCode.A);
        public ControlKey Back = new ControlKey("Back", KeyCode.S);
        public ControlKey Right = new ControlKey("Right", KeyCode.D);
        public ControlKey Attack = new ControlKey("Attack", KeyCode.Mouse0);
        public ControlKey Interact = new ControlKey("Interact", KeyCode.E);
    }

    [System.Serializable]
    public sealed class ControlKey
    {
        public ControlKey(string name, KeyCode key)
        {
            Name = name;
            Key = key;
        }

        public string Name;
        public KeyCode Key;

        public bool IsPressed() => Input.GetKeyDown(Key);
    }
}
