using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evading : MonoBehaviour
{
    public bool powerUp = true;
    public static bool PowerUp;
    public float seeDist;
    public float speed = 2f;

    private SphereCollider sc;
    private Rigidbody rb;
    private GameObject player;
    [SerializeField]
    private Material yellow;
    [SerializeField]
    private Material blue;
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<SphereCollider>();
        sc.radius = seeDist;

        rb = GetComponent<Rigidbody>();

        mr = GetComponent<MeshRenderer>();

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PowerUp = powerUp;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player" && powerUp == true) {
            Vector3 direction = transform.position - player.transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            Debug.Log(player.transform.position);
            rb.velocity = transform.forward * speed/2;
            mr.material = yellow;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" && powerUp == true) {
            mr.material = blue;
        }
    }
}
