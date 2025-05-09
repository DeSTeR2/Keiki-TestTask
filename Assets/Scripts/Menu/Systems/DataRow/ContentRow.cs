using System;
using System.Collections.Generic;
using Infrastructure.Services.Assets;
using LevelDatas;
using Menu.LevelDatas;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu.Systems.DataRow
{
    public class ContentRow : MonoBehaviour
    {
        [SerializeField] private Transform buttonParent;
        [SerializeField] private TextMeshProUGUI rowName;
        private LevelDataCollection _levelDataCollection;
        private IAssetProviderService _assetProviderService;

        public Action<LevelData, Color> OnLevelSelected;
        private LevelData _levelData;

        [Inject]
        public void Construct(LevelDataCollection levelDataCollection, IAssetProviderService assetProviderService)
        {
            _assetProviderService = assetProviderService;
            _levelDataCollection = levelDataCollection;
        }   
        
        public void SpawnButtons(LevelData levelData)
        {
            _levelData = levelData;
            List<Color> colors = _levelDataCollection.Colors;

            ButtonFactory buttonFactory = new ButtonFactory(levelData, buttonParent, colors, _assetProviderService);
            buttonFactory.CreateButtons((color) =>
            {
                OnLevelSelected?.Invoke(_levelData, color);
            });

            rowName.text = levelData.RowName;
        }
    }
}