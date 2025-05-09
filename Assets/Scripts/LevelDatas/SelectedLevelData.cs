using Menu.LevelDatas;
using UnityEngine;

namespace LevelDatas
{
    public class SelectedLevelData
    {
        public LevelData levelData;
        public Color objectColor;

        public void SetData(LevelData levelData, Color color)
        {
            this.levelData = levelData;
            objectColor = color;
        }

        public void UpdateColor(Color color)
        {
            objectColor = color;
        }
    }
}