using UnityEngine;
using AllJoynUnity;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


namespace multi
{
	public class MultiplayerHandler : ScriptableObject
	{
		private const string INTERFACE_NAME = "org.alljoyn.bus.multi";
		private const string SERVICE_NAME = "org.alljoyn.bus.multi";
		private const string SERVICE_PATH = "/rhrmulti";
		private const ushort SERVICE_PORT = 25;

		private static readonly string[] connectArgs = {"unix:abstract=alljoyn",
			"tcp:addr=127.0.0.1,port=9955",
			"launchd:"};
		private string connectedVal;

		private static AllJoyn.BusAttachment msgBus;
		private MyBusListener busListener;
		private MySessionPortListener sessionPortListener;
		private static MySessionListener sessionListener;
		private static TestBusObject testObj;
		private AllJoyn.InterfaceDescription testIntf;
		public AllJoyn.SessionOpts opts;

		public static ArrayList sFoundName = new ArrayList();
		public static string currentJoinedSession = null;
		private static uint currentSessionId = 0;
		private static string myAdvertisedName = null;

		public static bool AllJoynStarted = false;

		private string playerNick = "";
		private string connectedPlayerNick = "";

		private bool isDuringGame = false;
		public bool IHaveGotTheWorld = false;

		public class PlayerData {
			public string name;
			public double posX;
			public double posY;
			public double posZ;
			public double angleY;
			public bool IsWalking;
			public bool IsShooting;
			public bool IsSpawned;
		}

		public class EnemyData
		{
			public string name;
			public string target;
			public double posX;
			public double posY;
			public double posZ;
			public double angleY;
			public bool IsSpawned;
		}

		public List<PlayerData> playerDB = new List<PlayerData> ();
		public List<EnemyData> enemyDB = new List<EnemyData> ();

		private static Mutex mutex = new Mutex();

		private static bool allJoynDebugOn = false;

		public MultiplayerHandler()
		{
			sFoundName = new ArrayList();
		}

		public MultiplayerHandler(string nick)
		{
			sFoundName = new ArrayList();
			playerNick = nick;
		}

		public void Start()
		{
			DontDestroyOnLoad(this);
		}

		public void SendPlayerData(string name, double enemyPosX, double enemyPosY, double enemyPosZ, double turning, bool shooting, bool walking)
		{
			testObj.SendPlayerData(name, enemyPosX, enemyPosY, enemyPosZ, turning, shooting, walking);
		}

		public void SendEnemyInitData(string sender, string name, double enemyPosX, double enemyPosY, double enemyPosZ, double turning)
		{
			testObj.SendEnemyInitData(sender, name, enemyPosX, enemyPosY, enemyPosZ, turning);
		}

		public void SendEnemyAgroData(string sender, string myName, string playerName)
		{
			testObj.SendEnemyAgroData(sender, myName, playerName);
		}

		public void SendEnemyHPData(string sender, string myname, double health)
		{
			testObj.SendEnemyHPData(sender, myname, health);
		}




		public bool IsDuringGame()
		{
			return isDuringGame;
		}

		public void GameStarted()
		{
			isDuringGame	= true;
		}

		public void GameEnded()
		{
			isDuringGame = false;	
		}

		public string RetrievePlayerNick(string advertisedName)
		{
			int delimiterIndex = advertisedName.IndexOf("._") + 2 +
				msgBus.GlobalGUIDString.Length;
			return advertisedName.Substring(delimiterIndex);
		}

		public void SetPlayerNick(string nick)
		{
			playerNick = nick;
		}

		public void SetConnectedPlayerNick(string nick)
		{
			connectedPlayerNick = nick;
		}

		public string GetConnectedPlayerNick()
		{
			return connectedPlayerNick;
		}

		public string GetJoinedSession()
		{
			return currentJoinedSession;
		}

		public uint GetID()
		{
			return currentSessionId;
		}


		public void PlayerSignalHandler(AllJoyn.InterfaceDescription.Member member, string srcPath, AllJoyn.Message message)
		{
			if (playerNick != (string)message [0]) {
				int index = playerDB.FindIndex(
					delegate(PlayerData data)
					{
						return data.name == (string)message[0];
					});

				if (index == -1) {
					PlayerData temporaryData = new PlayerData();
					temporaryData.name = (string)message [0];
					temporaryData.posX = (double)message [1];
					temporaryData.posY = (double)message [2];
					temporaryData.posZ = (double)message [3];
					temporaryData.angleY = (double)message [4];
					temporaryData.IsWalking = (bool)message [5];
					temporaryData.IsShooting = (bool)message [6];
					temporaryData.IsSpawned = false;
					playerDB.Add (temporaryData);
				} 
				else 
				{
					playerDB[index].posX = (double)message [1];
					playerDB[index].posY = (double)message [2];
					playerDB[index].posZ = (double)message [3];
					playerDB[index].angleY = (double)message [4];
					playerDB[index].IsWalking = (bool)message [5];
					playerDB[index].IsShooting = (bool)message [6];
				}
			}
		}

