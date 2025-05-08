using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Assets
{
    public class AssetProviderService : IAssetProviderService
    {
        private readonly DiContainer _container;

        public AssetProviderService(DiContainer container)
        {
            _container = container;
        }

        public TAsset GetAsset<TAsset>(string path) where TAsset : Object
        {
            return Resources.Load<TAsset>(path);
        }

        public TObject GetReference<TObject>(GameObject go)
        {
            return go.GetComponent<TObject>();
        }

        public TObject Instantiate<TObject>(string path, DiContainer container = null)
        {
            container = ConfigureContainer(container);
            
            GameObject go = GetAsset<GameObject>(path);
            go = container.InstantiatePrefab(go);
            return GetReference<TObject>(go);
        }

        public TObject Instantiate<TObject>(string path, Transform at, DiContainer container = null)
        {            
            container = ConfigureContainer(container);
            
            GameObject go = GetAsset<GameObject>(path);
            go = container.InstantiatePrefab(go, at);
            return GetReference<TObject>(go);
        }

        public TObject Instantiate<TObject>(GameObject go, Transform at, DiContainer container = null)
        {
            container = ConfigureContainer(container);
            
            go = container.InstantiatePrefab(go, at);
            return GetReference<TObject>(go);
        }

        private DiContainer ConfigureContainer(DiContainer container)
        {
            if (container == null)
                container = _container;
            return container;
        }
    }
}