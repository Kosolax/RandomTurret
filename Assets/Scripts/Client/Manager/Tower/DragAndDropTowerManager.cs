using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropTowerManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public event Action<DragAndDropTowerManager> OnBeginDragEvent;

    public event Action<DragAndDropTowerManager> OnDragEvent;

    public event Action<DragAndDropTowerManager> OnDropEvent;

    public event Action<DragAndDropTowerManager> OnEndDragEvent;

    public int Index { get; set; }

    public TowerInGame TowerInGame { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            OnBeginDragEvent?.Invoke(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            OnDragEvent?.Invoke(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null)
        {
            OnDropEvent?.Invoke(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData != null)
        {
            OnEndDragEvent?.Invoke(this);
        }
    }

    private void Start()
    {
        this.TowerInGame = null;
        this.Index = -1;
    }
}