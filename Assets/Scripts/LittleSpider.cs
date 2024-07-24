using System.Collections;
using UnityEngine;

public class LittleSpider : MonoBehaviour
{
    GameObject sign;
    GameObject attack;
    GameObject telegram;
    public GameObject flower;

    public GameObject text;

    Sprite sprite1;
    public Sprite sprite2;
    public Sprite[] signSprites;
    public Sprite[] attackSprites;
    SpriteRenderer spriteRenderer;

    public bool finished;

    void Start()
    {
        sign = transform.GetChild(0).gameObject;
        attack = transform.GetChild(1).gameObject;
        telegram = transform.GetChild(2).gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite1 = spriteRenderer.sprite;
        finished = false;
    }

    public IEnumerator ShowNextAttack(string attackName){
        Coroutine coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x > 4){
            transform.position -= new Vector3(0.2f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
        yield return null;
        spriteRenderer.sprite = sprite2;
        foreach(Sprite sprite in signSprites)
        {
            sign.GetComponent<SpriteRenderer>().sprite = sprite;
            yield return new WaitForSeconds(0.06f);
        }
        text.SetActive(true);

        GameObject[] attacks = new GameObject[3];
        for(int i = 0; i < 3; i++){
            attacks[i] = attack.transform.GetChild(i).gameObject;
        }

        switch(attackName){
        case "1 spider":
            attacks[0].transform.localPosition = new Vector3(0,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[0];
            break;
        case "2 spiders":
            attacks[0].transform.localPosition = new Vector3(-0.6f,0);
            attacks[1].transform.localPosition = new Vector3(0.6f,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[0];
            attacks[1].GetComponent<SpriteRenderer>().sprite = attackSprites[0];
            break;
        case "spider, donut":
            attacks[0].transform.localPosition = new Vector3(-0.4f,0);
            attacks[1].transform.localPosition = new Vector3(0.4f,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[0];
            attacks[1].GetComponent<SpriteRenderer>().sprite = attackSprites[1];
            break;
        case "cupcake":
            attacks[0].transform.localPosition = new Vector3(0,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[2];
            break;
        case "croissant":
            attacks[0].transform.localPosition = new Vector3(0,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[3];
            break;
        case "donuts":
            attacks[0].transform.localPosition = new Vector3(-0.6f,0);
            attacks[1].transform.localPosition = new Vector3(0.6f,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[1];
            attacks[1].GetComponent<SpriteRenderer>().sprite = attackSprites[1];
            break;
        case "croissants":
            attacks[0].transform.localPosition = new Vector3(-0.4f,0);
            attacks[1].transform.localPosition = new Vector3(0.4f,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[3];
            attacks[1].GetComponent<SpriteRenderer>().sprite = attackSprites[3];
            break;
        case "final":
            attacks[0].transform.localPosition = new Vector3(-1,0);
            attacks[1].transform.localPosition = new Vector3(0,0);
            attacks[2].transform.localPosition = new Vector3(1,0);
            attacks[0].GetComponent<SpriteRenderer>().sprite = attackSprites[0];
            attacks[1].GetComponent<SpriteRenderer>().sprite = attackSprites[1];
            attacks[2].GetComponent<SpriteRenderer>().sprite = attackSprites[3];
            break;
        }
        yield return new WaitForSeconds(1);
        do{
            yield return null;
        }
        while(MainScript.turn == "Player");

        text.SetActive(false);
        
        foreach(GameObject attack in attacks){
            attack.GetComponent<SpriteRenderer>().sprite = null;
        }
        for(int i = signSprites.Length-1; i >= 0; i--){
            sign.GetComponent<SpriteRenderer>().sprite = signSprites[i];
            yield return new WaitForSeconds(0.06f);
        }
        sign.GetComponent<SpriteRenderer>().sprite = null;
        coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x < 10){
            transform.position += new Vector3(0.2f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
    }

    public IEnumerator Flower(){
        telegram.SetActive(false);
        Coroutine coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x > 1.4)
        {
            transform.position -= new Vector3(0.07f, 0);
            yield return null;
        }
        while(transform.position.x > 1)
        {
            transform.position -= new Vector3(0.02f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
        spriteRenderer.sprite = sprite2;
        yield return new WaitForSeconds(3.5f);
        coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x < 10)
        {
            transform.position += new Vector3(0.15f, 0);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        while(transform.position.x > 0.3f){
            transform.position -= new Vector3(0.07f, 0);
            flower.transform.position -= new Vector3(0.07f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
        yield return new WaitForSeconds(1);
        while(flower.transform.position.y > 0.3){
            flower.transform.position -= new Vector3(0.014f, 0.007f);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x < 1.4)
        {
            transform.position += new Vector3(0.04f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
        yield return new WaitForSeconds(3);
        finished = true;
        coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x < 10)
        {
            transform.position += new Vector3(0.15f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
    }

    public IEnumerator Telegram(){
        telegram.SetActive(true);
        StartCoroutine(TelegramAnimation());
        Coroutine coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x > 3){
            transform.position -= new Vector3(0.14f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
        do{
            yield return null;
        }
        while(!ActionScript.sparable);
        
        coroutine = StartCoroutine(WalkingAnimation());
        while(transform.position.x < 10){
            transform.position += new Vector3(0.14f, 0);
            yield return null;
        }
        StopCoroutine(coroutine);
    }

    IEnumerator WalkingAnimation(){
        while(true){
            spriteRenderer.sprite = sprite1;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = sprite2;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator TelegramAnimation(){
        while(true){
            while(telegram.transform.rotation.z < 0.05f){
                telegram.transform.Rotate(0, 0, 0.3f);
                yield return null;
            }
            yield return new WaitForSeconds(0.06f);
            while(telegram.transform.rotation.z > -0.05f){
                telegram.transform.Rotate(0, 0, -0.3f);
                yield return null;
            }
            yield return new WaitForSeconds(0.06f);
        }
    }

}
