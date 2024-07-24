using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PurpleSoulScript : MovementScript
{
    int lane; // down to up
    public int hp;
    public int reducedDamage;
    
    bool iframes;
    public bool petOnScreen; // When there's Muffet's pet, there are a lot of web strings; 

    public MovementScript movementScript;

    public GameObject lanes;
    GameObject hpBar;
    Text hpText;

    AudioSource damageSound;

    public Sprite[] hpBarSprites;
    public Sprite soul;
    public Sprite redSoul;

    public Sprite damaged;    
    new SpriteRenderer renderer;
    SpriteRenderer hpRenderer;
    

    void Start()
    {
        lane = 1;
        hp = 20;
        reducedDamage = 0;
        iframes = false;
        petOnScreen = false;

        movementScript = GetComponent<MovementScript>();

        lanes.SetActive(true);

        renderer = GetComponent<SpriteRenderer>();
        redSoul = renderer.sprite;
        renderer.sprite = soul;

        hpBar = GameObject.Find("UI").transform.GetChild(1).gameObject;
        hpText = GameObject.Find("Canvas").transform.GetChild(3).GetComponent<Text>(); // yeah...
        hpRenderer = hpBar.GetComponent<SpriteRenderer>();

        damageSound = GetComponent<AudioSource>();

        Destroy(GetComponent<MovementScript>()); // won't be useful anymore
    }

    void Update()
    {
        DisplayHP();
        if(MainScript.turn == "Enemy")
        {
            Move();
        }
    }

    void Move()
    {
        MoveHorizontal();
        if(Input.GetKeyDown("up") && lane < 3)
        {
            lane ++;
        }
        
        if(Input.GetKeyDown("down") && lane > 1)
        {
            lane --;
        }
        if (!petOnScreen)
        {
            switch(lane)
            {
                case 1:
                    transform.localPosition = new Vector3(transform.localPosition.x, -2.45f);
                    break;
                case 2:
                    transform.localPosition = new Vector3(transform.localPosition.x, -1.45f);
                    break;
                case 3:
                    transform.localPosition = new Vector3(transform.localPosition.x, -0.45f);
                    break;
            }
        }
        else{
            if(Input.GetKeyDown("up") && transform.position.y < 1.46f){
                transform.localPosition += new Vector3(0, 1);
            }
            else if (Input.GetKeyDown("down") && transform.position.y > -2.46f){
                transform.localPosition -= new Vector3(0, 1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        // there aren't any other colliders but i'm leaving it like this
        if (collider.gameObject.tag == "Attack")
        {
            LoseHP(4 - reducedDamage);
            Destroy(collider.gameObject);
        }
    }

    // i know triggers aren't supposed to be used this way, but if i use a collider the player gets stuck above the "pet" 
    void OnTriggerStay2D(Collider2D collider){
        if(petOnScreen){
            LoseHP(2 - reducedDamage);
        }
    }

    void DisplayHP()
    {
        hpRenderer.sprite = hpBarSprites[hp]; // WHY TF IS THIS NULL (nvm the array was empty, i fixed it)
        hpText.text = hp.ToString() + " / 20";
    }

    [ContextMenu("Deal damage")]
    void LoseHP(int hpToLose)
    {
        if (!iframes){
            if(hp > hpToLose){
            try{
                damageSound.Play();
            }
            catch(Exception e)
            {
                print(e);
                print("You deleted the audio source, you idiot");
            }
                hp -= hpToLose;
                StartCoroutine(Iframes());
            }
            else{
                GameObject.Find("GameOverScreen").transform.GetChild(0).gameObject.SetActive(true);
                 // Starts game over... this documentation is trash
            }
        }
    }

    IEnumerator Iframes()
    {
        iframes = true;
        for(int i = 0; i < 2; i++)
        {
            renderer.sprite = damaged;
            yield return new WaitForSeconds(0.13f);
            
            renderer.sprite = soul;
            yield return new WaitForSeconds(0.13f);
        }
        renderer.sprite = damaged;
        yield return new WaitForSeconds(0.13f);
        renderer.sprite = soul;
        iframes = false;
    }

}
