using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Health : NetworkBehaviour{
	public const int maxHealth = 100;
	[SyncVar(hook= "OnChangedHealth")]
	public int currenthealth = maxHealth;
	public GameObject healthBar;
	public void TakeDamage(int damage) {
		if(!this.isServer)
			return;

		currenthealth -= damage;

        if (currenthealth <= 0) {
			currenthealth = maxHealth;
			healthBar.GetComponent<Slider>().value=currenthealth;
			RpcReSpawn();
        }

        if (currenthealth != 0) {
			healthBar.SetActive(true);
			healthBar.GetComponent<Slider>().value=currenthealth;
		}
    }

	[ClientRpc]
    private void RpcReSpawn() {
        Spawns spawns = new Spawns();
		transform.position=spawns.getSpawn().position;
	}

    void OnChangedHealth(int health) {
		healthBar.GetComponent<Slider>().value = health;

        if (healthBar.GetComponent<Slider>().value <= 0) {
			healthBar.SetActive(false);
		} else {
			healthBar.SetActive(true);
		}
	}
}
