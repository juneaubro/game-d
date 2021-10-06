using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour
{
    public GameObject player;
    private FSMSystem fsm;
    public Material idle;
    public Material attention;
    public Material talking;
    public Material listening;
    public static Material Idle;
    public static Material Attention;
    public static Material Talking;
    public static Material Listening;
    public float attention_distance = 6f;
    public static bool can_see_player;
    public Transform Reset_Position;

    public static MeshRenderer MR;
    private SphereCollider SC;
    public void SetTransition(Transition t) {
        fsm.PerformTransition(t);
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeFSM();
        MR = GetComponent<MeshRenderer>();
        SC = GetComponent<SphereCollider>();
        SC.radius = attention_distance;
        Idle = idle;
        Attention = attention;
        Talking = talking;
        Listening = listening;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);
    }

    void MakeFSM() {
        IdleState Idle = new IdleState();
        Idle.AddTransition(Transition.SawPlayer, StateID.Attention);

        AttentionState Attention = new AttentionState();
        Attention.AddTransition(Transition.Interact, StateID.Talking);

        TalkingState Talking = new TalkingState();
        Talking.AddTransition(Transition.Interact, StateID.Listening);

        ListeningState Listening = new ListeningState();
        Listening.AddTransition(Transition.Interact, StateID.Attention);

        fsm = new FSMSystem();
        fsm.AddState(Idle);
        fsm.AddState(Attention);
        fsm.AddState(Talking);
        fsm.AddState(Listening);
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject == player) {
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

public class IdleState : FSMState {
    public IdleState() {
        stateID = StateID.Idle;
    }

    public override void Act(GameObject player, GameObject npc) {
        NPCControl.MR.material = NPCControl.Attention;
    }

    public override void Reason(GameObject player, GameObject npc) {
        if (NPCControl.can_see_player == true) {
            npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
        }
    }
}

public class AttentionState : FSMState {
    public AttentionState() {
        stateID = StateID.Attention;
    }

    public override void Act(GameObject player, GameObject npc) {
        NPCControl.MR.material = NPCControl.Talking;
    }

    public override void Reason(GameObject player, GameObject npc) {
        if(Input.GetKeyDown(KeyCode.E)) {
            npc.GetComponent<NPCControl>().SetTransition(Transition.Interact);
        }
    }
}

public class TalkingState : FSMState {
    public override void Act(GameObject player, GameObject npc) {
        NPCControl.MR.material = NPCControl.Listening;
    }

    public override void Reason(GameObject player, GameObject npc) {
        if (Input.GetKeyDown(KeyCode.E)) {
            npc.GetComponent<NPCControl>().SetTransition(Transition.Interact);
        }
    }
}

public class ListeningState : FSMState {
    public override void Act(GameObject player, GameObject npc) {
        NPCControl.MR.material = NPCControl.Attention;
    }

    public override void Reason(GameObject player, GameObject npc) {
        if (Input.GetKeyDown(KeyCode.E)) {
            npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
        }
    }
}