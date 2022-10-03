using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IDropHandler, IDragHandler, IPointerClickHandler {

	public int index;
	public Item item;
	Inventory inventory;

	void Start() {
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		item = new Item ();
		item.itemType = Item.ItemType.None;
		item.itemPower = 0;
		item.itemDefense = 0;
	}

	void Update() {
		if (item.itemType != Item.ItemType.None) {
			transform.GetChild (0).GetComponent<Image> ().sprite = item.itemIcon;
			transform.GetChild (0).GetComponent<Image> ().enabled = true;
		} 
		else {
			transform.GetChild (0).GetComponent<Image> ().enabled = false;
		}
	}

	public void OnDrop (PointerEventData data) {
		//простите меня за это
		if (inventory.draggingItem) {
			if (index == 0 && inventory.draggedItem.itemType == Item.ItemType.Head) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items [inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				} else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			} 

			if (index == 1 && inventory.draggedItem.itemType == Item.ItemType.Chest) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}
	
			if (index == 2 && inventory.draggedItem.itemType == Item.ItemType.Hands) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}

			if (index == 3 && inventory.draggedItem.itemType == Item.ItemType.Shoes) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}

			if (index == 4 && inventory.draggedItem.itemType == Item.ItemType.Weapon) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}

			if (index == 5 && inventory.draggedItem.itemType == Item.ItemType.Rings) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}
		}
		//ещё раз простите
	}

	public void OnPointerClick(PointerEventData data) {
		//простите меня за это
		if (inventory.draggingItem) {
			if (index == 0 && inventory.draggedItem.itemType == Item.ItemType.Head) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items [inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				} else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			} 

			if (index == 1 && inventory.draggedItem.itemType == Item.ItemType.Chest) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}

			if (index == 2 && inventory.draggedItem.itemType == Item.ItemType.Hands) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}

			if (index == 3 && inventory.draggedItem.itemType == Item.ItemType.Shoes) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}

			if (index == 4 && inventory.draggedItem.itemType == Item.ItemType.Weapon) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}

			if (index == 5 && inventory.draggedItem.itemType == Item.ItemType.Rings) {
				if (item.itemType != Item.ItemType.None) {
					inventory.Items[inventory.indexOfDraggedItem] = item;
					item = inventory.draggedItem;
					inventory.closeDraggedItem();
				} 
				else {
					item = inventory.draggedItem;
					inventory.closeDraggedItem ();
				}
			}
		}
		//ещё раз простите
	}

	public void OnDrag(PointerEventData data) {
		if (item.itemType != Item.ItemType.None) {
			inventory.draggedItem = item;
			inventory.showDraggedItem (item, -1);
			item = new Item ();
		}
	}
}
