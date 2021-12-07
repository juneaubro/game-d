using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class EnemySpawner : NetworkBehaviour {
	public int numOfNPCs;
    private GameObject npcPrefab;
    private Spawns spawn = new Spawns();

    public override void OnStartServer() {
        npcPrefab = (GameObject)Resources.Load("Prefabs/NPC", typeof(GameObject));

        for (int i = 0; i < numOfNPCs; i++) {
            var spawnPos = spawn.getSpawn().position;
            var spawnRotation=Quaternion.Euler(0f,Random.Range(0, 180), 0f);
            var npc=Instantiate(npcPrefab,spawnPos,spawnRotation);
            NetworkServer.Spawn(npc);
        }
    }
}
