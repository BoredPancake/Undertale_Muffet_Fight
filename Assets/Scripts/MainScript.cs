using System.Collections;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public static string turn ;
    public static int FAIM; //Fight, Act, Item ,Mercy

    public static GameObject player;

    static AudioSource soundEffect;
    public AudioClip audioClip1;
    static AudioClip changeActionSfx;

    
    void Start()
    {
        turn = "Player";
        FAIM = 1;
        player = GameObject.FindWithTag("Player");
        Application.targetFrameRate = 30;
        soundEffect = GetComponents<AudioSource>()[1];
        changeActionSfx = audioClip1;
    }

    
    void Update()
    {
    }

    public static void ChangeTurn(DialogueBoxScript dialogueBoxScript)
    {
        IEnumerator coroutine;
        if (turn == "Player")
        {
            coroutine = dialogueBoxScript.ChangeScale(0);
            dialogueBoxScript.StartCoroutine(dialogueBoxScript.Say(dialogueBoxScript.currentQuote));
            dialogueBoxScript.StartCoroutine(coroutine);
            turn = "Enemy";
            player.transform.position = new Vector3(0, -1.5f, 0);
            
        }
        else
        {
            coroutine = dialogueBoxScript.ChangeScale(1);
            dialogueBoxScript.StartCoroutine(coroutine);
            ActionScript actionScript = GameObject.Find("Player").GetComponent<ActionScript>();
            actionScript.selecting = true;
            player.GetComponent<PurpleSoulScript>().reducedDamage = 0;
            turn = "Player";
        }
    }
    public static void ChooseAction()
    {
        if (Input.GetKeyDown("right")) {
            //print("right arrow pressed");
            
            soundEffect.clip = changeActionSfx;
            soundEffect.Play();
            if (FAIM == 4)
            {
                FAIM = 1;
            }
            else
            {
                FAIM++;
            }
        }
        else if (Input.GetKeyDown("left")) {
            //print("left arrow pressed");
            
            soundEffect.clip = changeActionSfx;
            soundEffect.Play();
            if (FAIM == 1)
            {
                FAIM = 4;
            }
            else
            {
                FAIM--;
            }
        }
    }
}
