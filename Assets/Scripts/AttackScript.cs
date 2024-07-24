using System.Collections;
using UnityEngine;

public class AttackScript : MainScript // For your own safety, don't even try to read this code.
{
    public GameObject lanes;
    public GameObject substance;
    public GameObject purple;
    public GameObject spider;
    public GameObject donut;
    public GameObject croissant;
    public GameObject pet;
    public GameObject slowSpider;
    GameObject muffet;
    GameObject web;
    string[] flavorTexts;

    public Animator muffetAnimator;

    Sprite petAppearing;
    public Sprite petAppearing2;
    public Sprite[] petSprites;

    DialogueBoxScript dialogueBoxScript;
    public GameObject dialogueBox;
    GameObject arena;
    MovementScript movementScript;

    public LittleSpider spiderScript;

    public bool finishedCoroutine {get; set;}

    public enum MuffetStates{
        IDLE,
        POURING,
        DAMAGED
    }

    void Start()
    {
        muffet = GameObject.Find("Muffet");
        web = lanes.transform.GetChild(0).gameObject;
        player = GameObject.FindWithTag("Player");
        movementScript = player.GetComponent<MovementScript>();
        arena = player.transform.parent.gameObject;
        petAppearing = pet.GetComponent<SpriteRenderer>().sprite;
        dialogueBoxScript = dialogueBox.GetComponent<DialogueBoxScript>();
        StartCoroutine(MainCoroutine());

        flavorTexts = new string[] {"* Muffet pours you a cup of\n  spiders.", "* All the spiders clap along to\n  the music.",
        "* Muffet does a synchronized\n  dance with the other spiders.", "* Muffet tidies up the web\n  around you.", 
        "* Smells like freshly baked\n  cobwebs."};
    }

