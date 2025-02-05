using System.Collections.Generic;
using DefaultNamespace;
using Level;
using UI;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Данные игры
        public int currentLevel = 0;
        public int gold = 0;
        public WeaponConfig CurrentWeapon;
        public CharacterConfig CurrentCharachter;
        public int rewAdShowCount = 1;
        public int rewAdShowCooldown = 1;
        
        public List<LevelProgressData> levelProgress = new List<LevelProgressData>();
        public List<BoughtItemData> boughtWeapons = new List<BoughtItemData>();
        public List<BoughtItemData> boughtCharacters = new List<BoughtItemData>();

        



        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            openLevels[1] = true;
        }
    }
}
