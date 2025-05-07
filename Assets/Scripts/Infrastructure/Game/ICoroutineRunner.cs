using System.Collections;
using UnityEngine;

namespace Infrastructure.Game
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}