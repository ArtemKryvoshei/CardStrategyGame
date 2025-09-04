using Core.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Core.SceneTransitionMediator
{
    public interface ISceneTransitionMediator
    {
        UniTask LoadSceneWithTransitionAsync(string sceneName);
    }
}