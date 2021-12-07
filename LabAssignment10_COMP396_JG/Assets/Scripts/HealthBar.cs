using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthBar : MonoBehaviour{
    private void Update() {
        gameObject.transform.LookAt(Camera.main.transform.position);
    }
}
