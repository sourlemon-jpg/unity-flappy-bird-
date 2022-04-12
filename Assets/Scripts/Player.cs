using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource jumpAudio;
    public AudioSource scoreAudio;
    public AudioSource crashAudio;
    private Vector3 direction;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 2.0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    public float force;
    // Start is called before the first frame update
    
    // Update is called once per frame
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * force;
            jumpAudio.Play();
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                rb.velocity = Vector2.up * force;
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            crashAudio.Play();
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
            scoreAudio.Play();
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            crashAudio.Play();
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
