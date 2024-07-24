using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    GameObject gameOverText;
    GameObject player;
    public GameObject shard;

    GameObject[] allGameObjects;

    public Text asgoreText;
    
    // The text wrapping is weird, so I had to use invisible characters
    string[] quotes;

    public Sprite brokenSoul;
    AudioSource determination;
    public AudioSource breakingSound;
    public AudioSource asgoreVoice;

    public AudioClip break2;

    bool finishedSpeaking;

    void Start()
    {
        quotes = new string[] {"Y o u   c a n n o t   g i v e\nu p   j u s t   y e t . . .", "D o n ' t   l o s e   h o p e !", 
    "T h e   f u t u r e   o f\nm o n s t e r s\nd e p e n d s   o n   y o u !"}; 

        asgoreText.text = "";

        determination = GetComponent<AudioSource>();

        MainScript.turn = "GameOver";
        allGameObjects = FindObjectsOfType<GameObject>();
        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PurpleSoulScript>().redSoul;
        player.GetComponent<SpriteRenderer>().sortingLayerName = "Game Over";
        player.GetComponent<SpriteRenderer>().sortingOrder = 1;
        player.transform.parent.eulerAngles = new Vector3(0, 0, 0);
        GameObject.Find("Canvas").SetActive(false);

        foreach(GameObject gameObj in allGameObjects){
            if(gameObj != gameObject){
                foreach (AudioSource source in gameObj.GetComponents<AudioSource>())
                {
                    Destroy(source);
                }
                gameObj.SendMessage("StopAllCoroutines", SendMessageOptions.DontRequireReceiver);
            }
        }
        StartCoroutine(GameOver());
        
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        breakingSound.Play();
        player.GetComponent<SpriteRenderer>().sprite = brokenSoul;
        yield return new WaitForSeconds(1.3f);

        breakingSound.clip = break2;
        breakingSound.Play();
        Destroy(player.GetComponent<SpriteRenderer>());

        for(int i = 0; i < UnityEngine.Random.Range(4, 6); i++){
            Instantiate(shard, player.transform.position, player.transform.rotation);
        }

        yield return new WaitForSeconds(1);

        determination.Play();

        gameOverText = transform.GetChild(1).GetChild(0).gameObject;
        transform.GetChild(1).gameObject.SetActive(true);
        gameOverText.SetActive(true);
        Text text = gameOverText.GetComponent<Text>();
        text.color = new Color(255, 255, 255, 0);
        while(text.color.a < 1){
            text.color += new Color(0, 0, 0, 0.02f);
            yield return null;
        }
        StartCoroutine(Write(quotes[Random.Range(0, 3)]));
        do{
            yield return null;
        }
        while(!finishedSpeaking);
        yield return new WaitForSeconds(1);
        while(!Input.GetKeyDown("z"))
        {
            yield return null;
        }
        asgoreText.text = "";
        StartCoroutine(Write("P a n c a k e !\n"));
        do{
            yield return null;
        }
        while(!finishedSpeaking);
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(Write("S t a y   d e t e r m i n e d . . ."));
        yield return new WaitForSeconds(1.5f);
        while(!Input.GetKeyDown("z"))
        {
            yield return null;
        }
        asgoreText.text = "";
        yield return new WaitForSeconds(1.5f);
        while(determination.volume > 0){
            text.color -= new Color(0, 0, 0, 0.01f);
            determination.volume -= 0.002f;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }

    IEnumerator Write(string text)
    {
        finishedSpeaking = false;
        foreach(char character in text)
        {
            asgoreText.text += character;
            if(character != '\n' && character != ' '){
                asgoreVoice.Play();
                yield return new WaitForSeconds(0.08f);
            }
        }
        finishedSpeaking = true;
    }

}
