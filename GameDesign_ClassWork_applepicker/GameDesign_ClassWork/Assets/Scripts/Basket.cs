using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public Text scoreText;
    public int appleScore = 1;
    private int score = 0;
    public GameController gameController;
    public AudioSource colletSound;

    // Start is called before the first frame update
    void Start()
    {
        colletSound = GameObject.Find("Basket").GetComponent<AudioSource>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "Score: " + score;
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBasket();
    }
    private void MoveBasket()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 pos = transform.position;
        pos.x = mousePosWorld.x;
        transform.position = pos;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Apple")
        {
            colletSound.Play(0);
            score += appleScore;
            scoreText.text = string.Format("Score: {0}", score);
            Destroy(other.gameObject);
        }
    }
}
