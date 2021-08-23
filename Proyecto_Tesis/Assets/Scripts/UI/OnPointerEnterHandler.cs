using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class OnPointerEnterHandler : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent OnPointerEnterFunction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterFunction?.Invoke();
    }
}
