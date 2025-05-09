using System;
using System.Collections.Generic;
using Menu.LevelDatas;
using UnityEngine;

namespace LevelDatas
{
    [CreateAssetMenu(fileName = "Level data", menuName = "LevelData/Data collection")]
    public class LevelDataCollection : ScriptableObject
    {
        public List<LevelData> Datas;
        public List<Color> Colors;

        public SelectedLevelData CurrentLevel = new SelectedLevelData();

        public void GetNextLevel()
        {
            Color currentColor = CurrentLevel.objectColor;
            int colorIndex = Colors.IndexOf(currentColor);
            colorIndex = (colorIndex + 1) % Colors.Count;
            
            Color newColor = Colors[colorIndex];
            CurrentLevel.UpdateColor(newColor);
        }
    }
}