using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.LevelDatas
{
    [CreateAssetMenu(fileName = "Level data", menuName = "LevelData/Data collection")]
    public class LevelDataCollection : ScriptableObject
    {
        public List<LevelData> Datas;
        public List<Color> Colors;
        
        private Dictionary<LevelType, LevelData> imageDictionary = new();

        public LevelData GetData(LevelType levelType)
        {
            if (imageDictionary.ContainsKey(levelType))
            {
                return imageDictionary[levelType];
            }

            foreach (LevelData levelData in Datas)
            {
                if (levelData.LevelType == levelType)
                {
                    imageDictionary.Add(levelType, levelData);
                    return levelData;
                }
            }
            
            throw new Exception($"Did not found data of type {levelType.ToString()}");
        }
    }
}