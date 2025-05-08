using System;
using System.Collections.Generic;
using Infrastructure.Game;
using Infrastructure.Services.Assets;
using Menu.LevelDatas;
using UnityEngine;

namespace Menu.Systems.DataRow
{
    public class ButtonFactory
    {
        private readonly LevelData _levelData;
        private readonly Transform _buttonParent;
        private readonly List<Color> _colors;
        private readonly IAssetProviderService _assetProviderService;

        public ButtonFactory(LevelData levelData, Transform buttonParent, List<Color> colors,
            IAssetProviderService assetProviderService)
        {
            _levelData = levelData;
            _buttonParent = buttonParent;
            _colors = colors;
            _assetProviderService = assetProviderService;
        }

        public void CreateButtons(Action<Color> onClick)
        {
            for (int i = 0; i < _colors.Count; i++)
            {
                MenuButton btn = _assetProviderService.Instantiate<MenuButton>(Constants.MenuButton, _buttonParent);
                btn.SetInfo(_levelData, _colors[i], onClick);
            }
        }
    }
}