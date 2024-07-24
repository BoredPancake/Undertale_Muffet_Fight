using System;
using UnityEngine;

public class ShardAnimation : MonoBehaviour
{
    Vector2 speed;
    Rigidbody2D rb;
    public bool reachedMaxHigh;
    public Sprite[] sprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        reachedMaxHigh = false;
        rb = GetComponent<Rigidbody2D>();
        speed = new Vector2(UnityEngine.Random.Range(-0.3f, 0.2f), 0.5f);
        rb.AddForce(speed * 10, ForceMode2D.Impulse);
    }

    void Update()
    {
        if(reachedMaxHigh)
        {
            rb.AddForce(speed * -1.5f);
        }
        else{
            rb.AddForce(speed, ForceMode2D.Impulse);
            if(transform.position.y > 0)
            {
                reachedMaxHigh = true;
            }
        }
        if(Array.IndexOf(sprites, GetComponent<SpriteRenderer>().sprite) == 3)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Array.IndexOf(sprites, GetComponent<SpriteRenderer>().sprite) + 1];
        }

    }
}
