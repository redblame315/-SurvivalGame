using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using multi;
public class MultiplayerManager : MonoBehaviour
{	
	bool isHereAnotherPlayerSpawned = false;

	ClientServer clientServer;
	public string nick = "";
	public GameObject bodyPrefab;
	GameObject player;
	public int IsHost;
	int count = 0;
	ArrayList temp = new ArrayList();

	void Start() 
	{
		nick = PlayerPrefs.GetString ("MyNickname");
		IsHost = PlayerPrefs.GetInt ("IsHost");
		Debug.LogError (IsHost);
		clientServer = GameObject.FindGameObjectWithTag("ClientServer").GetComponent<ClientServer>();
		clientServer.Init (nick);
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update()
	{
		temp = clientServer.GetSessions();
		if (clientServer.HasJoinedSession())
		{
			if (count % 2 == 0) {
				clientServer.SendPlayerData (nick, (double)player.transform.position.x, (double)player.transform.position.y, (double)player.transform.position.z, (double)player.transform.rotation.eulerAngles.y, player.GetComponent<Animator> ().GetBool ("IsWalking"), player.GetComponentInChildren<PlayerShooting> ().isShooting);
			}
			count++;
		}

		else if (clientServer.GetSessions().Count > 0)
		{
			string name = clientServer.FoundNameToNick((string)temp[0]);
			clientServer.JoinSession((string)temp[0]);
		}

		if (clientServer.multiplayerHandler.playerDB.Count > 0) {
			foreach (multi.MultiplayerHandler.PlayerData data in clientServer.multiplayerHandler.playerDB) {
				if (!data.IsSpawned) {
					Quaternion rotat = new Quaternion ();
					rotat.eulerAngles = new Vector3 (0, (float)data.angleY, 0);
					GameObject tempBody = (GameObject)Instantiate (bodyPrefab, new Vector3 ((float)data.posX, (float)data.posY, (float)data.posZ), rotat);
					data.IsSpawned = true;
					tempBody.GetComponent<BodyScript> ().name = data.name;
				}
			}
		}
	
	}
}