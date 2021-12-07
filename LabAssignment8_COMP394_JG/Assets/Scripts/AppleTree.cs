using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Tweakable Properties")]
    public GameObject applePrefab;
    public Transform spawnPoint;
    public float speed = 5f;
    public float dx = 16f;
    public float probabilityOfAppleDrop = 2f;
    public float probabiltyOfDirChange = 0.005f;
    public AudioSource shootSound;
    private float intervalBetweenAppleDrops;

    // Start is called before the first frame update
    void Start()
    {
        shootSound = GameObject.Find("Apple Tree").GetComponent<AudioSource>();
        intervalBetweenAppleDrops = 1 / probabilityOfAppleDrop;
        InvokeRepeating("DropApple",2f, intervalBetweenAppleDrops);
        shootSound.volume = PlayerPrefs.GetFloat("Volume")/2;
    }

    // Update is called once per frame
    void Update()
    {
        //Move AppleTree
        Vector3 pos = this.transform.position;
        pos.x += speed * Time.deltaTime;
        this.transform.position = pos;

        //Check for and change direction
        if(Mathf.Abs(pos.x) > dx ||Random.value<probabiltyOfDirChange)
        {
            speed = -speed;
        }
    }
    void DropApple()
    {
        shootSound.Play(0);
        Instantiate(applePrefab, spawnPoint.position, Quaternion.identity);
    }
    float Normal01(int count) {
        float r = 0;
        for(int i = 0; i < count; i++)
            r += Random.value;
        r /= count;
        return r;
    }
    float Normal(float mu,float sigma,int count) {//unfinished
        if (count < 1)
            throw new System.ArgumentException("Count should be >= 1.");
        float r = 0;    
        for(int i = 0; i < count; i++)
            r += Random.value;
        r /= count;
        return r + mu;
    }
}
