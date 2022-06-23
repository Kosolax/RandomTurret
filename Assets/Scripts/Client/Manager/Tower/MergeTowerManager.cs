using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class MergeTowerManager : MonoBehaviour
{
    public Image DraggableTower;

    public DragAndDropTowerManager TowerSlotDragged;

    public List<DragAndDropTowerManager> TowerSlots;

    [Inject]
    private readonly TowerBusiness towerBusiness;

    private void BeginDrag(DragAndDropTowerManager towerSlot)
    {
        if (towerSlot.TowerInGame != null)
        {
            this.TowerSlotDragged = towerSlot;
            this.DraggableTower.sprite = towerSlot.TowerInGame.Image.sprite;
            this.DraggableTower.color = towerSlot.TowerInGame.Image.color;
            this.DraggableTower.transform.position = Input.mousePosition;
            this.DraggableTower.enabled = true;
        }
    }

    private void Drag(DragAndDropTowerManager towerSlot)
    {
        if (this.DraggableTower.enabled)
        {
            this.DraggableTower.transform.position = Input.mousePosition;
        }
    }

    private void Drop(DragAndDropTowerManager dropTowerSlot)
    {
        if (this.TowerSlotDragged != null && this.TowerSlotDragged.Index != dropTowerSlot.Index)
        {
            this.towerBusiness.MergeTower(this.TowerSlotDragged.Index, dropTowerSlot.Index);
        }
    }

    private void EndDrag(DragAndDropTowerManager towerSlot)
    {
        this.TowerSlotDragged = null;
        this.DraggableTower.enabled = false;
    }

    private void Start()
    {
        for (int i = 0; i < this.TowerSlots.Count; i++)
        {
            this.TowerSlots[i].OnBeginDragEvent += this.BeginDrag;
            this.TowerSlots[i].OnEndDragEvent += this.EndDrag;
            this.TowerSlots[i].OnDragEvent += this.Drag;
            this.TowerSlots[i].OnDropEvent += this.Drop;
        }
    }
}