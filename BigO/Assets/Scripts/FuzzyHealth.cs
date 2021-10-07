using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyHealth : MonoBehaviour {
    public float healthPercent = 100f;
    float health;
    float inPain = 0.4f;

    // Start is called before the first frame update
    void Start() {
        health = healthPercent / 100f;
    }

    // Update is called once per frame
    void Update() {
        string fuzzyHealthS = FuzzyHealthS(health);
        Debug.Log("Fuzzy Health = " + fuzzyHealthS);
    }

    private string FuzzyHealthS(float health) {
        float fuzzyHealthF = FuzzyHealthF(health);
        if (fuzzyHealthF == Healthy(health)) {
            return "Healthy";
        } else if (fuzzyHealthF == InPain(health)) {
            return "InPain";
        } else {
            return "Critical";
        }
    }

    private float FuzzyHealthF(float health) {
        float max = Mathf.Max(Healthy(health), InPain(health));
        return Mathf.Max(max,Critical(health));
    }

    private float Critical(float health) {
        return 1f - (InPain(health));
    }

    private float InPain(float health) {
        float k = 1f / inPain;

        if (health < 0 || health > 100)
            throw new ArgumentOutOfRangeException();

        if (health <= inPain) {
            return k * health;
        } else {
            return (1 - health) / (1 - inPain);
        }
    }

    private float Healthy(float health) {
        return health;
    }
}
