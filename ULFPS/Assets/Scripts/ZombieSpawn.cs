using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public GameObject zombiePrefab; // Zombie1
    public Transform spawnPoint; // posicion de spwan zombies 
    private float spawnDelay = 10f; //  inicio delay de spwan zombies 
    private float minSpawnDelay = 10f; // minimo entre spwan de zombies 
    private float spawnDelayDecrement = 5f; // cantidad minimo de retraso 
    private int maxHordes = 30; // cantidad maxima configurable de spwan 
    private int hordeCount = 0;

     private void Start()
    {
    

        InvokeRepeating("SpawnEnemyHorde", spawnDelay, spawnDelay);
    }

    private void SpawnEnemyHorde()
    {
        if (hordeCount >= maxHordes)
            return;

        int enemyCount = Random.Range(1, maxHordes + 1); // random 

        for (int i = 0; i < enemyCount; i++)
        {
            //Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

          
            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
            EnemyController enemyController = zombie.GetComponent<EnemyController>();
            enemyController.StartAttack();
            enemyController.EnableHitbox();
            
        }

        hordeCount++;

        spawnDelay -= spawnDelayDecrement;
        spawnDelay = Mathf.Max(spawnDelay, minSpawnDelay);

        CancelInvoke("SpawnEnemyHorde");
        InvokeRepeating("SpawnEnemyHorde", spawnDelay, spawnDelay);
    }
}