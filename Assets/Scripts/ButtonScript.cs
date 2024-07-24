using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public int buttonID;
    SpriteRenderer spriteRenderer;
    public Sprite unactive;
    public Sprite active;
    string turn;
    int FAIM;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        turn = MainScript.turn;
        FAIM = MainScript.FAIM;
        if (turn == "Player" && FAIM == buttonID) { 
            spriteRenderer.sprite = active;
        }
        else
        {
            spriteRenderer.sprite = unactive;
        }
    }
}
