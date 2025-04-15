using Project.Common.Configs;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Project.Common.Core.Quest
{
    public class TargetPointerController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private RectTransform _markerInScreenTransform;
        [SerializeField] private RectTransform _markerOutSideTransform;

        private Camera _camera => Camera.main;
        private float MinX => _camera.ViewportToScreenPoint(new Vector3(0f, 0f, 0f)).x;
        private float MaxX => _camera.ViewportToScreenPoint(new Vector3(1f, 0f, 0f)).x;
        private float MinY => _camera.ViewportToScreenPoint(new Vector3(0f, 0f, 0f)).y;
        private float MaxY => _camera.ViewportToScreenPoint(new Vector3(0f, 1f, 0f)).y;


        private void Update()
        {
            if (_target == null)
                return;

            Vector3 screenPointPosition = _camera.WorldToScreenPoint(_target.position);
            Vector2 viewPortPosition = _camera.ScreenToViewportPoint(screenPointPosition);

            if (viewPortPosition.x < 0f || viewPortPosition.x > 1f || viewPortPosition.y < 0f || viewPortPosition.y > 1f)
            {
                _markerInScreenTransform.gameObject.SetActive(false);
                _markerOutSideTransform.gameObject.SetActive(true);
                _markerOutSideTransform.position = new Vector2(
                    Mathf.Clamp(screenPointPosition.x, MinX + _markerOutSideTransform.sizeDelta.x / 2, MaxX - _markerOutSideTransform.sizeDelta.x / 2),
                    Mathf.Clamp(screenPointPosition.y, MinY + _markerOutSideTransform.sizeDelta.y / 2, MaxY - _markerOutSideTransform.sizeDelta.y / 2));
            }
            else
            {
                if (screenPointPosition.z > 0f)
                {
                    _markerInScreenTransform.position = screenPointPosition;
                    _markerInScreenTransform.gameObject.SetActive(true);
                    _markerOutSideTransform.gameObject.SetActive(false);
                }
                else
                {
                    _markerInScreenTransform.gameObject.SetActive(false);
                    _markerOutSideTransform.gameObject.SetActive(true);
                    _markerOutSideTransform.position = new Vector2(
                        (Screen.width - _markerOutSideTransform.sizeDelta.x / 2) * Mathf.Sign(_markerOutSideTransform.position.x),
                        Mathf.Clamp(screenPointPosition.y, MinY + _markerOutSideTransform.sizeDelta.y / 2, MaxY - _markerOutSideTransform.sizeDelta.y / 2));
                }
            }
        }

        public void SetTarget(Transform target, string name)
        {
            _text.text = name;
            _target = target;
        }
    }

    public class QuestController : IInitializable
    {
        private readonly QuestConfigService _configService;
        private readonly QuestView _view;
        private readonly List<QuestEvent> _questEvents = new();

        public QuestConfig CurrentQuestConfig { get; private set; }

        public QuestController(QuestConfigService configService, QuestView questView)
        {
            _configService = configService;
            _view = questView;
        }
        
        public void Initialize() =>
            CurrentQuestConfig = _configService.GetQuestConfig();

        public void AddQuestEvent(QuestEvent questEvent)
        {
            _questEvents.Add(questEvent);
            questEvent.OnEvent += CloseCurrentQuest;
        }

        public void RemoveQuestEvent(QuestEvent questEvent)
        {
            _questEvents.Remove(questEvent);
            questEvent.OnEvent -= CloseCurrentQuest;
        }

        private void CloseCurrentQuest(string id)
        {
            if (CurrentQuestConfig == null)
                return;
            
            foreach (QuestEvent questEvent in _questEvents)
            {
                if (id == questEvent.ID && id == CurrentQuestConfig.ID)
                    CurrentQuestConfig.IsActive = false;
            }
            GetNextQuest();
        }

        private void GetNextQuest() =>
            CurrentQuestConfig = _configService.GetQuestConfig();
    }

    public class QuestView
    {
        private readonly TargetPointerController _targetPointerController;

        public QuestView(TargetPointerController targetPointerController)
        {
            _targetPointerController = targetPointerController;
        }
    }

    public class QuestViewFactory
    {

    }
}
