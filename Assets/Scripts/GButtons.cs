using UnityEngine;

public class GButtons : MonoBehaviour
{

    public int A; // Valor do botão
    public void Press(int Value)
    {
        A = Value;
        Debug.Log("Button value set to: " + A);
    }

}