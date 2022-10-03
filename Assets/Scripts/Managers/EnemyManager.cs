using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints; //шта? массив спавн поинтов?


    void Start ()
    {
			InvokeRepeating ("Spawn", spawnTime, spawnTime);
			StartCoroutine (Spawn ());
    }


   IEnumerator Spawn ()
    {
     yield return new WaitForSeconds (5); // время до первой волны в секуднах
        while (true)
        {
            if (playerHealth.currentHealth <= 0f)
            {
                yield break;
            }

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            yield return new WaitForSeconds(7); // время между волнами в секундах
        }
    }
}


