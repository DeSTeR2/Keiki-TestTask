using System.Collections.Generic;
using Infrastructure.Game;
using Infrastructure.Services.Assets;
using Menu.LevelDatas;
using Menu.Systems.DataRow;
using UnityEngine;
using Zenject;

namespace Menu
{
    public class MenuController
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly LevelDataCollection _levelDataCollection;
        private readonly Transform _contentRowParent;
        private readonly DiContainer _container;

        public MenuController(IAssetProviderService assetProviderService, 
            LevelDataCollection levelDataCollection, Transform contentRowParent, DiContainer container)
        {
            _assetProviderService = assetProviderService;
            _levelDataCollection = levelDataCollection;
            _contentRowParent = contentRowParent;
            _container = container;
            CreateContentRows();
        }

        private void CreateContentRows()
        {
            List<LevelData> levelDatas = _levelDataCollection.Datas;
            for (int i = 0; i < levelDatas.Count; i++)
            {
                ContentRow row = _assetProviderService.Instantiate<ContentRow>(Constants.ContentRow, _contentRowParent, _container);
                row.SpawnButtons(levelDatas[i]);
            }
        }
    }
}