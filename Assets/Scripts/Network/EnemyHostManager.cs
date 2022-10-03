using UnityEngine;
using System.Collections;
using multi;

public class EnemyHostManager : MonoBehaviour
{
	public string ID;
	public GameObject enemy;
	public Transform[] spawnPoints;
	public float spawnTime = 3f;
	int quantity = 0;
	ClientServer clientServer;
	bool IsIt = true;
	int Host;

	void Start ()
    {	
		Host = GameObject.FindGameObjectWithTag ("MultiplayerManager").GetComponent<MultiplayerManager> ().IsHost;
		if (Host == 0)
		{
			Destroy (this);
		}

		clientServer = GameObject.FindGameObjectWithTag("ClientServer").GetComponent<ClientServer>();
    }

	void Update() {

		if (clientServer.HasJoinedSession () && IsIt) {
			InvokeRepeating ("Spawn", spawnTime, spawnTime);
			StartCoroutine (Spawn ());
			IsIt = false;
		}
	}

	IEnumerator Spawn ()
	{
		yield return new WaitForSeconds (5); // время до первой волны в секуднах
		while (true)
		{
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);
			EnemyHostMovement[] storage;
			storage = GameObject.FindObjectsOfType<EnemyHostMovement> ();
			if (storage.Length < 4) {
				GameObject TempEnemy = (GameObject)Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
				TempEnemy.GetComponent<EnemyHostMovement> ().ID = quantity.ToString () + ID;
				quantity++;
			}
			yield return new WaitForSeconds(7); // время между волнами в секундах
		}
	}
}


