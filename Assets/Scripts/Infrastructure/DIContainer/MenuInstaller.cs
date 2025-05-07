using System;
using Menu;
using Menu.LevelDatas;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private LevelDataCollection _dataCollection;
        [SerializeField] private Transform _contentRowParent;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_dataCollection).AsTransient();
            Container.BindInstance(_contentRowParent).AsTransient();
            Container.Bind<MenuController>().AsSingle().NonLazy();
        }
    }
}