using TS.Model;
using TS.View;

namespace TS.Controller
{
    public class GamePersisterController
    {
        private GamePersister _gamePersisterModel;
        private GamePersisterView _gamePersisterView;

        public GamePersisterController(GamePersister model, GamePersisterView view)
        {
            _gamePersisterModel = model;
            _gamePersisterView = view;
            Subscribe();
        }

        private void Subscribe()
        {
            _gamePersisterView.SaveGameRequested += SaveGame;
            _gamePersisterView.LoadGameRequested += LoadGame;
            _gamePersisterView.NewGameRequested += NewGame;
        }

        private void Unsubscribe()
        {
            _gamePersisterView.SaveGameRequested -= SaveGame;
            _gamePersisterView.LoadGameRequested -= LoadGame;
            _gamePersisterView.NewGameRequested -= NewGame;
        }

        public void AddSaveableObject(ISaveable obj)
        {
            _gamePersisterModel.AddPersistentObject(obj);
        }

        public void SaveGame()
        {
            _gamePersisterModel.SaveGame();
        }

        public void LoadGame()
        {
            _gamePersisterModel.LoadGame();
        }

        public void NewGame()
        {
            _gamePersisterModel.NewGame();
        }
    }
}