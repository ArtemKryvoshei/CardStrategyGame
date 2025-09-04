using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Core.SceneManagement
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(string sceneName);
    }
}