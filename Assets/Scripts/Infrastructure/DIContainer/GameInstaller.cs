using Game;
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
        
        public override void InstallBindings()
        {
            Container.BindInstance(RayCastSystem);
            Container.BindInstance(ObjectParent);
            Container.BindInstance(soundManager);

            Container.Bind<GameController>().To<GameController>().AsSingle();
        }
    }
}