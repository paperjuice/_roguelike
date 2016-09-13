using UnityEngine;
using System.Collections;

public class enemySpawnerBehaviour : MonoBehaviour {

	
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private GameObject[] enemy;

    private GameObject[] spawnPoints;


    


    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemy[0], enemy[0].transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, transform.rotation);
        }

    }


}
