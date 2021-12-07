using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour{
    private float speed = 4f;//m/s
    private float angularSpeed = 360f;//eg. deg/s (3 secs for a full turn=360 deg=>120 deg/s)
    private float bulletSpeed = 40f;
    private float bulletLife = 2f;
    private float bulletRPM = 0.1f;
    private float bulletCooldown;
    private GameObject bulletPrefab;
    private Health health;
    [SerializeField]private Transform bulletSpawn;
    public override void OnStartLocalPlayer() {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    private void Start() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,gameObject.transform.position.z); 
        bulletPrefab = (GameObject)Resources.Load("Prefabs/Bullet", typeof(GameObject));
        bulletCooldown = bulletRPM;
        health=GetComponent<Health>();
    }

    private void FixedUpdate(){
        if (!this.isLocalPlayer)
            return;

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        transform.Rotate(0, h * angularSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, v * speed * Time.deltaTime);

        if (Input.GetButton("Fire1")) {
            CmdFire();
        }
    }

    private void Update() {
        if (!this.isLocalPlayer)
            return;
    }

    [Command]
    private void CmdFire() {
        if (bulletCooldown > bulletRPM) {
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * bulletSpeed;
            Destroy(bullet, bulletLife);

            NetworkServer.Spawn(bullet);//bullet information is sent to the server so all clients can see the bullet

            bulletCooldown = 0f;
        } else {
            bulletCooldown += Time.deltaTime;
        }
    }
}
