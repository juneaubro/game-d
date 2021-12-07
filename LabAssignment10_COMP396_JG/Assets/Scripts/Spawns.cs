using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawns : MonoBehaviour{
	public static List<Transform> spawnsList = new List<Transform>();

    private void Awake() {
        int children = GameObject.Find("SpawnPoints").transform.childCount;
        for (int i = 0; i < children; ++i)
            spawnsList.Add(GameObject.Find("SpawnPoints").transform.GetChild(i));
    }

    public Transform getSpawn() {
        Transform transform = null;
        var rand = new System.Random();
        int spawnNum = rand.Next(0, 4);

        for (int i = 0; i < Spawns.spawnsList.Count; i++) {
            if (spawnNum == i)
                transform = Spawns.spawnsList[spawnNum];
        }

        return transform;
    }
}
