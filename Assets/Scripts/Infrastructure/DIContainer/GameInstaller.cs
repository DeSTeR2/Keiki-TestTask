using Game;
using Game.Figure;
using Systems;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraRayCastSystem RayCastSystem;
        [SerializeField] private Transform ObjectParent;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private PathAnimation pathAnimation;
        [SerializeField] private SegmentPath segmentPath;
        
        public override void InstallBindings()
        {
            Container.BindInstance(RayCastSystem);
            Container.BindInstance(ObjectParent);
            Container.BindInstance(soundManager);
            Container.BindInstance(gameConfig);
            Container.BindInstance(pathAnimation);
            Container.BindInstance(segmentPath);

            Container.Bind<GameController>().To<GameController>().AsSingle();
        }
    }
}