		public void EnemyInitSignalHandler(AllJoyn.InterfaceDescription.Member member, string srcPath, AllJoyn.Message message)
		{
			if (playerNick != (string)message [0]) {
				int index = enemyDB.FindIndex(
					delegate(EnemyData data)
					{
						return data.name == (string)message[1];
					});
				if (index == -1) {
					EnemyData temporaryData = new EnemyData ();
					temporaryData.name = (string)message [1];
					temporaryData.posX = (double)message [2];
					temporaryData.posY = (double)message [3];
					temporaryData.posZ = (double)message [4];
					temporaryData.angleY = (double)message [5];
					temporaryData.IsSpawned = false;
					enemyDB.Add (temporaryData);
				} else {
					enemyDB[index].posX = (double)message [2];
					playerDB[index].posY = (double)message [3];
					playerDB[index].posZ = (double)message [4];
					playerDB[index].angleY = (double)message [5];
				}
			}
		}

		public void EnemyAgroSignalHandler(AllJoyn.InterfaceDescription.Member member, string srcPath, AllJoyn.Message message)
		{
			if (playerNick != (string)message [0]) {
				int index = enemyDB.FindIndex(
					delegate(EnemyData data)
					{
						return data.name == (string)message[1];
					});
				enemyDB[index].target = (string)message [2];
			}
		}

		public void EnemyHPSignalHandler(AllJoyn.InterfaceDescription.Member member, string srcPath, AllJoyn.Message message)
		{
			if (playerNick != (string)message [0]) {
				int index = enemyDB.FindIndex(
					delegate(EnemyData data)
					{
						return data.name == (string)message[1];
					});
				enemyDB.RemoveAt (index);
			}
		}



		class TestBusObject : AllJoyn.BusObject
		{
			private AllJoyn.InterfaceDescription.Member playerMember;
			private AllJoyn.InterfaceDescription.Member enemyInitMember;
			private AllJoyn.InterfaceDescription.Member enemyAgroMember;
			private AllJoyn.InterfaceDescription.Member enemyHPMember;

			public TestBusObject(AllJoyn.BusAttachment bus, string path) : base(path, false)
			{
				AllJoyn.InterfaceDescription exampleIntf = bus.GetInterface(INTERFACE_NAME);
				AllJoyn.QStatus status = AddInterface(exampleIntf);
				if(!status)
				{
					//Debug.LogError("Failed to add interface " + status.ToString());
				}

				playerMember = exampleIntf.GetMember("player");
				enemyInitMember = exampleIntf.GetMember("enemyInit");
				enemyAgroMember = exampleIntf.GetMember("enemyAgro");
				enemyHPMember = exampleIntf.GetMember("enemyHP");
			}

			protected override void OnObjectRegistered ()
			{
				//Debug.LogError("ObjectRegistered has been called");
			}

			public void SendPlayerData(string name, double enemyPosX, double enemyPosY, double enemyPosZ, double turning, bool shooting, bool walking)
			{
				AllJoyn.MsgArgs payload = new AllJoyn.MsgArgs((uint)7);
				payload[0].Set((string)name);
				payload[1].Set((double)enemyPosX);
				payload[2].Set((double)enemyPosY);
				payload[3].Set((double)enemyPosZ);
				payload[4].Set((double)turning);
				payload[5].Set((bool)shooting);
				payload[6].Set((bool)walking);

				byte flags = AllJoyn.ALLJOYN_FLAG_GLOBAL_BROADCAST;
				AllJoyn.QStatus status = Signal(null, 0, playerMember, payload, 0, flags);
				if (!status)
				{
					//Debug.LogError("failed to send vector(data) signal: " + status.ToString());
				}
			}

			public void SendEnemyInitData(string sender, string name, double enemyPosX, double enemyPosY, double enemyPosZ, double turning)
			{
				//Debug.LogError ("SendEnemyInitData");
				AllJoyn.MsgArgs payload = new AllJoyn.MsgArgs((uint)6);
				payload[0].Set((string)sender);
				payload[1].Set((string)name);
				payload[2].Set((double)enemyPosX);
				payload[3].Set((double)enemyPosY);
				payload[4].Set((double)enemyPosZ);
				payload[5].Set((double)turning);

				byte flags = AllJoyn.ALLJOYN_FLAG_GLOBAL_BROADCAST;
				AllJoyn.QStatus status = Signal(null, 0, enemyInitMember, payload, 0, flags);
				if (!status)
				{
					//Debug.LogError("failed to send vector(data) signal: " + status.ToString());
				}
			}

