using Project.Common.Core;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Common.UI
{
    public class CursorRayCaster : CursorBaseRaycaster
    {      
        private PlayerState _playerState;

        [Inject] private void Construct(
            PlayerState playerState)
        {
            _playerState = playerState;
        }

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            if (_playerState.InComputer == false)
                return;

            base.Raycast(eventData, resultAppendList);
        }
    }
}
