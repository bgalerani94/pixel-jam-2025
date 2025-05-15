using UnityEngine;
using System.Collections.Generic; // Necessário para usar List<T>

public class Main : MonoBehaviour
{    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        GeniusEngine g = new GeniusEngine(); // Cria uma instância da classe GEngine        
        //List<int> genius = g.Generate_numbers(); // Gera e armazena os números aleatórios
        g.setNumeros();
        g.getNumeros();
        g.jogar(g.getNumeros());
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
