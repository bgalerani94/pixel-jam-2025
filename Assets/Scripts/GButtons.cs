using UnityEngine;

public class GButtons : MonoBehaviour
{
    public GeniusEngine genius;

    public int btnValue; // Valor do botão
    public void Press(int Value)
    {
        genius.playerinput = Value;
    }

}