using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	GameObject player;
	PlayerHealth playerHealth;
	PlayerShooting playerShooting;

	void Start () {
		for (int i = 0; i < 6 ; i++) {
			transform.GetChild (i).GetComponent<CharacterSlot> ().index = i;
		}

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerShooting = player.GetComponentInChildren<PlayerShooting>();
	}

	void Update() {
		
		playerShooting.damageBonus = transform.GetChild (0).GetComponent<CharacterSlot> ().item.itemPower +
		transform.GetChild (1).GetComponent<CharacterSlot> ().item.itemPower +
		transform.GetChild (2).GetComponent<CharacterSlot> ().item.itemPower +
		transform.GetChild (3).GetComponent<CharacterSlot> ().item.itemPower +
		transform.GetChild (4).GetComponent<CharacterSlot> ().item.itemPower +
		transform.GetChild (5).GetComponent<CharacterSlot> ().item.itemPower;

		playerHealth.defense = transform.GetChild (0).GetComponent<CharacterSlot> ().item.itemDefense +
			transform.GetChild (1).GetComponent<CharacterSlot> ().item.itemDefense +
			transform.GetChild (2).GetComponent<CharacterSlot> ().item.itemDefense +
			transform.GetChild (3).GetComponent<CharacterSlot> ().item.itemDefense +
			transform.GetChild (4).GetComponent<CharacterSlot> ().item.itemDefense +
			transform.GetChild (5).GetComponent<CharacterSlot> ().item.itemDefense;
	}

}
