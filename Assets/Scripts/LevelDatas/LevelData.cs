using System;
using Game.Figure;
using UnityEngine;

namespace Menu.LevelDatas
{
    [Serializable]
    public class LevelData
    {
        public LevelType LevelType;
        public string RowName;
        public Sprite Sprite;
        public Figure Prefab;
        public AudioClip TaskSound;
    }
}