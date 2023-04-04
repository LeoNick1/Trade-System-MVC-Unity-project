using System;
using System.IO;
using UnityEngine;

namespace TS.Model
{
    public class JsonSaver : ISaveSystem
    {
        private string _fileName;
        private string _saveDirectory;

        public JsonSaver(string fileName, string saveDirectory)
        {
            _fileName = fileName;
            _saveDirectory = saveDirectory;
        }

        public void Save(GameData data)
        {
            string filePath = Path.Combine(_saveDirectory, _fileName);

            try
            {
                string jsonToSave = JsonUtility.ToJson(data, true);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    Debug.Log($"Game saved to {filePath}");
                    writer.Write(jsonToSave);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Cannot save data to {filePath}. Error: {e.Message}");
            }
        }

        public GameData Load()
        {
            string filePath = Path.Combine(_saveDirectory, _fileName);

            GameData loadedData = null;

            if (File.Exists(filePath))
            {
                try
                {
                    string loadedJson = "";
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        loadedJson = reader.ReadToEnd();
                    }
                    loadedData = JsonUtility.FromJson<GameData>(loadedJson);
                }
                catch (Exception e)
                {
                    Debug.Log($"Cannot load data from {filePath}. Error: {e.Message}");
                }
            }

            return loadedData;
        }
    }
}
