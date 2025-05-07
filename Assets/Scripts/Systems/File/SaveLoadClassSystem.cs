using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Systems.File
{
    public class SaveLoadClassSystem : MonoBehaviour
    {
        [SerializeField] List<ScriptableObject> saveScriptableObjects;
        private List<IFile> _filesList;

        public void FindAndLoad()
        {
            FindObjects();
            Load();
        }
        
        private void FindObjects()
        {
            _filesList = new List<IFile>();
            _filesList.AddRange(saveScriptableObjects);
        }

        public void Save()
        {
            foreach (IFile file in _filesList) { 
                file.Save();
            }
        }

        private void Load()
        {
            foreach (IFile file in _filesList)
            {
                bool result = file.Load();
                if (result == false)
                {
                    Debug.Log("Loading is failed");
                }
            }
        }

        private void OnApplicationQuit() => Save();
        private void OnDestroy() => Save();
    }
}