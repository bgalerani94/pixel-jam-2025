using UnityEngine;
using System.Collections.Generic; // Importa o namespace System.Collections.Generic para usar List<T>

public class HelloWorld : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<int> numeros = new List<int>();
        int round = 5; //o round que o jogador está jogando define a quantidade de cores que ele pode escolher


        // Adiciona números de 1 a <ROUND> à lista
        for (int i = 1; i <= round; i++)
            {
                // Gera números aleatórios de 1 a 4
                // 1 = azul, 2 = amarelo, 3 = vermelho, 4 = verde
                numeros.Add(UnityEngine.Random.Range(1, 5));
            }
        Debug.Log("Lista completa: " + string.Join(", ", numeros));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1.5f, -2, 0) * Time.deltaTime * speed;
    }
}
