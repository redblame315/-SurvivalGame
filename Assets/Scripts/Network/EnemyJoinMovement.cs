using UnityEngine;
using System.Collections;
using multi;
public class EnemyJoinMovement : MonoBehaviour
{
	GameObject player;
    NavMeshAgent nav;
	public string ID;
	public string target = "";
	string myName;
	ClientServer clientServer;
    void Start ()
    {
		clientServer = GameObject.FindGameObjectWithTag("ClientServer").GetComponent<ClientServer>();
		player = GameObject.FindGameObjectWithTag ("Player");
        nav = GetComponent <NavMeshAgent> ();
		myName = GameObject.FindGameObjectWithTag ("MultiplayerManager").GetComponent<MultiplayerManager>().nick;
    }


    void Update ()
    {	
		if (nav != null) {
			Vector3 temp = transform.position; 
			temp.y = 0f; 
			transform.position = temp;

			int index = clientServer.multiplayerHandler.enemyDB.FindIndex (
				           delegate(multi.MultiplayerHandler.EnemyData data) {
					return data.name == ID;
				});
			if (index == -1) {
				Destroy (this);
			}

			foreach (multi.MultiplayerHandler.EnemyData data in clientServer.multiplayerHandler.enemyDB) {
				if (data.name == ID) {
					target = data.target;
				}
			}
			
			if (target == "") {
				nav.enabled = false;
			} else {
				if (target == myName) {
					nav.SetDestination (player.transform.position);
				} else {
					GameObject[] storageOfBudies;
					storageOfBudies = GameObject.FindGameObjectsWithTag ("Body");
					foreach (GameObject tempBody in storageOfBudies) {
						if (tempBody.GetComponent<BodyScript> ().name == target) {
							Vector3 tempVector = tempBody.transform.position; 
							temp.y = 0f; 
							nav.SetDestination (tempVector);
						}
					}
				}
			}
		}

	}


}