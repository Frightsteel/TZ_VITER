using UnityEngine;
using UnityEngine.EventSystems;

public class Pocket : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameManager _gameManager;

    public bool IsPermanent;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Transform target = eventData.pointerDrag.transform;
            target.SetParent(transform);
            target.localPosition = Vector3.zero;

            if (IsPermanent)
            {
                _gameManager.TargetCount--;
                _gameManager.UpdateGameStatus(transform);
            }
        }
    }
}
