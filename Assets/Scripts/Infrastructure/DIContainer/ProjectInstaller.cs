using Game;
using Infrastructure.Events;
using Infrastructure.Game;
using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.StateMachine;
using LevelDatas;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain loadingCurtain;
        [SerializeField] float forcedTimeToWait = 3;
        [SerializeField] private BootsTrapper _bootsTrapper;
        [SerializeField] private SelectedLevelData _selectedLevel;
        [SerializeField] private AllEvents _allEvents;
        
        public override void InstallBindings()
        {
            Container.BindInstance(loadingCurtain).AsTransient();
            Container.BindInstance(_bootsTrapper).AsTransient();
            Container.BindInstance(_allEvents).AsTransient();
            Container.BindInstance(_selectedLevel).AsTransient();
            Container.Bind<ICoroutineRunner>().FromInstance(_bootsTrapper).AsTransient();
            
            Container.Bind<IAssetProviderService>().To<AssetProviderService>().AsSingle();
            Container.Bind<GameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<GameFactory>().To<GameFactory>().AsSingle();
            
            Container.Bind<SceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInstance(forcedTimeToWait).AsTransient().WhenInjectedInto<SceneLoader>();
        }
    }
}