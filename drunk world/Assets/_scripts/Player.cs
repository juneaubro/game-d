using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public LayerMask clickable;
    private NavMeshAgent myAgent;
    private GameObject overWorld;
    private GameObject underWorld;
    private GameObject tree;
    private GameObject treeText;
    private bool hasKey;
    private void Start() {
        myAgent = GetComponent<NavMeshAgent>();
        overWorld = GameObject.Find("OverWorld");
        underWorld = GameObject.Find("UnderWorld");
        tree = GameObject.Find("OptimizedTree");
        treeText = GameObject.Find("tree text");

    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {//left click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo, 100, clickable)) {
                myAgent.SetDestination(hitInfo.point);
            }
        }
        if (hasKey) {
            tree.GetComponent<MeshRenderer>().enabled = true;
            treeText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "bed overworld") {
            myAgent.enabled = false;
            transform.position = new Vector3(underWorld.transform.position.x, underWorld.transform.position.y+1f, underWorld.transform.position.z);
            myAgent.enabled = true;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+underWorld.transform.position.y, Camera.main.transform.position.z);
        } else if(other.gameObject.name == "bed underworld") {
            myAgent.enabled = false;
            transform.position = new Vector3(overWorld.transform.position.x, overWorld.transform.position.y+1, overWorld.transform.position.z);
            myAgent.enabled = true;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - underWorld.transform.position.y, Camera.main.transform.position.z);
        }
        if(other.gameObject.name == "Key") {
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            hasKey = true;
        }
    }
}
