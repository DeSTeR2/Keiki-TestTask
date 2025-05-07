using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public abstract class SavebleSO : ScriptableObject, IFile
    {
        protected abstract SavebleData Data { get; }
        public virtual string FileName => $"{name}.json";
        public virtual void Assign<T>(T data) where T : IData => Data.Copy(data);
        public virtual void Save() => FileSystem.Save(FileName, Data);
        public abstract bool Load();

    }
}