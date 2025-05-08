using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Assets
{
    public interface IAssetProviderService
    {
        TAsset GetAsset<TAsset>(string path) where TAsset : Object;
        TObject GetReference<TObject>(GameObject go);
        
        TObject Instantiate<TObject>(string path, DiContainer container = null);
        TObject Instantiate<TObject>(string path, Transform at, DiContainer container = null);
        TObject Instantiate<TObject>(GameObject go, Transform at, DiContainer container = null);
        
    }
}