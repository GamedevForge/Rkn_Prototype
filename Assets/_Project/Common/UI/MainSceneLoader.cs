using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Common.UI
{
    public class MainSceneLoader : MonoBehaviour
    {
        [SerializeField] private DayReceiver _dayReceiver;

        private ZenjectSceneLoader _sceneLoader;

        [Inject] private void Construct(ZenjectSceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        public void LoadScene()
        {
            _sceneLoader.LoadScene("Playground", LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(_dayReceiver.DayNumber).WhenInjectedInto<DayHandler>();
            });
        }
    }
}