using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeniusEngine
{
    private List<int> numeros = new List<int>();

    public void setNumeros(){
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
    public List<int> jogada(int n)
    {
        List<int> resposta = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            //Recebe o input do jogador
            resposta.Add(/*INPUT DO JOGADOR*/1);//coloquei esse 1 só pra não dar erro, mas aqui vai o input do jogador
        }
        return resposta;
    }     

    public void jogar(List<int> gerados)
    {

        int falha = 0; //quantas chances o jogador tem

        // Esse for vai controlar a leitura dos numeros por round
        // Talvez remover esse for (ou substituir) pra validar se o jogador acertou os números
        for (int i = 1; i <= 1; i++)//quantidade de rounds
        {
            List<int> subsequencia = gerados.GetRange(0, i);//Armazena os numeros gerados adicionando um a cada round

            //aqui o jogador vai ter que digitar os numeros
            //round 1 = 1 numero, round 2 = 2 numeros e assim por diante
            List<int> resposta = jogada(i); //chama a função jogada passando o round atual            

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