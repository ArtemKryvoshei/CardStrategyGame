using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.PrefabFactory
{
    public interface IPrefabFactory
    {
        UniTask<GameObject> SpawnAsync(string address, Vector3 position, Quaternion rotation, Transform parent = null);
        UniTask<T> SpawnAsync<T>(string address, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component;
    }
}