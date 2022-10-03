using UnityEngine;
using System.Collections;

public class Item {

	public string itemName;
	public int itemID;
	public string itemDesc;
	public Sprite itemIcon;
	public GameObject itemModel;
	public int itemPower;
	public int itemDefense;
	public int itemValue;
	public ItemType itemType;

	public enum ItemType {
		None,
		Weapon,
		Consumable,
		Quest,
		Head,
		Shoes,
		Chest,
		Trousers,
		Earrings,
		Necklace,
		Rings,
		Hands
	}

	public Item(string name, int id, string desc, int power, int defense, int value, ItemType type) {
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemPower = power;
		itemDefense = defense;
		itemValue = value;
		itemType = type;
		itemIcon = Resources.Load<Sprite> ("" + name);
	}

	public Item() {
	}
}
