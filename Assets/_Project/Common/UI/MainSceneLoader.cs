using Project.Common.Installers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Common.UI
{
    public class MainSceneLoader : MonoBehaviour
    {
        [SerializeField] private DayReceiver _dayReceiver;

        private ZenjectSceneLoader _sceneLoader;
        private DayHandler _dayHandler;

        [Inject] private void Construct(ZenjectSceneLoader sceneLoader,
            DayHandler dayHandler)
        {
            _sceneLoader = sceneLoader;
            _dayHandler = dayHandler;
        }

        public void LoadScene()
        {
            _dayHandler.SetDayCount(_dayReceiver.DayNumber);
            _sceneLoader.LoadScene("Playground", LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(_dayReceiver.DayNumber).WhenInjectedInto<ProjectInstaller>();
            });
        }
    }
}