using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightScript : MonoBehaviour
{
    public GameObject muffet;
    public GameObject target;
    public GameObject crosshair;
    public GameObject slash;
    public GameObject hpBar;
    public GameObject HP;
    public GameObject fadeAway;
    public GameObject playerHpBar;

    public Text LV;
    public Text maxPlayerHP; 

    public LittleSpider littleSpider;

    public Sprite[] slashSprites;
    Sprite playerHP;

    public GameObject flavorText;
    public Text text;

    public AudioSource audioSource;
    AudioClip slashSound;
    public AudioClip hitSound;
    public AudioClip dustSound;
    public AudioClip LV_up;

    public Animator muffetAnimator;

    public DialogueBoxScript dialogueBoxScript;

    int muffetHP;
    public int ATK;
    
    void Start(){
        muffetHP = 1250;
        ATK = 13; //let's assume the player has a toy knife, that's the easiest weapon to implement
        slashSound = audioSource.clip;
    }

    public IEnumerator Attack(){
        target.SetActive(true);
        crosshair.SetActive(true);
        flavorText.SetActive(false);
        crosshair.transform.position = new Vector3(-5.75f, -1.48f);
        crosshair.GetComponent<SpriteRenderer>().color = new Color(1,1,1f);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        do{
            crosshair.transform.position += new Vector3(0.25f, 0);
            if(crosshair.transform.position.x > 5.7f){
                text.text = "MISS";
                text.color = new Color(0.7f, 0.7f, 0.7f);
                text.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
                target.SetActive(false);
                crosshair.SetActive(false);
                text.gameObject.SetActive(false);
                MainScript.ChangeTurn(GameObject.Find("Dialogue box").GetComponent<DialogueBoxScript>());
                flavorText.SetActive(true);
                yield break;
            }

            yield return null;
        }
        while(!Input.GetKeyDown("z"));
        Coroutine coroutine = StartCoroutine(CrosshairBlink());
        audioSource.clip = slashSound;
        audioSource.Play();
        StartCoroutine(SlashAnimation());
        yield return new WaitForSeconds(1);
        Hit();
        yield return null;

        float hpProportion = muffetHP / 1250f;
        while(HP.transform.localScale.x / 8.9f > hpProportion){
            if(HP.transform.localScale.x < 0)
            {
                HP.SetActive(false);
                break;
            }
            if(muffetHP <= 0){
                HP.transform.localScale -= new Vector3(0.3f, 0);
            }
            else{
                HP.transform.localScale -= new Vector3(0.03f, 0);
            }
            yield return null;
        }
        yield return new WaitForSeconds(1);

        target.SetActive(false);
        crosshair.SetActive(false);
        text.gameObject.SetActive(false);
        hpBar.SetActive(false);
        StopCoroutine(coroutine);
        if(muffetHP <= 0){
            StartCoroutine(Death());
        }
        else{
            muffetAnimator.SetInteger("MuffetState", (int)AttackScript.MuffetStates.IDLE);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
            MainScript.ChangeTurn(GameObject.Find("Dialogue box").GetComponent<DialogueBoxScript>());
            flavorText.SetActive(true);
        }
    }


    [ContextMenu("Hit")]
    void Hit(){
        muffetAnimator.SetInteger("MuffetState", (int)AttackScript.MuffetStates.DAMAGED);
        int damage;
        int distance = (int)Mathf.Abs(crosshair.transform.position.x) * 16; //unity distance unit is 16 pixels according to my 15 second research
        if(distance > 12){
            damage = (int)Mathf.Round((ATK + Random.Range(0, 2f)) * (1 - (distance / 16 / target.GetComponent<SpriteRenderer>().bounds.size.x)) * 2);
        }
        else{
            damage = (int)Mathf.Round((ATK + Random.Range(0, 2f)) * 2.2f);
        }
        audioSource.clip = hitSound;
        audioSource.Play();
        text.text = damage.ToString();
        text.color = Color.red;
        text.gameObject.SetActive(true);
        hpBar.SetActive(true);
        muffetHP -= damage;
    }

    IEnumerator Death(){
        ActionScript actionScript = GetComponent<ActionScript>();
        actionScript.music.Stop();
        audioSource.clip = dustSound;
        audioSource.Play();
        while(fadeAway.transform.position.y > 5){
            fadeAway.transform.position -= new Vector3(0, 0.2f);
            yield return null;
        }
        muffet.SetActive(false);
        yield return null;
        fadeAway.SetActive(false);
        StartCoroutine(littleSpider.Flower());
        while(!littleSpider.finished){
            yield return null;
        }

        PurpleSoulScript purpleSoulScript = GetComponent<PurpleSoulScript>();
        maxPlayerHP.text = purpleSoulScript.hp.ToString() + " / 44";
        LV.text = "lv 7";
        playerHP = purpleSoulScript.hpBarSprites[purpleSoulScript.hp / 2];
        Destroy(purpleSoulScript);
        playerHpBar.GetComponent<SpriteRenderer>().sprite = playerHP;

        audioSource.clip = LV_up;
        audioSource.Play();
        
        flavorText.SetActive(true);
        StartCoroutine(dialogueBoxScript.Write("* YOU WON!\n* You earned 300 XP and 0 gold.\n* Your LOVE increased."));
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(2);
    }

    IEnumerator CrosshairBlink(){
        while(true){
            crosshair.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
            yield return new WaitForSeconds(0.1f);
            crosshair.GetComponent<SpriteRenderer>().color = new Color(1,1,1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SlashAnimation(){
        slash.SetActive(true);
        SpriteRenderer renderer = slash.GetComponent<SpriteRenderer>();
        renderer.sprite = slashSprites[0];
        for(int i = 1; i < slashSprites.Length - 1; i++){
            renderer.sprite = slashSprites[i];
            yield return new WaitForSeconds(0.14f);
        }
        slash.SetActive(false);
    }
}
