using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GeniusEngine : MonoBehaviour
{
    private List<int> numeros = new List<int>();
    public int playerinput=-1;

    public void setNumeros()
    {
        this.numeros.Clear(); // Limpa a lista antes de adicionar novos números
        for (int i = 0; i < 5; i++)// Gera 5 números aleatórios
        {
            // Gera números aleatórios de 1 a 4 (1 = azul, 2 = amarelo, 3 = vermelho, 4 = verde)
            this.numeros.Add(UnityEngine.Random.Range(1, 5));
        }
        Debug.Log("Numeros gerados aleatóriamente: " + string.Join(", ", numeros));
    }

    public List<int> getNumeros(){
        return this.numeros;        
    }


    //Essa função vai controlar o input do jogador
    public IEnumerator jogada(int n, Action <List<int>> callback)
    {
        List<int> resp = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            //Recebe o input do jogador
            yield return new WaitUntil(() => playerinput != -1);
            resp.Add(playerinput);
            playerinput = -1;
        }
        callback(resp);
        yield return null;
    }     

    public IEnumerator jogar(List<int> gerados)
    {

        int falha = 0; //quantas chances o jogador tem

        // Esse for vai controlar a leitura dos numeros por round
        // Talvez remover esse for (ou substituir) pra validar se o jogador acertou os números
        for (int i = 1; i <= 5; i++)//quantidade de rounds
        {
            List<int> subsequencia = gerados.GetRange(0, i);//Armazena os numeros gerados adicionando um a cada round

            //aqui o jogador vai ter que digitar os numeros
            //round 1 = 1 numero, round 2 = 2 numeros e assim por diante
            List<int> resposta = new List<int>(); //chama a função jogada passando o round atual    
            yield return StartCoroutine(jogada(i, resp=>resposta=resp)); // Chama a função jogada e espera até que o jogador tenha inserido os números        


            //esse if é pra validar o acerto e caso negativo incrementar a falha e voltar um round

            if (!subsequencia.SequenceEqual(resposta))
            {
                falha++;
                Debug.Log("Round " + i + "Testando decremento do round");
                Debug.Log("Round " + i + " Falha: " + falha);
                i--; //aqui decrementa o i pra repetir o round      
                if (falha == 3)//aqui só aceitamos 3 falhas como exemplo, só alterar esse valor que mudamos quantas falhas serão aceitas
                {
                    Debug.Log("Você perdeu!");
                    break; // Sai do loop se o jogador falhar 3 vezes
                }
            }
            resposta.Clear();// O player precisa entrar com todas as cores em cada round, não só a última
        }
    }
}