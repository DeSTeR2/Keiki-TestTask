using System;
using UnityEngine;

namespace Menu.LevelDatas
{
    [Serializable]
    public class LevelData
    {
        public LevelType LevelType;
        public Sprite Sprite;
        public string RowName;
    }
}