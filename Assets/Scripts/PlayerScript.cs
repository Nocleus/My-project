using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    
    public float speed;

    public Text score;
    public Text life;
    public GameObject win;
    public GameObject lose;
    public GameObject player;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject target;
    public AudioClip backgroundMusic;
    public AudioClip winMusic;
    public AudioSource AudioSource;
    
    private int scoreValue = 0;
    private int lifeValue = 3;
    private int level = 0;

    Animator anim;
    public bool rotX = false;
    
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "SCORE: " + scoreValue.ToString();
        life.text = "HEALTH: " + lifeValue.ToString();

        player.gameObject.SetActive(true);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);

        AudioSource.clip = backgroundMusic;
        AudioSource.Play();
        AudioSource.loop = true;

        anim = GetComponent<Animator>();
        anim.SetInteger("State", 1);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 2);
            if (rotX == true)
            {
                rotX = false;
                transform.Rotate(new Vector3(0,-180,0));
            }
        }
        

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 2);
            if (rotX == false)
            {
                rotX = true;
                transform.Rotate(new Vector3(0,180,0));
            }
        }

        
        if (!Input.anyKey)
        {
            anim.SetInteger("State",1);
        }
        
        if (scoreValue == 4)
        {
            level += 1;
            scoreValue -= 4;
            score.text = "SCORE: " + scoreValue.ToString();
            if (level == 1)
            {
                transform.position = new Vector3(25.0f, -1.9f, 0.0f);
                lifeValue = 3;
                life.text = "HEALTH: " + lifeValue.ToString();
            }
            if(level == 2)
            {
                win.gameObject.SetActive(true);
                stage1.gameObject.SetActive(false);
                stage2.gameObject.SetActive(false);
                AudioSource.clip = winMusic;
                AudioSource.Play();
                AudioSource.loop = false;
            }
        }

        if (lifeValue == 0)
        {
            lose.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }

        
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "SCORE: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
        }

        if (collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            life.text = "HEALTH: " + lifeValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        
        }
            
    }
}