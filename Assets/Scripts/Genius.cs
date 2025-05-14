using System.Collections.Generic; // Necessário para usar List<T>
using UnityEngine;

public class GeniusEngine
{
    // Método Genius que retorna uma lista de números aleatórios
    public void Genius()
    {
        List<int> numeros = new List<int>(); // Cria a lista para armazenar os números aleatórios
        numeros.Clear(); // Limpa a lista antes de adicionar novos números
        for (int i = 0; i < 5; i++)
        {
            // Gera números aleatórios de 1 a 4 (1 = azul, 2 = amarelo, 3 = vermelho, 4 = verde)
            numeros.Add(UnityEngine.Random.Range(1, 5));
        }
        Debug.Log("Números gerados: " + string.Join(", ", numeros)); // Exibe os números gerados no console

        // Esse for vai controlar a leitura dos numeros por round
        // Talvez remover esse for (ou substituir) pra validar se o jogador acertou os números
        for (int i=1; i <= 5; i++)
        {
            List<int> subsequencia = numeros.GetRange(0, i);//Armazena os numeros gerados adicionando um a cada round
            Debug.Log("Round " + (i) + ": " + string.Join(", ", subsequencia)); // Exibe a sequência atual no console
        }

    
    }
}