    IEnumerator MainCoroutine()
    {
        // there are so many ways i could make this code better
        // but i don't really feel like it
        while(turn == "Player" || !dialogueBoxScript.finishedSpeaking){
            yield return null;
        }
        StartCoroutine(Purple());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false); // Write once, copy and paste everywhere... it's not really a good idea, but it works
        dialogueBoxScript.currentText = "* You're trapped in a strange\n  purple web!";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Why so pale? \nYou should be proud~";
        StartCoroutine(spiderScript.ShowNextAttack("1 spider"));

        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack1());
        do{
            yield return null;
        }
        while(!finishedCoroutine);

        
        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Proud that you're\ngoing to make a\ndelicious cake~\nAhuhuhu~";
        StartCoroutine(spiderScript.ShowNextAttack("1 spider"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack2());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Let you go? \nDon't be silly~";
        StartCoroutine(spiderScript.ShowNextAttack("2 spiders"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack3());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Your SOUL is going to make every spider very happy~~~";
        StartCoroutine(spiderScript.ShowNextAttack("spider, donut"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack4());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Oh, how rude of me! I almost forgot to introduce you to my pet~";
        StartCoroutine(spiderScript.ShowNextAttack("cupcake"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Pet(1));
        do{
            yield return null;
        }
        while(!finishedCoroutine);
        ChangeTurn(dialogueBoxScript);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "The person who warned us about you...";
        StartCoroutine(spiderScript.ShowNextAttack("spider, donut"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack6());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Offered us a LOT of money for your SOUL.";
        StartCoroutine(spiderScript.ShowNextAttack("1 spider"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack7());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "They had such a sweet smile~ and... ahuhu~ ";
        StartCoroutine(spiderScript.ShowNextAttack("spider, donut"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack8());
        do{
            yield return null;
        }
        while(!finishedCoroutine);
        
        
        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "It's strange, but I swore I saw them in the shadows... Changing shape...?";
        StartCoroutine(spiderScript.ShowNextAttack("croissant"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack9());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Oh, it's lunch time, isn't it? And I forgot to feed my pet~";
        StartCoroutine(spiderScript.ShowNextAttack("cupcake"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Pet(2));
        do{
            yield return null;
        }
        while(!finishedCoroutine);
        ChangeTurn(dialogueBoxScript);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "With that money, the spider clans can finally be reunited~";
        StartCoroutine(spiderScript.ShowNextAttack("1 spider"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack11());
        do{
            yield return null;
        }
        while(!finishedCoroutine);

        
        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "You haven't heard? Spiders have been trapped in the RUINS for generations!";
        StartCoroutine(spiderScript.ShowNextAttack("donuts"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack12());
        do{
            yield return null;
        }
        while(!finishedCoroutine);

        
        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Even if they go under the door, Snowdin's fatal cold is impassable alone.";
        StartCoroutine(spiderScript.ShowNextAttack("croissants"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack13());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "But with the money from your SOUL, we'll be able to rent them a heated limo~";
        StartCoroutine(spiderScript.ShowNextAttack("2 spiders"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack14());
        do{
            yield return null;
        }
        while(!finishedCoroutine);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "And with all of the leftovers...? We could have a nice vacation~";
        StartCoroutine(spiderScript.ShowNextAttack("final"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Attack15());
        do{
            yield return null;
        }
        while(!finishedCoroutine);

        
        lanes.SetActive(false);
        dialogueBoxScript.currentText = flavorTexts[Random.Range(0, flavorTexts.Length)];
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "But enough of that... It's time for dinner, isn't it? Ahuhuhu~";
        StartCoroutine(spiderScript.ShowNextAttack("cupcake"));
        while(turn == "Player"){
            yield return null;
        }
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(Pet(3));
        do{
            yield return null;
        }
        while(!finishedCoroutine);
        StartCoroutine(dialogueBoxScript.Say("You're still alive? Ahuhuhu~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("Oh, my pet~ Looks like it's time for dessert~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(spiderScript.Telegram());
        yield return new WaitForSeconds(1);
        StartCoroutine(dialogueBoxScript.Say("Huh? A telegram from the spiders in the RUINS?"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("What? They're saying that they saw you, and..."));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("... even if you are stingy, you never hurt a single spider! "));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("Oh my, this has all been a big misunderstanding~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("I thought you were someone that hated spiders~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("The person who asked for that SOUL..."));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("They must have meant a DIFFERENT human in a striped shirt~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("Sorry for all the trouble~ Ahuhuhu~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("I'll make it up to you~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("You can come back here any time... And, for no charge at all..."));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("I'll wrap you up and let you play with my pet again!"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("Ahuhuhuhuhuhu~ Just kidding~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        StartCoroutine(dialogueBoxScript.Say("I'll SPARE you now~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        ChangeTurn(dialogueBoxScript);


        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        ActionScript.sparable = true;
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Ahuhuhu~ What are you doing~";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
        

        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "It's time to go~";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
        

        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Feeling comfortable trapped in that web?";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
        

        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Ahuhuhuhu~ Well, I don't mind keeping you here~";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
        

        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "If you don't mind being gobbled up~ Ahuhuhu~ ";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
        

        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "Just kidding, of course~";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
        

        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "... well... maybe ONE little nibble~~";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
        

        lanes.SetActive(false);
        dialogueBoxScript.currentText = "* Muffet is sparing you.";
        dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
        dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
        dialogueBoxScript.currentQuote = "No, no, it's time to go~";
        while(turn == "Player"){
            yield return null;
        }
        dialogueBoxScript.StopWriting();
        lanes.SetActive(true);
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3);
        ChangeTurn(dialogueBoxScript);
            
        while(true){
            lanes.SetActive(false);
            dialogueBoxScript.currentText = "* Muffet is sparing you.";
            dialogueBoxScript.writing = dialogueBoxScript.Write(dialogueBoxScript.currentText);
            dialogueBoxScript.StartCoroutine(dialogueBoxScript.writing);
            dialogueBoxScript.currentQuote = "...";
            while(turn == "Player"){
                yield return null;
            }
            dialogueBoxScript.StopWriting();
            lanes.SetActive(true);
            while(!dialogueBoxScript.finishedSpeaking)
            {
                yield return null;
            }
            yield return new WaitForSeconds(3);
            ChangeTurn(dialogueBoxScript);
        }
    }







    IEnumerator Purple()
    {
        finishedCoroutine = false;
        muffetAnimator.SetInteger("MuffetState", (int)MuffetStates.POURING);
        StartCoroutine(SpawnSubstance());
        yield return new WaitForSeconds(1f);
        substance.SetActive(true);
        
        while (substance.transform.localScale.y < 2.82f)
        {
            substance.transform.localScale += new Vector3(0, 0.15f, 0);
            substance.transform.position += new Vector3(0, 0.074f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        muffetAnimator.SetInteger("MuffetState", (int)MuffetStates.IDLE);
        movementScript.ChangeSoul();
        yield return new WaitForSeconds(1.5f);
        Color color = substance.GetComponent<Renderer>().material.color;
        for (float i = 1.0f; i > 0; i -= 0.03f)
        {
            color.a = i;
            substance.GetComponent<Renderer>().material.SetColor("_Color", color);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(dialogueBoxScript.Say("...I think purple is a better look on you! Ahuhuhu~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }
        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator SpawnSubstance(){
        for(int i = 0; i < 20; i++){
            StartCoroutine(PurpleSubstance());
            yield return new WaitForSeconds(0.12f);
        }
    }

    IEnumerator Attack1()
    {
        finishedCoroutine = false;

        Spider(new string[]{"top", "right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top", "right"});
        yield return new WaitForSeconds(0.4f);

        Spider(new string[]{"bottom", "right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom", "right"});
        yield return new WaitForSeconds(0.6f);

        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.4f);

        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack2()
    {
        finishedCoroutine = false;

        Spider(new string[]{"middle", "right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom", "right"});
        yield return new WaitForSeconds(0.4f);

        Spider(new string[]{"top", "right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle", "right"});
        yield return new WaitForSeconds(0.4f);

        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.6f);

        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.6f);

        Spider(new string[]{"middle", "right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom", "right"});
        yield return new WaitForSeconds(0.4f);

        Spider(new string[]{"top", "right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle", "right"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }
    IEnumerator Attack3()
    {
        finishedCoroutine = false;

        Spider(new string[]{"middle", "right"});
        Spider(new string[]{"bottom", "right"});
        yield return new WaitForSeconds(0.4f);

        Spider(new string[]{"top", "right"});
        yield return new WaitForSeconds(0.6f);

        Spider(new string[]{"top", "right"});
        Spider(new string[]{"middle", "right"});        
        yield return new WaitForSeconds(0.4f);

        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.8f);

        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"top","left"});
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.8f);

        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"top","right"});
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack4()
    {
        finishedCoroutine = false;

        Spider(new string[]{"top", "right"});
        yield return new WaitForSeconds(0.4f);
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.4f);
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.8f);

        Spider(new string[]{"top","left"});
        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"middle","left"});
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"top","left"});
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.6f);

        Donut(new string[] {"top", "left"});
        yield return new WaitForSeconds(0.6f);
        Donut(new string[] {"bottom", "left"});
        yield return new WaitForSeconds(0.6f);
        Donut(new string[] {"top", "left"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack5() // spiders appearing when the arena moves towards the pet
    {
        for(int i = 0; i < 7; i++){
            Spider(new string[]{"top","right"});
            Spider(new string[]{"bottom","right"});
            yield return new WaitForSeconds(0.6f);
            Spider(new string[]{"middle","right"});
            yield return new WaitForSeconds(0.6f);
        }
        Spider(new string[]{"top","right"});
        Spider(new string[]{"bottom","right"});
    }

    IEnumerator Attack6(){

        finishedCoroutine = false;

        Spider(new string[]{"top","right"});
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.6f);
        Donut(new string[]{"top","right"});
        Donut(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.6f);
        Donut(new string[]{"top","left"});
        Donut(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"middle","right"});
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.6f);
        Donut(new string[]{"top","right"});
        Donut(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"top","left"});
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack7(){

        finishedCoroutine = false;

        Spider(new string[]{"top","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack8(){
        finishedCoroutine = false;
        
        Donut(new string[]{"top","left"});
        Donut(new string[]{"top","right"});
        Donut(new string[]{"bottom","left"});
        Donut(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.6f);
        Spider(new string[]{"top","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.6f);

        Croissant(new string[]{"middle","left"});
        yield return new WaitForSeconds(3);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack9(){
        finishedCoroutine = false;
        Croissant(new string[]{"top","left"});
        Croissant(new string[]{"bottom","left"});
        yield return new WaitForSeconds(1);
        Croissant(new string[]{"middle","left"});
        yield return new WaitForSeconds(1);
        Croissant(new string[]{"top","right"});
        Croissant(new string[]{"bottom","right"});
        yield return new WaitForSeconds(1);
        Croissant(new string[]{"middle","right"});
        yield return new WaitForSeconds(4);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }
    
    IEnumerator Attack10(){ //during Pet(2)
        for(int i = 0; i < 3; i++){
            Spider(new string[]{"bottom","right"});
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"top","right"});
            Spider(new string[]{"middle","right"});
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"top","right"});
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"bottom","right"});
            Spider(new string[]{"middle","right"});
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(0.6f);
        for(int i = 0; i < 2; i++){
            Spider(new string[]{"middle","right"});
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"top","right"});
            Spider(new string[]{"bottom","right"});
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator Attack11(){

        finishedCoroutine = false;

        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","left"});
        Spider(new string[]{"top","right"});
        Spider(new string[]{"bottom","left"});
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack12(){

        finishedCoroutine = false;

        Donut(new string[]{"middle","left"});
        Donut(new string[]{"top","left"});
        yield return new WaitForSeconds(0.8f);
        Donut(new string[]{"middle","right"});
        Donut(new string[]{"top","right"});
        yield return new WaitForSeconds(0.8f);
        Donut(new string[]{"middle","left"});
        Donut(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.8f);
        Donut(new string[]{"middle","right"});
        Donut(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.8f);
        Donut(new string[]{"top","left"});
        Donut(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.8f);
        Donut(new string[]{"top","right"});
        Donut(new string[]{"bottom","right"});
        yield return new WaitForSeconds(2);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack13(){
        finishedCoroutine = false;

        Croissant(new string[]{"top","left"});
        yield return new WaitForSeconds(0.6f);
        Croissant(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.6f);
        Croissant(new string[]{"middle","left"});
        yield return new WaitForSeconds(0.8f);
        Croissant(new string[]{"top","right"});
        yield return new WaitForSeconds(0.6f);
        Croissant(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.6f);
        Croissant(new string[]{"middle","right"});
        yield return new WaitForSeconds(0.8f);
        Croissant(new string[]{"top","left"});
        yield return new WaitForSeconds(0.6f);
        Croissant(new string[]{"bottom","left"});
        yield return new WaitForSeconds(0.6f);
        Croissant(new string[]{"middle","left"});
        yield return new WaitForSeconds(4);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack14(){
        finishedCoroutine = false;
        for(int i = 0; i < 5; i++){
            Spider(new string[]{"top","right"}, 0.08f);
            Spider(new string[]{"bottom","right"}, 0.08f);
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"middle","left"}, 0.14f);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(2.6f);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack15(){
        finishedCoroutine = false;
        StartCoroutine(dialogueBoxScript.Say("Or even build a spider baseball field~"));
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }

        Donut(new string[]{"middle","left"});
        Donut(new string[]{"top","left"});
        Donut(new string[]{"middle","right"});
        Donut(new string[]{"bottom","right"});
        yield return new WaitForSeconds(1);
        Spider(new string[]{"top","left"});
        Spider(new string[]{"bottom","left"});
        Spider(new string[]{"top","right"});
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.3f);
        Spider(new string[]{"top","right"});
        Spider(new string[]{"bottom","right"});
        yield return new WaitForSeconds(1.3f);
        Croissant(new string[]{"top","left"});
        Croissant(new string[]{"bottom","right"});
        yield return new WaitForSeconds(0.1f);
        Croissant(new string[]{"top","right"});
        Croissant(new string[]{"bottom","left"});
        yield return new WaitForSeconds(1);
        Spider(new string[]{"middle","left"});
        Spider(new string[]{"middle","right"});
        yield return new WaitForSeconds(3);

        ChangeTurn(dialogueBoxScript);
        finishedCoroutine = true;
    }

    IEnumerator Attack16(){ //pet 3
        for(int i = 0; i < 4; i++){
            Spider(new string[]{"top","right"});
            Spider(new string[]{"middle","right"});
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"bottom","right"});
            Spider(new string[]{"top","right"});
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"bottom","right"});
            Spider(new string[]{"middle","right"});
            yield return new WaitForSeconds(0.4f);
            Spider(new string[]{"bottom","right"});
            Spider(new string[]{"top","right"});
            yield return new WaitForSeconds(0.4f);
        }
    }




    IEnumerator Pet(int breakfastLunchDinner)
    {
        finishedCoroutine = false;

        if(breakfastLunchDinner == 1){
            StartCoroutine(dialogueBoxScript.Say("It's breakfast time, isn't it? Have fun, you two~"));
        }
        while(!dialogueBoxScript.finishedSpeaking)
        {
            yield return null;
        }

        while(dialogueBox.transform.position.x > -3.33){
            arena.transform.position -= new Vector3(0.13f, 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        switch(breakfastLunchDinner){
            case 1:
                StartCoroutine(Attack5());
                break;
            case 2:
                StartCoroutine(Attack10());
                break;
            case 3:
                StartCoroutine(Attack16());
                break;
        }

        string direction = "up";
        while(arena.transform.position.x < 0.4f)
        {
            if(direction == "up"){
                if(dialogueBox.transform.position.y < -1){
                    //sine wave? never heard of it
                    arena.transform.position += (dialogueBox.transform.position.y < -1.3) ? new Vector3(0.015f, 0.04f) : new Vector3(0.015f, 0.02f);
                }
                else{
                    direction = "down";
                }
            }
            else if(dialogueBox.transform.position.y > -2){
                arena.transform.position += (dialogueBox.transform.position.y > -1.7) ? new Vector3(0.015f, -0.04f) : new Vector3(0.015f, -0.02f);
            }
            else{
                direction = "up";
            }
            yield return null;
        }
        arena.transform.position = new Vector3(0.4f, 0);
        yield return new WaitForSeconds(0.3f);

        while(dialogueBox.transform.localScale.x < 0.58f){
            dialogueBox.transform.localScale += new Vector3(0.03f, 0);
            dialogueBox.transform.localPosition += new Vector3(0.16666f, 0);
            yield return null;
        }

        pet.SetActive(true);
        pet.transform.localPosition = new Vector3(3.5f,-1.76f);
        pet.GetComponent<SpriteRenderer>().sprite = petAppearing;

        for(int i = 0; i < 15; i++){
            pet.transform.position -= new Vector3(0.01f, 0);
            yield return null;
            pet.transform.position += new Vector3(0.01f, 0);
            yield return null;
        }
        pet.GetComponent<SpriteRenderer>().sprite = petAppearing2;

        for(int i = 0; i < 15; i++){
            pet.transform.position -= new Vector3(0.01f, 0);
            yield return null;
            pet.transform.position += new Vector3(0.01f, 0);
            yield return null;
        }
        StartCoroutine(StartClimb());
        while(dialogueBox.transform.localPosition.x > 0){        
            if(arena.transform.position.x > 0){
                arena.transform.position -= new Vector3(0.03f, 0);
            }
            if(dialogueBox.transform.localScale.x > 0.4f){
                dialogueBox.transform.localScale -= new Vector3(0.03f, 0);;
            }
            dialogueBox.transform.position -= new Vector3(0.03f, 0);
            yield return null;
        }
    }

    IEnumerator StartClimb(){
        pet.GetComponent<SpriteRenderer>().sprite = null;

        GameObject webClone = Instantiate(web, lanes.transform);
        webClone.transform.localPosition = new Vector3(0, -3.45f);

        webClone = Instantiate(web, lanes.transform);
        webClone.transform.localPosition = new Vector3(0, -4.45f);
        while(dialogueBox.transform.localScale.y < 2)
        {
            if (muffet.transform.position.y < 4.8f){
                muffet.transform.position += new Vector3(0, 0.15f);
            }
            dialogueBox.transform.localScale += new Vector3(0, 0.05f);
            dialogueBox.transform.position -= new Vector3(0, 0.05f);
            arena.transform.position += new Vector3(0, 0.1f);
            yield return null;
        }
        StartCoroutine(Climb());
    }



    IEnumerator Climb(){
        player.GetComponent<PurpleSoulScript>().petOnScreen = true;
        StartCoroutine(SpawnLanes());
        StartCoroutine(SpawnSlowSpiders());
        string rotationDirection = "right";
        StartCoroutine(PetAnimation());

        for (int frames = 400; frames > 0; frames--){
            arena.transform.position -= new Vector3(0, 0.05f);
            dialogueBox.transform.localPosition += new Vector3(0, 0.05f);
            pet.transform.localPosition += new Vector3(0, 0.05f);
            if(player.transform.position.y < -3.3f){
                player.transform.position += new Vector3(0, 0.05f);
            }
            if (rotationDirection == "right"){
                
                if(arena.transform.rotation.z > -0.04f){
                    arena.transform.Rotate(0, 0, -0.2f);
                    arena.transform.position += new Vector3(0.006f, 0);
                }
                else{
                    rotationDirection = "left";
                }
            }
            else if(arena.transform.rotation.z < 0.04f){
                arena.transform.Rotate(0, 0, 0.2f);
                arena.transform.position -= new Vector3(0.006f, 0);
            }
            else{
                rotationDirection = "right";
            }

            yield return null;
        }
        for(int i = lanes.transform.childCount; i > 3; i--){
            Destroy(lanes.transform.GetChild(i - 1).gameObject);
        }
        player.GetComponent<PurpleSoulScript>().petOnScreen = false;
        arena.transform.position = new Vector3(0, 0);
        dialogueBox.transform.localPosition = new Vector3(0, -1.48f);
        dialogueBox.transform.localScale = new Vector3(0.4f, 1);
        arena.transform.eulerAngles = new Vector3(0, 0, 0);
        muffet.transform.position = new Vector3(0, 2.2f);

        finishedCoroutine = true;
    }
    IEnumerator SpawnLanes(){
        GameObject[] clones = new GameObject[30];
        for(int i = 0; i < clones.Length; i++)
        {
            clones[i] = Instantiate(web, lanes.transform);
            clones[i].transform.localPosition = new Vector3(0, i + 0.55f);
            if(finishedCoroutine){
                yield break;
            }
            yield return new WaitForSeconds(0.66f);
        }
    }

    IEnumerator SpawnSlowSpiders(){
        SlowSpider("left", 2.55f);
        yield return new WaitForSeconds(Random.Range(0.35f, 0.54f));
        SlowSpider("right", 2.55f);
        SlowSpider("left", 3.55f);
        yield return new WaitForSeconds(Random.Range(0.2f, 0.54f));
        SlowSpider("left", 2.55f);
        yield return new WaitForSeconds(Random.Range(0.52f, 0.6f));
        SlowSpider("left", 3.55f);
        SlowSpider("right", 3.55f);
        yield return new WaitForSeconds(Random.Range(0.4f, 0.54f));
        SlowSpider("left", 5.55f);
        SlowSpider("right", 5.55f);
        SlowSpider("right", 7.55f);
        yield return new WaitForSeconds(Random.Range(0.5f, 0.7f));
        SlowSpider("right", 5.55f);
        SlowSpider("left", 7.55f);
        yield return new WaitForSeconds(Random.Range(0.35f, 0.44f));
        SlowSpider("left", 7.55f);
        SlowSpider("left", 18.55f);
        SlowSpider("right", 11.55f);
        SlowSpider("left", 11.55f);
        yield return new WaitForSeconds(Random.Range(0.25f, 0.54f));
        SlowSpider("left", 9.55f);
        SlowSpider("right", 9.55f);
        yield return new WaitForSeconds(Random.Range(0.45f, 0.7f));
        SlowSpider("left", 9.55f);
        SlowSpider("left", 11.55f);
        yield return new WaitForSeconds(Random.Range(0.6f, 0.8f));
        SlowSpider("left", 14.55f);
        yield return new WaitForSeconds(Random.Range(0.35f, 0.54f));
        SlowSpider("right", 14.55f);
        yield return new WaitForSeconds(Random.Range(0.25f, 0.44f));
        SlowSpider("left", 14.55f);
        SlowSpider("left", 16.55f);
        SlowSpider("right", 16.55f);
        SlowSpider("left", 18.55f);
        yield return new WaitForSeconds(Random.Range(0.35f, 0.54f));
        SlowSpider("right", 16.55f);
        SlowSpider("left", 18.55f);
        yield return new WaitForSeconds(Random.Range(0.45f, 0.54f));
        SlowSpider("left", 18.55f);
    }

    IEnumerator PetAnimation(){
        pet.transform.position = new Vector3(0, -3);
        int n = 0;
        while(!finishedCoroutine)
        {
            pet.GetComponent<SpriteRenderer>().sprite = petSprites[n];
            if(n == 3){
                n = 0;
            }
            else{
                n++;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }





    IEnumerator PurpleSubstance(){
        GameObject purpleClone = Instantiate(purple);
        yield return new WaitForSeconds(1);
        Destroy(purpleClone);
    }

    void Spider(string[] direction, float speed = 0.2f)
    {
        GameObject spiderClone = Instantiate(spider, transform);
        spiderClone.GetComponent<PrefabScript>().direction = direction;
        spiderClone.GetComponent<PrefabScript>().spiderSpeed = speed;
    }

    void Donut(string[] direction)
    {
        GameObject donutClone = Instantiate(donut, transform);
        donutClone.GetComponent<PrefabScript>().direction = direction;
    }

    void SlowSpider(string direction, float height){
        GameObject slowClone = Instantiate(slowSpider, transform);
        slowClone.GetComponent<PrefabScript>().direction = new string[] {"", direction};
        slowClone.transform.localPosition = new Vector3(slowClone.transform.localPosition.x, height);
    }

    void Croissant(string[] direction){
        GameObject croissantClone = Instantiate(croissant, transform);
        croissantClone.GetComponent<PrefabScript>().direction = direction;
    }

}
