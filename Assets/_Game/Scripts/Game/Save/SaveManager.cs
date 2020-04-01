using UnityEngine;

namespace Assets._Game.Scripts.Game.Save
{
    class SaveManager : ISaveManager
    {
        #region PUBLIC_VALUES
        public SaveData lastSave { get; private set; }
        #endregion // PUBLIC_VALUES

        #region CONSTS
        private const string KEY_SAVE = "save";
        #endregion // CONSTS

        //////////////////////////////////

        #region CONTRUCTORS
        public SaveManager()
            => lastSave = GetSave() ?? new SaveData();
        #endregion // CONTRUCTORS

        //////////////////////////////////

        #region PUBLIC_METHODS
        public void Save(int bestScore)
        {
            lastSave = new SaveData() { bestScore = bestScore };

            var json = JsonUtility.ToJson(lastSave);
            PlayerPrefs.SetString(KEY_SAVE, json);
            PlayerPrefs.Save();
        }

        public SaveData GetSave()
        {
            var data = PlayerPrefs.GetString(KEY_SAVE);
            return JsonUtility.FromJson<SaveData>(data);
        }
        #endregion // PUBLIC_METHODS
    }
}
