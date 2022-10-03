using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public List<GameObject> Slots = new List<GameObject> ();
    [HideInInspector]public GameObject RemoveSlot;
    public GameObject removeSlotPrefab;
	public List<Item> Items = new List<Item>();
	public GameObject slots;
	ItemDatabase database;

	public GameObject tooltip;
	int x = -90;
	int y = 80;

	public GameObject draggedItemGameObject;
	public bool draggingItem = false;
	public Item draggedItem;
	public int indexOfDraggedItem;

    public int heightCoord = 0, weightCoord = 0;

	void Update() {
		if (draggingItem) {
			Vector3 posi = (Input.mousePosition - GameObject.FindGameObjectWithTag ("Canvas").GetComponent<RectTransform> ().localPosition);
			draggedItemGameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (posi.x + 15, posi.y - 15, posi.z);
		}
    }

	void Start () {

		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
		int slotamount = 0;

		for (int i = 1; i < 6; i++) {
			for (int j = 1; j < 5; j++) {
				GameObject slot = (GameObject)Instantiate (slots);
				slot.GetComponent<SlotScript> ().slotNumber = slotamount;
				Slots.Add (slot);
				Items.Add(new Item());

				slot.transform.parent = this.gameObject.transform;
				slot.name = "Slot" + i + "." + j;
				slot.GetComponent<RectTransform> ().localPosition = new Vector3 (x, y, 0);
				x += 60;
				if (j == 4 ) {
					x = -90;
					y -= 55;
				}
				slotamount++;

                
            }
            GameObject RemoveSlot = (GameObject)Instantiate(removeSlotPrefab);
            RemoveSlot.transform.parent = this.gameObject.transform;
            RemoveSlot.GetComponent<RectTransform>().localPosition = new Vector3(270, -120, 0);

        }

         
		addItem (0);
        addItem (1);
		addItem (2);
		addItem (3);
		addItem (4);
		addItem (5);
		addItem (6);
        addItem(7);
        addItem(8);
        addItem(8);
        addItem(8);
        addItem(9);
        addItem(9);
        addItem(9);


    }

	public void CheckIfItemExist(int itemID, Item item) {
		for (int i = 0; i < Items.Count; i++) {
			if (Items [i].itemID == itemID) {
				Items [i].itemValue++;
				break;
			} 
			else if(i == Items.Count - 1) {
				addItemAtEmptySlot (item);
			}
		}
	}

	public void addItem(int id) {
		for (int i = 0; i < database.items.Count; i++) {
			if (database.items[i].itemID == id) {
				Item item = database.items[i];
				if (database.items [i].itemType == Item.ItemType.Consumable) {
					CheckIfItemExist (id, item);
					break;
				} 
				else {
					addItemAtEmptySlot (item);
					break;
				}
			}
		}
	}

	void addItemAtEmptySlot(Item item) {
		for (int i = 0; i < Items.Count; i++) {
			if (Items [i].itemName == null) {
				Items [i] = item;
				Items [i].itemValue = 1;
				break;
			}
		}
	}

	public void ShowTooltip(Vector3 toolPosition, Item item) {
		tooltip.GetComponent<RectTransform> ().localPosition = new Vector3 (toolPosition.x+20, toolPosition.y, toolPosition.z);
		tooltip.SetActive (true);

		tooltip.transform.GetChild (0).GetComponent<Text>().text = item.itemName;
		//tooltip.transform.GetChild (1).GetComponent<Text>().text = "+" + item.itemPower + " to Power";
		tooltip.transform.GetChild (2).GetComponent<Text>().text = item.itemDesc;
	}

	public void closeTooltip() {
		tooltip.SetActive (false);
	}
    
	public void showDraggedItem(Item item, int slotNumber) {
        
        indexOfDraggedItem = slotNumber;
		closeTooltip ();
		draggedItemGameObject.SetActive(true);
		draggedItem = item;
		draggingItem = true;
		draggedItemGameObject.GetComponent<Image>().sprite = item.itemIcon;
        
	}

	public void closeDraggedItem() { 
   
        draggingItem = false;
		draggedItemGameObject.SetActive(false);
	    
    }
    
}