			public void SendEnemyAgroData(string sender, string myName, string playerName)
			{
				//Debug.LogError ("SendEnemyAgroData");
				AllJoyn.MsgArgs payload = new AllJoyn.MsgArgs((uint)3);
				payload[0].Set((string)sender);
				payload[1].Set((string)myName);
				payload[2].Set((string)playerName);

				byte flags = AllJoyn.ALLJOYN_FLAG_GLOBAL_BROADCAST;
				AllJoyn.QStatus status = Signal(null, 0, enemyAgroMember, payload, 0, flags);
				if (!status)
				{
					Debug.LogError("failed to send vector(data) signal: " + status.ToString());
				}
			}

			public void SendEnemyHPData(string sender, string Myname, double health)
			{
				AllJoyn.MsgArgs payload = new AllJoyn.MsgArgs((uint)3);
				payload[0].Set((string)sender);
				payload[1].Set((string)Myname);
				payload[2].Set((double)health);

				byte flags = AllJoyn.ALLJOYN_FLAG_GLOBAL_BROADCAST;
				AllJoyn.QStatus status = Signal(null, 0, enemyHPMember, payload, 0, flags);
				if (!status)
				{
					Debug.LogError("failed to send vector(data) signal: " + status.ToString());
				}
			}

		}

		class MyBusListener : AllJoyn.BusListener
		{
			protected override void FoundAdvertisedName(string name, AllJoyn.TransportMask transport, string namePrefix)
			{
				//Debug.LogError("FoundAdvertisedName(name=" + name + ", prefix=" + namePrefix + ")");
				if (string.Compare(myAdvertisedName, name) == 0)
				{
					//Debug.LogError("Ignoring my advertisement");
				}
				else if (string.Compare(SERVICE_NAME, namePrefix) == 0)
				{
					//Debug.LogError("Bla");
					sFoundName.Add(name);
				}
			}

			protected override void ListenerRegistered(AllJoyn.BusAttachment busAttachment)
			{
				//Debug.LogError("ListenerRegistered: busAttachment=" + busAttachment);
			}

			protected override void NameOwnerChanged(string busName, string previousOwner, string newOwner)
			{
				//Debug.LogError("NameOwnerChanged: name=" + busName + ", oldOwner=" +
					//previousOwner + ", newOwner=" + newOwner);
			}

			protected override void LostAdvertisedName(string name, AllJoyn.TransportMask transport, string namePrefix)
			{
				//Debug.LogError("LostAdvertisedName(name=" + name + ", prefix=" + namePrefix + ")");
				sFoundName.Remove(name);
			}
		}

		class MySessionPortListener : AllJoyn.SessionPortListener
		{
			private MultiplayerHandler multiplayerHandler;

			public MySessionPortListener(MultiplayerHandler multiplayerHandler)
			{
				this.multiplayerHandler = multiplayerHandler;
			}

			protected override bool AcceptSessionJoiner(ushort sessionPort, string joiner, AllJoyn.SessionOpts opts)
			{

				if (sessionPort != SERVICE_PORT)
				{
					//Debug.LogError("Rejecting join attempt on unexpected session port " + sessionPort);
					return false;
				}
				//Debug.LogError("Accepting join session request from " + joiner + 
					//" (opts.proximity=" + opts.Proximity + ", opts.traffic=" + opts.Traffic + 
					//", opts.transports=" + opts.Transports + ")");
				return true;
			}

			protected override void SessionJoined(ushort sessionPort, uint sessionId, string joiner)
			{
				//Debug.LogError("Session Joined!!!!!!");
				currentSessionId = sessionId;
				currentJoinedSession = myAdvertisedName;
				multiplayerHandler.SetConnectedPlayerNick(joiner);
				multiplayerHandler.GameStarted();
				if(sessionListener == null) {
					sessionListener = new MySessionListener(multiplayerHandler);
					msgBus.SetSessionListener(sessionListener, sessionId);
				}
			}
		}

		class MySessionListener : AllJoyn.SessionListener
		{
			private MultiplayerHandler multiplayerHandler;

			public MySessionListener(MultiplayerHandler multiplayerHandler)
			{
				this.multiplayerHandler = multiplayerHandler;	
			}
			protected override void	SessionLost(uint sessionId)
			{
				multiplayerHandler.SetConnectedPlayerNick("");
				multiplayerHandler.GameEnded();
				//Debug.LogError("SessionLost (" + sessionId + ")");
			}

