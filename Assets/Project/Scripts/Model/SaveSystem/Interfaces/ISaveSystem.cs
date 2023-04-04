namespace TS.Model
{
    public interface ISaveSystem
    {
        void Save(GameData data);

        GameData Load();
    }
}