using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class InputController : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler
    {
        [SerializeField] private Camera _camera;
        private Vector2 _prevPoint = Vector2.zero;

        public Action<Vector2> onDrag;
        public Action onPointerClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick.Invoke();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _prevPoint = GetEventWorldPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var currentPoint = GetEventWorldPosition(eventData);
            onDrag.Invoke(_prevPoint - currentPoint);
            _prevPoint = currentPoint;
        }

        private Vector2 GetEventWorldPosition(PointerEventData eventData)
        {
            return _camera.ScreenToWorldPoint(eventData.position);
        }
    }
}