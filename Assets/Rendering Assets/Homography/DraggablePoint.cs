using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CENTIS.Homography
{
    [ExecuteInEditMode]
    public class DraggablePoint 
        : MonoBehaviour
            , IBeginDragHandler
            , IEndDragHandler
            , IDragHandler
    {
        public new Camera camera;
        public Color normalColor = Color.white;
        public Color dragColor = Color.red;

        SpriteRenderer _renderer;
        Vector3 _startPos;
        Vector3 _startMousePos;
    
        public event Action HasChanged;
    
        public Vector2 ViewPosition
        {
            get 
            { 
                if (!camera) return Vector2.zero;
                Vector3 p = camera.WorldToViewportPoint(transform.position); 
                return new Vector2(p.x, p.y);
            }
            
            set 
            { 
                if (!camera) return;
                Vector3 p = camera.ViewportToWorldPoint(new Vector3(value.x, value.y, 0)); 
                transform.position = new Vector3(p.x, p.y, transform.position.z);
            }
        }

        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.color = normalColor;
        }

        public void OnBeginDrag(PointerEventData data)
        {
            _startPos = transform.localPosition;
            _startMousePos = Input.mousePosition;
            _renderer.color = dragColor;
#if UNTIY_EDITOR
        Debug.Log("Start Drag");
#endif
        }

        public void OnEndDrag(PointerEventData data)
        {
            _renderer.color = normalColor;
#if UNTIY_EDITOR
        Debug.Log("Stop Drag");
#endif
        }

        public void OnDrag(PointerEventData data)
        {
            float z = transform.localPosition.z;
            Vector3 mousePos = Input.mousePosition;
            Vector3 currentPos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, z));
            Vector3 startPos = camera.ScreenToWorldPoint(new Vector3(_startMousePos.x, _startMousePos.y, z));
            Vector3 dPos = currentPos - startPos;
            transform.localPosition = _startPos + dPos;
            
            HasChanged?.Invoke();
        }
    }

}