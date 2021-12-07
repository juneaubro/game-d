using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NPCController : NetworkBehaviour{
    private float speed = 2f;//m/s
    private float angularSpeed = 180f;//eg. deg/s (3 secs for a full turn=360 deg=>120 deg/s)
    private Health health;

    private void Start() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,gameObject.transform.position.z); 
        health=GetComponent<Health>();
    }

    private void FixedUpdate(){
        
    }

    private void Update() {
        if (!this.isLocalPlayer)
            return;
    }
}
