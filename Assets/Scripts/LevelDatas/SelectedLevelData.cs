using Menu.LevelDatas;
using UnityEngine;

namespace LevelDatas
{
    [CreateAssetMenu(fileName = "Selected level", menuName = "LevelData/Selected level")]
    public class SelectedLevelData : ScriptableObject
    {
        public LevelData levelData;
        public Color objectColor;

        public void SetData(LevelData levelData, Color color)
        {
            this.levelData = levelData;
            objectColor = color;
        }
    }
}