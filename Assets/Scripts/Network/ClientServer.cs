using UnityEngine;
using AllJoynUnity;
using System.Collections;

namespace multi
{
	public class ClientServer : MonoBehaviour
	{
		private bool isWorking = false;
		private string playerNick = "";
		private int playerNr = 1;

		void Start()
		{
			DontDestroyOnLoad(this);
		}

		void LateUpdate()
		{
			if (!isWorking)
				return;
			if (Input.GetKeyDown(KeyCode.Escape)) {
				multiplayerHandler.CloseDown();
				Application.Quit();
			}

		}


		public void Init(string nick)
		{
			isWorking = true;
			playerNick = nick;
			playerNr = 1;

			multiplayerHandler = (MultiplayerHandler)ScriptableObject.CreateInstance("MultiplayerHandler");
			multiplayerHandler.SetPlayerNick(playerNick);
			multiplayerHandler.StartUp();
			Debug.LogError("Init");
		}

		public void Destroy() {
			multiplayerHandler.CloseDown();	
		}


		public void SendPlayerData(string name, double enemyPosX, double enemyPosY, double enemyPosZ, double turning, bool shooting, bool walking)
		{

			multiplayerHandler.SendPlayerData(name, enemyPosX, enemyPosY, enemyPosZ, turning, shooting, walking);

		}

		public void SendEnemyInitData(string sender, string name, double enemyPosX, double enemyPosY, double enemyPosZ, double turning)
		{
			multiplayerHandler.SendEnemyInitData(sender, name, enemyPosX, enemyPosY, enemyPosZ, turning);
		}

		public void SendEnemyAgroData(string sender, string myName, string playerName)
		{
			multiplayerHandler.SendEnemyAgroData(sender, myName, playerName);
		}

		public void SendEnemyHPData(string sender, string myname, double health)
		{
			multiplayerHandler.SendEnemyHPData(sender, myname, health);
		}


		public int GetPlayerNr()
		{
			return playerNr;
		}

		public string GetPlayerNick()
		{
			return playerNick;	
		}

		public bool IsDuringGame()
		{
			return multiplayerHandler.IsDuringGame();
		}

		public void SetTestStart()
		{
			multiplayerHandler.GameStarted();	
		}

		public bool isAllJoynStarted()
		{
			return MultiplayerHandler.AllJoynStarted;
		}

		public void StartUp()
		{
			multiplayerHandler.StartUp();
		}

		public void CloseDown()
		{
			multiplayerHandler.CloseDown();	
		}

		public bool HasJoinedSession()
		{
			return MultiplayerHandler.currentJoinedSession != null;
		}

		public void JoinSession(string session)
		{
			multiplayerHandler.JoinSession(session);
			playerNr = 2;
		}

		public string GetConnectedPlayerName()
		{
			return multiplayerHandler.GetConnectedPlayerNick();
		}

		public uint GetID()
		{
			return multiplayerHandler.GetID();
		}

		public string GetJoinedSession()
		{
			return multiplayerHandler.GetJoinedSession();
		}

		public ArrayList GetPlayersNicks()
		{
			ArrayList nicks = new ArrayList();
			foreach (string name in MultiplayerHandler.sFoundName)
			{
				nicks.Add(multiplayerHandler.RetrievePlayerNick(name));
			}

			return nicks;
		}

		public ArrayList GetSessions()
		{
			return MultiplayerHandler.sFoundName;
		}

		public string FoundNameToNick(string foundName)
		{
			return multiplayerHandler.RetrievePlayerNick(foundName);	
		}

		public MultiplayerHandler multiplayerHandler;
	}
}