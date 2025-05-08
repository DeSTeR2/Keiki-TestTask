using System;
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
        private Action<Color> _onClick;
        private Button _button;

        public void SetInfo(LevelData levelData, Color color, Action<Color> onClick)
        {
            _onClick = onClick;
            _color = color;
            _levelData = levelData;

            iconImage.sprite = _levelData.Sprite;
            iconImage.color = _color;

            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                _onClick?.Invoke(_color);
            });
        }
    }
}