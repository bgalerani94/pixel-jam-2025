using UnityEngine;
using System.Collections.Generic; // Necessário para usar List<T>

public class Main : MonoBehaviour
{    
    public GeniusEngine g;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {            
        g.setNumeros();
        StartCoroutine(g.jogar(g.getNumeros()));
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
