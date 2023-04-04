using System.Collections.Generic;
using UnityEngine;

namespace TS.Model
{
    /// <summary>
    /// Manages saving, loading and starting a new game
    /// </summary>
  
    public class GamePersister
    {
        public GameData SaveData { get; private set; }

        private SOProfileParser _profileParser;
        private List<ISaveable> _objectsToPersist = new List<ISaveable>();

        private ISaveSystem _saveSystem;

        private string _fileName;
        private string _directoryName;

        public GamePersister()
        {
            _profileParser = new SOProfileParser();
            _fileName = "SaveGame.json";
            _directoryName = Application.persistentDataPath;
            _saveSystem = new JsonSaver(_fileName, _directoryName);
        }

        public void LoadGame()
        {
            SaveData = _saveSystem.Load();

            if (SaveData == null)
            {
                Debug.Log("No saved game found, starting a new game");
                NewGame();
                return;
            }

            for (int i = 0; i < _objectsToPersist.Count; i++)
            {
                _objectsToPersist[i].LoadData(SaveData);
            }
        }

        public void SaveGame()
        {
            SaveData = new GameData();

            for (int i = 0; i < _objectsToPersist.Count; i++)
            {
                _objectsToPersist[i].SaveData(SaveData);
            }

            _saveSystem.Save(SaveData);
        }


        public void NewGame()
        {
            SaveData = _profileParser.GetDefaultGameData();

            for (int i = 0; i < _objectsToPersist.Count; i++)
            {
                _objectsToPersist[i].LoadData(SaveData);
            }
        }

        public void AddPersistentObject(ISaveable obj)
        {
            if (obj == null)
            {
                return;
            }

            _objectsToPersist.Add(obj);
        }
    }
}