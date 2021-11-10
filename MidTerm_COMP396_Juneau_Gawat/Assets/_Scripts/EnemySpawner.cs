using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float numOfEnemies = 10f;
    public float time = 10f;

    private GameObject enemy;
    private void Start() {
        enemy = GameObject.FindWithTag("Enemy");
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() {
        for(int i = 0; i < numOfEnemies; i++) {
            Instantiate(enemy);
        }

        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnEnemy());
    }
}
