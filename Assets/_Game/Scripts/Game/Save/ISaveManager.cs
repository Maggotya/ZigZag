
namespace Assets._Game.Scripts.Game.Save
{
    interface ISaveManager
    {
        SaveData lastSave { get; }

        void Save(int bestScore);
        SaveData GetSave();
    }
}
