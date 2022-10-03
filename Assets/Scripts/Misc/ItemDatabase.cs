using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	void Start () {
		items.Add (new Item ("assault", 0, "The best armor", 0, 10, 1, Item.ItemType.Chest));
		items.Add (new Item ("BottleRED", 1, "HP Potion", 0, 10, 1, Item.ItemType.Consumable));
		items.Add (new Item ("rapier", 2, "The best weapon", 100, 0, 1, Item.ItemType.Weapon));
		items.Add (new Item ("helm_of_the_dominator", 3, "The best head armor", 0, 10, 1, Item.ItemType.Head));
		items.Add (new Item ("gloves", 4, "The best gloves", 0, 10, 1, Item.ItemType.Hands));
		items.Add (new Item ("power_treads_agi", 5, "The best shoes", 0, 10, 1, Item.ItemType.Shoes));
		items.Add (new Item ("ring_of_protection", 6, "The Amazing ring", 0, 10, 1, Item.ItemType.Rings));
		items.Add (new Item ("chainmail", 7, "The best armor", 0, 10, 1, Item.ItemType.Chest));
        items.Add(new Item("BottleGREEN", 8, "Speed Potion", 0, 10, 1, Item.ItemType.Consumable));
        items.Add(new Item("BottleBLUE", 9, "Power Potion", 0, 10, 1, Item.ItemType.Consumable));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
