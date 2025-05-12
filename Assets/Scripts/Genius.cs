using System.Collections.Generic; // Necessário para usar List<T>
using UnityEngine; // Necessário para usar UnityEngine.Random

// Classe GeniusEngine agora sem herdar de MonoBehaviour
public class GeniusEngine
{
    // Método Genius que retorna uma lista de números aleatórios
    public List<int> Genius()
    {
        List<int> numeros = new List<int>(); // Cria a lista para armazenar os números aleatórios
        int round = 5; // O round define o número de cores/itens que o jogador pode escolher

        // Adiciona números aleatórios de 1 a 4 à lista
        for (int i = 0; i < round; i++)
        {
            // Gera números aleatórios de 1 a 4 (1 = azul, 2 = amarelo, 3 = vermelho, 4 = verde)
            numeros.Add(UnityEngine.Random.Range(1, 5)); // UnityEngine.Random.Range(min, max) retorna valores entre min (inclusive) e max (exclusivo)
        }

        return numeros; // Retorna a lista com os números aleatórios
    }
}
