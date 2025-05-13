using UnityEngine;
using System.Collections.Generic; // Necessário para usar List<T>

public class Main : MonoBehaviour
{    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        GeniusEngine g = new GeniusEngine(); // Cria uma instância da classe GEngine        
        g.Genius(); // Gera os números aleatórios
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
