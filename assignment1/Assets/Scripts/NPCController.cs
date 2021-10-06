using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCState {
        Idle,
        Attention,
        Talking,
        Listening
    };

    public float attention_distance = 6f;
    public NPCState current_state = NPCState.Idle;
    public bool can_see_player;
    public GameObject player;
    public float angular_speed_deg_per_sec = 60f;
    public float angular_speed_rad_per_sec;
    public Material Idle;
    public Material Attention;
    public Material Talking;
    public Material Listening;
    public Transform Reset_Position;

    private SphereCollider SC;
    private MeshRenderer MR;

    void ChangeState(NPCState new_state) {
        current_state = new_state;
    }

    float Deg2Rad(float deg) {
        //return deg * Mathf.Deg2Rad;
        return deg / 180f * Mathf.PI;
    }
    float Rad2Deg(float rad) {
        //return rad * Mathf.Rad2Deg;
        return rad / Mathf.PI * 180f;
    }

    void Start() {
        angular_speed_rad_per_sec = Deg2Rad(angular_speed_deg_per_sec);
        MR = GetComponent<MeshRenderer>();
        SC = GetComponent<SphereCollider>();
        SC.radius = attention_distance;
    }

    void Update() {
        angular_speed_rad_per_sec = Deg2Rad(angular_speed_deg_per_sec);
        HandleFSM();
    }

    void HandleFSM() {
        switch (current_state) {
            case NPCState.Idle:
                HandleIdleState();
                break;
            case NPCState.Attention:
                HandleAttentionState();
                break;
            case NPCState.Talking:
                HandleTalkingState();
                break;
            case NPCState.Listening:
                HandleListeningState();
                break;
            default:
                break;
        }
    }

    void HandleIdleState() {
        Debug.Log("NPC is in Idle State");
        
        if(can_see_player == true) {
            ChangeState(NPCState.Attention);
        } else {
            MR.material = Idle;
        }
    }

    void HandleAttentionState() {
        Debug.Log("NPC is in Attention State");

        if(can_see_player == true) {
            MR.material = Attention;
        } else {
            MR.material = Idle;
            ChangeState(NPCState.Idle);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            MR.material = Talking;
            ChangeState(NPCState.Talking);
        }
    }

    void HandleTalkingState() {
        Debug.Log("NPC is in Talking State");

        if (can_see_player == false || Input.GetKeyDown(KeyCode.Escape)) {
            MR.material = Idle;
            ChangeState(NPCState.Idle);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            MR.material = Listening;
            ChangeState(NPCState.Listening);
        }
    }

    void HandleListeningState() {
        Debug.Log("NPC is in Listening State");

        if (can_see_player == false || Input.GetKeyDown(KeyCode.Escape)) {
            MR.material = Idle;
            ChangeState(NPCState.Idle);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            MR.material = Idle;
            ChangeState(NPCState.Idle);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject == player) {
            can_see_player = true;
            Vector3 NPCPos = gameObject.transform.position;
            Vector3 PlayerPos = other.gameObject.transform.position;
            Quaternion toPlayer = Quaternion.LookRotation(PlayerPos - NPCPos);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, toPlayer, 2f * Time.deltaTime);
        } else {
            can_see_player = false;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Reset_Position.rotation, 2f * Time.deltaTime);
        }
    }
}
