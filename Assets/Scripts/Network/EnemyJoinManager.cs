using UnityEngine;
using System.Collections;
using multi;

public class EnemyJoinManager : MonoBehaviour
{	
	public GameObject Enemy;
	int Host;
	ClientServer clientServer;
	void Start ()
	{
		Host = GameObject.FindGameObjectWithTag ("MultiplayerManager").GetComponent<MultiplayerManager> ().IsHost;
		if (Host == 1)
		{
			Destroy (this);
		}
		clientServer = GameObject.FindGameObjectWithTag("ClientServer").GetComponent<ClientServer>();
	}

	void Update() 
	{
		if (clientServer.multiplayerHandler.enemyDB.Count > 0) {
			foreach (multi.MultiplayerHandler.EnemyData data in clientServer.multiplayerHandler.enemyDB) {
				if (!data.IsSpawned) {
					Quaternion rotat = new Quaternion ();
					rotat.eulerAngles = new Vector3 (0, (float)data.angleY, 0);
					GameObject tempEnemy = (GameObject)Instantiate (Enemy, new Vector3 ((float)data.posX, (float)data.posY, (float)data.posZ), rotat);
					data.IsSpawned = true;
					tempEnemy.GetComponent<EnemyJoinMovement> ().ID = data.name;
				}
			}
		}
	}

	public void Spawn(string ID, double x, double y, double z, double rot) {
		Quaternion rotat = new Quaternion ();
		rotat.eulerAngles = new Vector3 (0, (float)rot, 0);

		GameObject TempEnemy = (GameObject)Instantiate(Enemy, new Vector3 ((float)x, (float)y, (float)z), rotat);
		TempEnemy.GetComponent<EnemyJoinMovement> ().ID = ID;
	}
  
}


