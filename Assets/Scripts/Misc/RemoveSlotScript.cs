using UnityEngine;
using System;
using System.Collections;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.EventSystems;

class RemoveSlotScript : MonoBehaviour, IDropHandler /* , IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerClickHandler */ {

    public Inventory inventory;
    
    public GameObject Loot;

    public void Start() {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData data)
    {
        if (inventory.draggingItem)
        {
            Vector3 PlayerPosition = GameObject.FindWithTag("Player").transform.position;

            float randNum = UnityEngine.Random.Range(0, 2 * Mathf.PI); // рандомное число от 0 до 2*pi(угол)
            GameObject tmp = (GameObject)Instantiate(Loot, PlayerPosition + new Vector3(2 * Mathf.Cos(randNum), 0.7f, 2 * Mathf.Sin(randNum)), Quaternion.identity); // выбрасывает предметы в рандомное положение на окружности определенного радиуса
            tmp.GetComponent<LootType>().LootID = inventory.draggedItem.itemID;
            tmp.GetComponent<LootType>().Value = inventory.draggedItem.itemValue;
            inventory.closeDraggedItem();
        }

    }

    
}

