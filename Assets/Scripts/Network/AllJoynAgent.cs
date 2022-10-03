using UnityEngine;
using AllJoynUnity;

public class AllJoynAgent : MonoBehaviour
{
	void Awake()
	{
		//Debug.LogError("AllJoyn Library version: " + AllJoyn.GetVersion());
		//Debug.LogError("AllJoyn Library buildInfo: " + AllJoyn.GetBuildInfo());
		DontDestroyOnLoad(this);
	}
	
	void OnApplicationQuit()
	{
		AllJoyn.StopAllJoynProcessing();
	}
}
