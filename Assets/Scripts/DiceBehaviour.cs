using UnityEngine;

public class DiceBehaviour : MonoBehaviour
{
    private int rDado4;
    private int rDado6;
    private int rDado8;
    private int rDado10;
    private int rDado12;
    private int rDado20;

    public void Dado4()
    {
        rDado4 = Random.Range(1, 5);
    }
    public void Dado6()
    {
        rDado6 = Random.Range(1, 7);
    }
    public void Dado8()
    {
        rDado8 = Random.Range(1, 9);
    }
    public void Dado10()
    {
        rDado10 = Random.Range(1, 11);
    }
    public void Dado12()
    {
        rDado12 = Random.Range(1, 13);
    }
    public void Dado20()
    {
        rDado20 = Random.Range(1, 21);
    }

}
