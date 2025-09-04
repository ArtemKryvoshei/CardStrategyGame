using System.Threading.Tasks;
using Core.LoadingScreenService;
using Core.Other;
using Core.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.SceneTransitionMediator
{
    public class SceneTransitionMediator : ISceneTransitionMediator
    {
        private readonly ILoadingScreenService _loadingScreen;
        private readonly ISceneLoader _sceneLoader;

        public SceneTransitionMediator(ILoadingScreenService loadingScreen, ISceneLoader sceneLoader)
        {
            _loadingScreen = loadingScreen;
            _sceneLoader = sceneLoader;
        }

        public async UniTask LoadSceneWithTransitionAsync(string sceneName)
        {
            _loadingScreen.Show();

            await _sceneLoader.LoadSceneAsync(sceneName);

            // фейковая задержка
            await UniTask.Delay(System.TimeSpan.FromSeconds(ConstantsHolder.LOAD_SCREEN_SHOW_TIME));

            _loadingScreen.Hide();
        }
    }
}