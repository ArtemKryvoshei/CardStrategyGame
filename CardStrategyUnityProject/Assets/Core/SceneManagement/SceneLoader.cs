using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = true;

            await operation.ToUniTask();
        }
    }
}