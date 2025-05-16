using UnityEngine;

public class GButtons : MonoBehaviour
{

    public void Press(int Value)
    {
        if (GeniusEngine.WaitInput) {
        GeniusEngine.botao = Value;
        }
    }

}