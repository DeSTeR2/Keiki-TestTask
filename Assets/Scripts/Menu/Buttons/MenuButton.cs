using Menu.LevelDatas;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Systems.DataRow
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        private LevelData _levelData;
        private Color _color;

        public void SetInfo(LevelData levelData, Color color)
        {
            _color = color;
            _levelData = levelData;

            iconImage.sprite = _levelData.Sprite;
            iconImage.color = _color;
        }
    }
}