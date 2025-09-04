using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.PrefabFactory
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly Dictionary<string, GameObject> _cache = new();

        public async UniTask<GameObject> SpawnAsync(string address, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (!_cache.TryGetValue(address, out var prefab))
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);
                prefab = await handle.ToUniTask();
                _cache[address] = prefab;
            }

            var instance = Object.Instantiate(prefab, position, rotation, parent);
            return instance;
        }

        public async UniTask<T> SpawnAsync<T>(string address, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
        {
            var go = await SpawnAsync(address, position, rotation, parent);
            return go.GetComponent<T>();
        }
    }
}