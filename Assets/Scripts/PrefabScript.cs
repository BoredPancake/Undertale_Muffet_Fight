using System.Collections;
using UnityEngine;

public class PrefabScript : MonoBehaviour 
{

    public string type; // Spider, Bagel, Slow, Croissant
    public string[] direction; // e.g. {"top", "left"}

    public float spiderSpeed;

    bool finishedCoroutine;
    bool finishedAttack;
    AttackScript attackScript;

    void Start()
    {
        attackScript = GameObject.Find("Attacks").GetComponent<AttackScript>();
        finishedCoroutine = true;
        if (type != "Slow"){
            switch(direction[0])
            {
                case "top":
                    transform.localPosition =  new Vector3(0, -0.45f);
                    break;
                case "middle":
                    transform.localPosition =  new Vector3(0, -1.45f);
                    break;
                case "bottom":
                    transform.localPosition =  new Vector3(0, -2.45f);
                    break;
            }

        }
        if (direction[1] == "left")
        {
            transform.localPosition -= (type == "Slow") ? new Vector3(2, 0) : new Vector3(4, 0);
        }
        else{
            transform.localPosition += (type == "Slow") ? new Vector3(2, 0) : new Vector3(4, 0);
        }
        if (type == "Croissant"){
            StartCoroutine(Croissant());
        }
    }

    void Update()
    {
        if (type == "Spider")
        {
            Spider();
        }
        
        if (type == "Donut")
        {
            Donut();
        }
        if (type == "Slow"){
            SlowSpider();
        }
        if (type == "Croissant"){
            transform.Rotate(0, 0, 5);
        }

        finishedAttack = attackScript.finishedCoroutine;
        if(finishedAttack){
            Destroy(gameObject);
        }
    }

    void Spider()
    {
        if (direction[1] == "left")
        {
            transform.localPosition += new Vector3(spiderSpeed, 0);
        }
        else{
            transform.localPosition -= new Vector3(spiderSpeed, 0);
        }
        if (Mathf.Abs(transform.localPosition.x) > 4.001f)
        {
            Destroy(gameObject);
        }
    }

    void SlowSpider(){
        if (direction[1] == "left")
        {
            transform.localPosition += new Vector3(0.03f, 0);
        }
        else{
            transform.localPosition -= new Vector3(0.03f, 0);
        }
        if (transform.localPosition.x < -2.1f)
        {
            direction[1] = "left";
        }
        else if (transform.localPosition.x > 2.1f){
            direction[1] = "right";
        }
    }

    void Donut() //I think this is actually a spider donut, don't know why I called the sprite a bagel
    {
        if (direction[1] == "left")
        {
            transform.localPosition += new Vector3(0.12f, 0);
        }
        else{
            transform.localPosition -= new Vector3(0.12f, 0);
        }
        if (Mathf.Abs(transform.localPosition.x) > 4.001f)
        {
            Destroy(gameObject);
        }
        
        if(direction[0] == "top"){
            transform.position -= new Vector3(0, 0.072f, 0);
        }
        else if (direction[0] == "bottom"){
            transform.position += new Vector3(0, 0.072f, 0);
        }
        if (transform.position.y < -2.75 && finishedCoroutine){
            StartCoroutine(Bounce("up"));
        }
        else if(transform.position.y > -0.35 && finishedCoroutine){
            StartCoroutine(Bounce("down"));
        }
    }

    IEnumerator Bounce(string upOrDown)
    {
        finishedCoroutine = false;
        yield return new WaitForEndOfFrame();
        if(upOrDown == "up"){
            direction[0] = "bottom";
        }
        else{
            direction[0] = "top";
        }
        transform.localScale = new Vector3(0.5f, 0.3f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(0.5f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        finishedCoroutine = true;
    }

    IEnumerator Croissant(){
        float speed = 0.2f;
        if(direction[1] == "left"){
            while (transform.position.x < 2){
                transform.position += new Vector3(speed, 0);
                if(transform.position.x > 0 && transform){
                    speed *= 0.917f;
                }
                yield return null;
            }
            while(transform.position.x > -4){
                transform.position -= new Vector3(speed, 0);
                if(transform.position.x > 0){
                    speed /= 0.9f;
                }
                yield return null;
            }
            Destroy(gameObject);
        }
        else{
            while (transform.position.x > -2){
                transform.position -= new Vector3(speed, 0);
                if(transform.position.x < 0){
                    speed *= 0.917f;
                }
                yield return null;
            }
            while(transform.position.x < 4){
                transform.position += new Vector3(speed, 0);
                if(transform.position.x < 0){
                    speed /= 0.9f;
                }
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
