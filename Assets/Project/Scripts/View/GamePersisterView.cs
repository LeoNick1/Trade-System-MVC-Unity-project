using System;
using UnityEngine;

namespace TS.View
{
    public class GamePersisterView : MonoBehaviour
    {
        [SerializeField] private bool _shouldSaveOnExit = true;

        public event Action SaveGameRequested;
        public event Action LoadGameRequested;
        public event Action NewGameRequested;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnSaveGameRequest();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                OnLoadGameRequest();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                OnNewGameRequest();
            }
        }

        private void OnApplicationQuit()
        {
            if (_shouldSaveOnExit)
            {
                OnSaveGameRequest();
            }
        }

        private void OnSaveGameRequest()
        {
            SaveGameRequested?.Invoke();
        }

        private void OnLoadGameRequest()
        {
            LoadGameRequested?.Invoke();
        }

        private void OnNewGameRequest()
        {
            NewGameRequested?.Invoke();
        }
    }
}