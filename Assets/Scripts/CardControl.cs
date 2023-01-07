using UnityEngine;
using UnityEngine.EventSystems;

public class CardControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CardPoolController CardPoolController { get; set; }
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;
    private Vector2 _startRectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startRectTransform = _rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        if (_rectTransform.anchoredPosition.x < -300)
        {
            CardPoolController.OnUnit();
            gameObject.SetActive(false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _startRectTransform;
    }
}