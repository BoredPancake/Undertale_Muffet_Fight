using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text text;
    IEnumerator Start()
    {
        text = GetComponent<Text>();
        yield return new WaitForSeconds(10);
        text.text = "you can leave now";
        yield return new WaitForSeconds(10);
        text.text = "what are you doing? looking for easter eggs?";
        yield return new WaitForSeconds(10);
        text.text = "well, this is the last message here";
        yield return new WaitForSeconds(10);
        text.text = "ok, maybe this one";
        yield return new WaitForSeconds(20);
        text.text = "don't you have anything better to do?";
    }
}
