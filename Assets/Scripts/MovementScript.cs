using UnityEngine;

public class MovementScript : MonoBehaviour 
{
    public GameObject dialogueBox;
    /* Probably should have put hp here. 
       Now there's a bug where if you heal on turn 1,
       it says "You recovered x HP" instead of 
       "Your HP was maxed out".
       Also this will be a problem if I want 
       (or anyones wants) to reuse this code
       in the future. 
    */
    
    void Update()
    {
        MoveHorizontal();
        if (MainScript.turn == "Enemy")
        {
            if (Input.GetKey("up") && transform.position.y < -0.3f)
            {
                transform.position += new Vector3(0, 0.1f);
            }
            else if (Input.GetKey("down") && transform.position.y > -2.6f)
            {
                transform.position -= new Vector3(0, 0.1f);
            }
        }
    }
    protected void MoveHorizontal() //will be useful in inherited PurpleSoulScript
    {
        if (MainScript.turn == "Enemy")
        {
            if (Input.GetKey("right") &&  transform.localPosition.x < 2.1)
            {
                transform.localPosition += new Vector3(0.1f, 0);
            }
            else if (Input.GetKey("left") && transform.localPosition.x > -2.1)
            {
                transform.localPosition -= new Vector3(0.1f, 0);
            }
        }
    }

    public void ChangeSoul()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        this.gameObject.GetComponent<PurpleSoulScript>().enabled = true;
    }
}
