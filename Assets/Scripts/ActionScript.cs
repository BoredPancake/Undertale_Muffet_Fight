using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{
    public GameObject muffet;
    public GameObject particles;

    DialogueBoxScript dialogueBoxScript;
    public GameObject dialogueBox;
    public Text flavorText;
    public Text money;
    string currentText;

    public Animator animator;

    public AudioSource audioSource;
    public AudioClip cat;
    AudioClip regularSound;
    public AudioSource music;
    public AudioClip victory;

    public bool selecting;
    public static bool sparable;
    int FAIM;
    Food[] items;
    
    int struggles;
    bool paidOnce;
    int goldToPay;


    class Food{
        public string name;
        public string shortName;
        public int hpToHeal;
        public Food(string longName, string sName, int hp){
            name = longName;
            shortName = sName;
            hpToHeal = hp;
        }
    }
    
    void Start()
    {
        regularSound = audioSource.clip;
        selecting = true;
        dialogueBoxScript = dialogueBox.GetComponent<DialogueBoxScript>();
        items = new Food[] {new Food("Instant Noodles", "InstaNood", 4), new Food("Hot Cat", "Hot Cat", 21), new Food("Crab Apple", "CrabApple", 18), new Food("Cinnamon Bunny", "CinnaBun", 22)};
        struggles = 0;
        paidOnce = false;
        goldToPay = 10;
    }

    void Update()
    {
        currentText = dialogueBoxScript.currentText;
        FAIM = MainScript.FAIM;
        if (MainScript.turn == "Player" && selecting)
        {
            MainScript.ChooseAction();
            switch (FAIM)
            {
                case 1:
                    transform.position = new Vector2(-5.4f, -4.4f);
                    break;
                case 2:
                    transform.position = new Vector2(-2.2f, -4.4f);
                    break;
                case 3:
                    transform.position = new Vector2(0.73f, -4.4f);
                    break;
                case 4:
                    transform.position = new Vector2(3.85f, -4.4f);
                    break;
            }
            
            if (Input.GetKeyDown("z"))
            {
                selecting = false;
                dialogueBoxScript.StopWriting();
                switch (FAIM)
                {
                    case 1:
                        StartCoroutine(Fight());
                        break;
                    case 2:
                        StartCoroutine(Act());
                        break;
                    case 3:
                        if(items.Length > 0){
                            StartCoroutine(Item());
                        }
                        else{
                            selecting = true;
                            flavorText.text = currentText;
                        }
                        break;
                    case 4: 
                        StartCoroutine(Mercy());
                        break;
                }
            }
        }
    }

    IEnumerator Fight()
    {
        if(sparable)
        {
            GetComponent<FightScript>().ATK = 1499;
            flavorText.color = Color.yellow;
        }
        transform.position = new Vector2(-5, -0.5f);
        flavorText.text = "   Muffet";
        yield return null;
        while(!Input.GetKeyDown("x")){
             if(Input.GetKeyDown("z"))
             {
                flavorText.color = Color.white;
                StartCoroutine(GetComponent<FightScript>().Attack());
                yield break;
             }
             yield return null;
        }
        flavorText.color = Color.white;
        selecting = true;
        flavorText.text = currentText;
    }

    IEnumerator Act()
    {
        int currentAct = 1;
        money.gameObject.SetActive(true);

        if(sparable){
            struggles = 3;
        }

        while(!Input.GetKeyDown("x")){
            yield return null;
            flavorText.text = "";

            if(Input.GetKeyDown("left") || Input.GetKeyDown("right") ){
                if(currentAct % 2 == 0){
                    currentAct--;
                }
                else if(currentAct + 1 <= 3){
                    currentAct++;
                }
            }
            if(Input.GetKeyDown("down") || Input.GetKeyDown("up")){
                if(currentAct > 2){
                    currentAct -= 2;
                }
                else if(currentAct + 2 <= 3){
                    currentAct += 2;
                }
            }

            switch(currentAct){
                case 1:
                    transform.position = new Vector2(-5.2f, -0.5f);
                    flavorText.text = "  Check         * Struggle\n* Pay " + goldToPay.ToString() + "G";
                    break;
                case 2:
                    transform.position = new Vector2(0.12f, -0.5f);
                    flavorText.text = "* Check           Struggle\n* Pay " + goldToPay.ToString() + "G";
                    break;
                case 3: 
                    transform.position = new Vector2(-5.2f, -1.2f);
                    flavorText.text = "* Check         * Struggle\n  Pay " + goldToPay.ToString() + "G";
                    break;
            }
            if(Input.GetKeyDown("z")){
                money.gameObject.SetActive(false);
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
                switch(currentAct){
                    case 1:
                        StartCoroutine(Act("check"));
                        break;
                    case 2:
                        StartCoroutine(Act("struggle"));
                        if(struggles == 2){
                            yield return new WaitForSeconds(3.1f);
                            do{
                                yield return null;
                            }
                            while(!Input.GetKeyDown("z"));
                            yield return null;
                        }
                        break;
                    case 3:
                        StartCoroutine(Act("pay"));
                        // I didn't have many references and I didn't feel like getting to Muffet myself
                        // so I just decided to give player 0G so there are less options for the "pay" act
                        if(!paidOnce){
                            yield return new WaitForSeconds(4.1f);
                            do{
                                yield return null;
                            }
                            while(!Input.GetKeyDown("z"));
                            yield return null;
                        }
                        break;
                }
                do{
                    yield return null;
                }
                while(!Input.GetKeyDown("z"));
                MainScript.ChangeTurn(dialogueBoxScript);
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
                yield break;
            }
        }
        selecting = true;
        money.gameObject.SetActive(false);
        flavorText.text = currentText;
    }

    IEnumerator Item()
    {
        int currentItem = 1;
        string[] itemNames = new string[4];
        audioSource.clip = regularSound;

        yield return null;

        while(!Input.GetKeyDown("x")){
            flavorText.text = "";

            if(Input.GetKeyDown("left") || Input.GetKeyDown("right") ){
                if(currentItem % 2 == 0){
                    currentItem--;
                }
                else if(items.Length >= currentItem + 1){
                    currentItem++;
                }
            }
            if(Input.GetKeyDown("down") || Input.GetKeyDown("up")){
                if(currentItem > 2){
                    currentItem -= 2;
                }
                else if(items.Length >= currentItem + 2){
                    currentItem += 2;
                }
            }

            switch(currentItem){
                case 1:
                    transform.position = new Vector2(-5.2f, -0.5f);
                    break;
                case 2:
                    transform.position = new Vector2(0.12f, -0.5f);
                    break;
                case 3: 
                    transform.position = new Vector2(-5.2f, -1.2f);
                    break;
                case 4:
                    transform.position = new Vector2(0.12f, -1.2f);
                    break;
            }
            for(int i = 0; i < 4; i++){
                itemNames[i] = "";
            }
            for(int i = 0; i < items.Length; i++){
                if(currentItem - 1 == i){
                    itemNames[i] = "  " + items[i].shortName;
                }
                else{
                    itemNames[i] = "* " + items[i].shortName;
                }
                
            }

            for(int i = 0; i < 4; i++)
            {
                flavorText.text += itemNames[i];
                if(i % 2 == 0){
                    for(int j = 0; j < 16 - itemNames[i].Length; j++){
                        flavorText.text += " ";
                    }
                }
                else{
                    flavorText.text += "\n";
                }
            }
            
            if(Input.GetKeyDown("z")){
                Food item = items[currentItem - 1];
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
                if(item.name == "Instant Noodles"){
                    StartCoroutine(Noodles(item));
                }
                else{
                    Eat(item);
                    yield return null;
                    dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
                    do{
                        yield return null;
                    }
                    while(!Input.GetKeyDown("z"));
                    MainScript.ChangeTurn(dialogueBoxScript);
                    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
                }
                yield break;
            }

            yield return null;
        }
        selecting = true;
        flavorText.text = currentText;
    }

    IEnumerator Mercy()
    {
        transform.position = new Vector2(-5, -0.5f);
        flavorText.text = "   Spare";

        if(sparable){
            flavorText.color = Color.yellow;
        }
        yield return null;
        while(!Input.GetKeyDown("x")){
             if(Input.GetKeyDown("z"))
             {
                if(sparable)
                {
                    GetComponent<SpriteRenderer>().sprite = null;
                    animator.SetInteger("MuffetState", (int)AttackScript.MuffetStates.DAMAGED);
                    Color color = muffet.GetComponent<SpriteRenderer>().color;
                    muffet.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.4f);
                    particles.SetActive(true);
                    flavorText.color = Color.white;
                    music.Stop();
                    audioSource.clip = victory;
                    audioSource.Play();
                    StartCoroutine(dialogueBoxScript.Write("* YOU WON!\n* You earned 0 XP and 0 gold."));
                    yield return new WaitForSeconds(3);
                    do{
                        yield return null;
                    }
                    while(Input.GetKeyDown("z"));
                    SceneManager.LoadScene(1);
                }
                else{
                    yield return null;
                    MainScript.ChangeTurn(dialogueBoxScript);
                }
                yield break;
             }
             yield return null;
        }
        selecting = true;
        flavorText.color = Color.white;
        flavorText.text = currentText;
    }

    IEnumerator Act(string name){
        switch(name){
            case "check":
                StartCoroutine(dialogueBoxScript.Write("* MUFFET 8 ATK 0 DEF\n* If she invites you to her\n  parlor, excuse yourself."));
                break;
            case "struggle":
                switch(struggles){
                    case 0:
                        StartCoroutine(dialogueBoxScript.Write("* You struggle to escape the web.\n* Muffet covers her mouth\n  and giggles at you."));
                        break;
                    case 1:
                        StartCoroutine(dialogueBoxScript.Write("* You struggle to escape the web.\n* Muffet laughs and claps\n  her hands."));
                        break;
                    case 2:
                        StartCoroutine(dialogueBoxScript.Write("* You struggle to escape the web.\n"));
                        yield return new WaitForSeconds(3);
                        while (!Input.GetKeyDown("z")){
                            yield return null;
                        }
                        StartCoroutine(dialogueBoxScript.Write("* Muffet is so amused by\n  your antics that she gives\n  you a discount!"));
                        goldToPay = 5;
                        break;
                    default:
                        StartCoroutine(dialogueBoxScript.Write("* You struggle to escape the web.\n  Nothing happened."));
                        break;
                }
                struggles++;
                break;
            case "pay":
                if(sparable){
                    StartCoroutine(dialogueBoxScript.Write("* Muffet refuses your money."));
                    break;
                }
                if(paidOnce){
                    StartCoroutine(dialogueBoxScript.Write("* You're out of money.\n  Muffet shakes her head."));
                    break;
                }
                StartCoroutine(dialogueBoxScript.Write("* You empty your pockets...\n  But you don't have\n  any money at all!"));
                yield return new WaitForSeconds(4);
                while (!Input.GetKeyDown("z")){
                    yield return null;
                }
                StartCoroutine(dialogueBoxScript.Write("* Muffet takes pity on\n  you and reduces her\n  ATTACK for this turn."));
                GetComponent<PurpleSoulScript>().reducedDamage = 1;
                    paidOnce = true;
                break;
        }
    }


    IEnumerator Noodles(Food noodles){
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        StartCoroutine(dialogueBoxScript.Write("* You remove the Instant\n  Noodles from their\n  packaging."));
        yield return new WaitForSeconds(3.3f);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));
        StartCoroutine(dialogueBoxScript.Write("* You put some water in\n  the pot and place it on\n  the heat."));
        yield return new WaitForSeconds(3.3f);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));
        StartCoroutine(dialogueBoxScript.Write("* You wait for the water\n  to boil..."));
        yield return new WaitForSeconds(3);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        music.Pause();
        StartCoroutine(dialogueBoxScript.Write("* ...\n"));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(dialogueBoxScript.Write("* ...\n", false));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(dialogueBoxScript.Write("* ...", false));
        yield return new WaitForSeconds(0.3f);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        StartCoroutine(dialogueBoxScript.Write("* It's "));
        yield return new WaitForSeconds(1);
        StartCoroutine(dialogueBoxScript.Write("boiling.", false));
        yield return new WaitForSeconds(1);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        StartCoroutine(dialogueBoxScript.Write("* You place the noodles\n"));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(dialogueBoxScript.Write("  into the pot.", false));
        yield return new WaitForSeconds(1);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        for(int i = 4; i > 0; i--){
            StartCoroutine(dialogueBoxScript.Write("* " + i.ToString()));
            yield return new WaitForSeconds(1.2f);
            if(i == 1){
                StartCoroutine(dialogueBoxScript.Write(" minute left ", false));
            }
            else{
                StartCoroutine(dialogueBoxScript.Write(" minutes left ", false));
            }
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(dialogueBoxScript.Write("until\n  the noodles ", false));
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(dialogueBoxScript.Write("are finished.", false));
            yield return new WaitForSeconds(1.5f);
            do
            {
                yield return null;
            }
            while(!Input.GetKeyDown("z"));
        }
        StartCoroutine(dialogueBoxScript.Write("* The noodles "));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(dialogueBoxScript.Write("are finished.", false));
        yield return new WaitForSeconds(1.5f);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        StartCoroutine(dialogueBoxScript.Write("* ...they don't taste very\n  good."));
        yield return new WaitForSeconds(2);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        StartCoroutine(dialogueBoxScript.Write("* You add the flavor packet."));
        yield return new WaitForSeconds(2);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        StartCoroutine(dialogueBoxScript.Write("* That's better."));
        yield return new WaitForSeconds(2);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));

        StartCoroutine(dialogueBoxScript.Write("* Not great, but better."));
        yield return new WaitForSeconds(2);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));
        music.Play();
        Eat(noodles);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        yield return new WaitForSeconds(3);
        do
        {
            yield return null;
        }
        while(!Input.GetKeyDown("z"));
        MainScript.ChangeTurn(dialogueBoxScript);
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
    }



    void Eat(Food food){
        int hp = gameObject.GetComponent<PurpleSoulScript>().hp;
        string output;

        if(hp + food.hpToHeal < 20){
            gameObject.GetComponent<PurpleSoulScript>().hp += food.hpToHeal;
            output = "* You recovered " + food.hpToHeal.ToString() + " hp.";
        }
        else{
            gameObject.GetComponent<PurpleSoulScript>().hp = 20;
            output = "* Your HP was maxed out.";
        }
        if(food.name == "Hot Cat"){
            audioSource.clip = cat;
            audioSource.Play();
        }
        else{
            audioSource.Play();
        }
        //copied this part from stack overflow
        int index = System.Array.IndexOf(items, food);
        List<Food> list = new List<Food>(items);
        list.RemoveAt(index);
        items = list.ToArray();

        // Instant Noodles apparently have "You ate" instead of "You eat". That's strange.
        if(food.name == "Instant Noodles"){
            dialogueBoxScript.writing = dialogueBoxScript.Write("* You ate the Instant Noodles.\n" + output);
        }
        else{
            dialogueBoxScript.writing = dialogueBoxScript.Write("* You eat the " + food.name + ".\n" + output);
        }
        
    }
}
