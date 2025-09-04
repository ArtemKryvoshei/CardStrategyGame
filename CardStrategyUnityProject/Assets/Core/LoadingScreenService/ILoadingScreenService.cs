using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Core.LoadingScreenService
{
    public interface ILoadingScreenService
    {
        UniTask InitializeAsync(string address);
        void Show();
        void Hide();
    }
}