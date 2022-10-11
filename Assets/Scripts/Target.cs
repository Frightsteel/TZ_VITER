using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Target : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _image;
    private Canvas _mainCanvas;
    private RectTransform _rect;

    private Animator _animator;

    // animations IDs
    private int animIDVanish;

    private Transform currentParent;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _image = GetComponent<Image>();

        _animator = GetComponent<Animator>();

        if (GetComponentInParent<Pocket>().IsPermanent)
        {
            //_image.raycastTarget = false;
        }  

        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        animIDVanish = Animator.StringToHash("IsLineCompleted");
    }

    public void RunAnimation()
    {
        _animator.SetBool(animIDVanish, true);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentParent = _rect.parent;
        transform.SetParent(currentParent.parent);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == currentParent.parent)
        {
            transform.SetParent(currentParent);
            transform.localPosition = Vector3.zero;
        }
        if (!GetComponentInParent<Pocket>().IsPermanent)
            _image.raycastTarget = true;
    }
}
