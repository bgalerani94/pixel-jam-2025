using UnityEngine;
using System.Collections.Generic; // Necessário para usar List<T>

public class Main : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<int> numeros = new List<int>();
        GeniusEngine g = new GeniusEngine(); // Cria uma instância da classe GEngine        
        g.Genius(); // Gera os números aleatórios
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1.5f, -2, 0) * Time.deltaTime * speed;        
    }
}