			protected override void SessionMemberAdded(uint sessionId, string uniqueName)
			{
				//Debug.LogError("SessionMemberAdded (" + sessionId + "," + uniqueName + ")");
			}

			protected override void SessionMemberRemoved(uint sessionId, string uniqueName)
			{
				//Debug.LogError("SessionMemberRemoved (" + sessionId + "," + uniqueName + ")");
			}
		}

		public void StartUp()
		{
			//Debug.LogError("Starting AllJoyn");
			AllJoynStarted = true;
			AllJoyn.QStatus status = AllJoyn.QStatus.OK;
			{
				//Debug.LogError("Creating BusAttachment");
				msgBus = new AllJoyn.BusAttachment("myApp", true);

				status = msgBus.CreateInterface(INTERFACE_NAME, false, out testIntf);
				if (status)
				{
					//Debug.LogError("Interface Created.");
					testIntf.AddSignal ("player", "sddddbb", "playerPoints", 0);
					testIntf.AddSignal("enemyInit", "ssdddd", "enemyPoints", 0);
					testIntf.AddSignal ("enemyAgro", "sss", "enemyPoints1", 0);
					testIntf.AddSignal ("enemyHP", "ssd", "enemyPoints2", 0);
					testIntf.Activate();
				}
				else
				{
					//Debug.LogError("Failed to create interface 'org.alljoyn.Bus.chat'");
				}

				busListener = new MyBusListener();
				if (status)
				{
					msgBus.RegisterBusListener(busListener);
					//Debug.LogError("BusListener Registered.");
				}


				if (testObj == null)
					testObj = new TestBusObject(msgBus, SERVICE_PATH);

				if (status)
				{
					status = msgBus.Start();
					if (status)
					{
						//Debug.LogError("BusAttachment started.");

						msgBus.RegisterBusObject(testObj);
						for (int i = 0; i < connectArgs.Length; ++i)
						{
							//Debug.LogError("Connect trying: " + connectArgs[i]);
							status = msgBus.Connect(connectArgs[i]);
							if (status)
							{
								//Debug.LogError("BusAttchement.Connect(" + connectArgs[i] + ") SUCCEDED.");
								connectedVal = connectArgs[i];
								break;
							}
							else
							{
								//Debug.LogError("BusAttachment.Connect(" + connectArgs[i] + ") failed.");
							}
						}
						if (!status)
						{
							//Debug.LogError("BusAttachment.Connect failed.");
						}
					}
					else
					{
						//Debug.LogError("BusAttachment.Start failed.");
					}
				}

				myAdvertisedName = SERVICE_NAME+ "._" + msgBus.GlobalGUIDString + playerNick;





				AllJoyn.InterfaceDescription.Member playerMember = testIntf.GetMember ("player");
				status = msgBus.RegisterSignalHandler(this.PlayerSignalHandler, playerMember, null);
				if (!status)
				{
					//Debug.LogError("Failed to add vector signal handler " + status);
				}
				else
				{
					//Debug.LogError("add vector signal handler " + status);
				}

				AllJoyn.InterfaceDescription.Member enemyInitMember = testIntf.GetMember("enemyInit");
				status = msgBus.RegisterSignalHandler(this.EnemyInitSignalHandler, enemyInitMember, null);
				if (!status)
				{
					Debug.LogError("Failed to add vector signal handler " + status);
				}
				else
				{
					//Debug.LogError("add vector signal handler " + status);
				}

				AllJoyn.InterfaceDescription.Member enemyAgroMember = testIntf.GetMember("enemyAgro");
				status = msgBus.RegisterSignalHandler(this.EnemyAgroSignalHandler, enemyAgroMember, null);
				if (!status)
				{
					Debug.LogError("Failed to add vector signal handler " + status);
				}
				else
				{
					//Debug.LogError("add vector signal handler " + status);
				}

				AllJoyn.InterfaceDescription.Member enemyHPMember = testIntf.GetMember("enemyHP");
				status = msgBus.RegisterSignalHandler(this.EnemyHPSignalHandler, enemyHPMember, null);
				if (!status)
				{
					Debug.LogError("Failed to add vector signal handler " + status);
				}
				else
				{
					//Debug.LogError("add vector signal handler " + status);
				}


				status = msgBus.AddMatch("type='signal',interface='org.alljoyn.bus.multi'");
				if (!status)
				{
					Debug.LogError("Failed to add vector Match " + status.ToString());
				}
				else
				{
					//Debug.LogError("add vector Match " + status.ToString());
				}
			}

			if (status)
			{
				status = msgBus.RequestName(myAdvertisedName,
					AllJoyn.DBus.NameFlags.ReplaceExisting | AllJoyn.DBus.NameFlags.DoNotQueue);
				if (!status)
				{
					Debug.LogError("RequestName(" + SERVICE_NAME + ") failed (status=" + status + ")");
				}
			}

			opts = new AllJoyn.SessionOpts(AllJoyn.SessionOpts.TrafficType.Messages, false,
				AllJoyn.SessionOpts.ProximityType.Any, AllJoyn.TransportMask.Any);
			if (status)
			{
				ushort sessionPort = SERVICE_PORT;
				sessionPortListener = new MySessionPortListener(this);
				status = msgBus.BindSessionPort(ref sessionPort, opts, sessionPortListener);
				if (!status || sessionPort != SERVICE_PORT)
				{
					Debug.LogError("BindSessionPort failed (" + status + ")");
				}
				//Debug.LogError("BBindSessionPort on port (" + sessionPort + ")"); ;
			}

			if (status)
			{
				status = msgBus.AdvertiseName(myAdvertisedName, opts.Transports);
				if (!status)
				{
					Debug.LogError("Failed to advertise name " + myAdvertisedName + " (" + status + ")");
				}
			}

			status = msgBus.FindAdvertisedName(SERVICE_NAME);
			if (!status)
			{
				Debug.LogError("org.alljoyn.Bus.FindAdvertisedName failed.");
			}
		}
		public bool JoinSession(string session)
		{
			
			AllJoyn.QStatus status = AllJoyn.QStatus.NONE;
			if (sessionListener != null) {
				status = msgBus.SetSessionListener(null, currentSessionId);
				sessionListener = null;
				if (!status) {
					Debug.LogError("SetSessionListener status(" + status.ToString() + ")");
				}
			}
			sessionListener = new MySessionListener(this);
			//Debug.LogError("About to call JoinSession (Session=" + session + ")");
			status = msgBus.JoinSession(session, SERVICE_PORT, sessionListener, out currentSessionId, opts);
			if(status)
			{
				//Debug.LogError("Client JoinSession SUCCESS (Session id=" + currentSessionId + ")");
				currentJoinedSession = session;
			}
			else
			{
				//Debug.LogError("RHR JoinSession failed (status=" + status.ToString() + ")");
			}

			return status ? true : false;
		}
		public void CloseDown()
		{	
			/*if (msgBus == null)
				return;
			AllJoynStarted = false;
			
			AllJoyn.QStatus status = msgBus.CancelFindAdvertisedName(SERVICE_NAME);
			if (!status){
                Debug.LogError("CancelAdvertisedName failed status(" + status.ToString() + ")");
			}
			status = msgBus.CancelAdvertisedName(myAdvertisedName, opts.Transports);
			if (!status) {
                Debug.LogError("CancelAdvertisedName failed status(" + status.ToString() + ")");
			}
			status = msgBus.ReleaseName(myAdvertisedName);
			if (!status) {
                Debug.LogError("ReleaseName status(" + status.ToString() + ")");
			}
			status = msgBus.UnbindSessionPort(SERVICE_PORT);
			if (!status) {
                Debug.LogError("UnbindSessionPort status(" + status.ToString() + ")");
			}
			
			status = msgBus.Disconnect(connectedVal);
			if (!status) {
                Debug.LogError("Disconnect status(" + status.ToString() + ")");
			}
			
			AllJoyn.InterfaceDescription.Member playerMember = testIntf.GetMember("player");
			status = msgBus.UnregisterSignalHandler(this.PlayerSignalHandler, playerMember, null);
			vectorMember = null;
			if (!status) {
                Debug.LogError("UnregisterSignalHandler Vector status(" + status.ToString() + ")");
			}
			if (sessionListener != null) {
				status = msgBus.SetSessionListener(null, currentSessionId);
				sessionListener = null;
				if (!status) {
                    Debug.LogError("SetSessionListener status(" + status.ToString() + ")");
				}
			}
            Debug.LogError("No Exceptions(" + status.ToString() + ")");
			currentSessionId = 0;
			currentJoinedSession = null;
			sFoundName.Clear();
			
			connectedVal = null;
        	msgBus = null;
			busListener = null;
			sessionPortListener = null;
			testObj = null;
			testIntf = null;
	        opts = null;
			myAdvertisedName = null;
			
			AllJoynStarted = false;
			
			sFoundName = new ArrayList();
			
			AllJoyn.StopAllJoynProcessing();*/
		}
	}
}
