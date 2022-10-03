using UnityEngine;
using System.Collections;
using multi;

public class EnemyHostMovement : MonoBehaviour
{
    GameObject player;
    NavMeshAgent nav;
	ClientServer clientServer;
	public string ID;
	string target;
	string PreviousTarget;
	string myName;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        nav = GetComponent <NavMeshAgent> ();
		clientServer = GameObject.FindGameObjectWithTag("ClientServer").GetComponent<ClientServer>();
		myName = GameObject.FindGameObjectWithTag ("MultiplayerManager").GetComponent<MultiplayerManager>().nick;
		PreviousTarget = "";
		target = "";
		clientServer.multiplayerHandler.SendEnemyInitData (myName, ID, (double)gameObject.transform.position.x, (double)gameObject.transform.position.x, (double)gameObject.transform.position.x, (double)gameObject.transform.rotation.eulerAngles.y);
    }


    void Update ()
	{	
		if (nav != null) {
		Vector3 temp = transform.position; 
		temp.y = 0f; 
		transform.position = temp;

		GameObject[] storageOfBudies;
		storageOfBudies = GameObject.FindGameObjectsWithTag ("Body");

		Vector3 abra = transform.position - player.transform.position;
		float distance = abra.magnitude;
		target = myName;

		foreach (GameObject tempBody in storageOfBudies) {
			Vector3 abra2 = transform.position - tempBody.transform.position;
			float tempDistance = abra2.magnitude;
			if (tempDistance < distance) {
				distance = tempDistance;
				target = tempBody.GetComponent<BodyScript> ().name;
			}
		}
		if (target != myName) {
			foreach (GameObject tempBody in storageOfBudies) {
				if (tempBody.GetComponent<BodyScript> ().name == target) {
					Vector3 tempVector = tempBody.transform.position; 
					temp.y = 0f; 
					nav.SetDestination (tempVector);
				}
			}
		} else {
			nav.SetDestination (player.transform.position);
		}

		if (target != PreviousTarget) {
			clientServer.SendEnemyAgroData (myName, ID, target);
			PreviousTarget = target;
		}

		//clientServer.multiplayerHandler.SendEnemyInitData (myName, ID, (double)gameObject.transform.position.x, (double)gameObject.transform.position.x, (double)gameObject.transform.position.x, (double)gameObject.transform.rotation.eulerAngles.y);
		}
	}	







}
