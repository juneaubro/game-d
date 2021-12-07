using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    private AudioSource song;
    private void Start() {
        song = GameObject.Find("Music").GetComponent<AudioSource>();
        song.Play(0);
    }
    private void Update() {
        song.volume = PlayerPrefs.GetFloat("Volume")*2;
        if (MainMenu.mute) {
            song.mute = true;
        } else {
            song.mute = false;
        }
    }
}
