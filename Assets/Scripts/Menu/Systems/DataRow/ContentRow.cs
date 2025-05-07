using System;
using System.Collections.Generic;
using Infrastructure.Services.Assets;
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

        [Inject]
        public void Construct(LevelDataCollection levelDataCollection, IAssetProviderService assetProviderService)
        {
            _assetProviderService = assetProviderService;
            _levelDataCollection = levelDataCollection;
        }   
        
        public void SpawnButtons(LevelData levelData)
        {
            List<Color> colors = _levelDataCollection.Colors;

            ButtonFactory buttonFactory = new ButtonFactory(levelData, buttonParent, colors, _assetProviderService);
            buttonFactory.CreateButtons();

            rowName.text = levelData.RowName;
        }
    }
}