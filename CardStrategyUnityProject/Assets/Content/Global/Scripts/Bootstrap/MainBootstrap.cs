using AddressablesGenerated;
using Core.AudioManager;
using UnityEngine;
using Core.EventBus;
using Core.IInitializeQueue;
using Core.LoadingScreenService;
using Core.PrefabFactory;
using Core.SceneManagement;
using Core.SceneTransitionMediator;
using Core.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;

namespace Content.Features.Bootstrap.Scripts
{
    public class MainBootstrap : MonoBehaviour
    {
        [SerializeField] private string _mainSceneName = "MainScene";
        [SerializeField] private ComponentsInitializeManager componentsInitManager;
        [SerializeField] private bool dontDestroy;
        
        private void Start()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            ServiceLocator.Register<IEventBus>(new EventBus());

            var prefabFactory = new PrefabFactory();

            var loadingScreen = new LoadingScreenService(prefabFactory);
            await loadingScreen.InitializeAsync(Address.UI.LoadingScreen);
            ServiceLocator.Register<ILoadingScreenService>(loadingScreen);

            var sceneLoader = new SceneLoader();
            ServiceLocator.Register<ISceneLoader>(sceneLoader);

            var mediator = new SceneTransitionMediator(loadingScreen, sceneLoader);
            ServiceLocator.Register<ISceneTransitionMediator>(mediator);

            var audioManagerPrefab = await prefabFactory.SpawnAsync(Address.Managers.AudioManager, Vector3.zero, Quaternion.identity);
            ServiceLocator.Register<IAudioManager>(audioManagerPrefab.GetComponent<AudioManager>());

            ServiceLocator.Register<IPrefabFactory>(prefabFactory);

            // инициализация компонентов
            componentsInitManager.InitializeComponents();

            if (dontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }

            await mediator.LoadSceneWithTransitionAsync(_mainSceneName);
        }
    }
}