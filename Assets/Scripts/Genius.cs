using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeniusEngine
{
    private List<int> numeros = new List<int>();

    public void setNumeros(){
        this.numeros.Clear(); // Limpa a lista antes de adicionar novos números
        for (int i = 0; i < 5; i++)
        {
            // Gera números aleatórios de 1 a 4 (1 = azul, 2 = amarelo, 3 = vermelho, 4 = verde)
            this.numeros.Add(UnityEngine.Random.Range(1, 5));
        }                
    }

    public List<int> getNumeros(){
        return this.numeros;        
    }
     

    public void jogar(List<int> gerados){

        int falha = 0; //quantas chances o jogador tem
        
        // Esse for vai controlar a leitura dos numeros por round
        // Talvez remover esse for (ou substituir) pra validar se o jogador acertou os números
        for (int i=1; i <= 5; i++)
        {
            List<int> subsequencia = gerados.GetRange(0, i);//Armazena os numeros gerados adicionando um a cada round
            
            //aqui o jogador vai ter que digitar os numeros
            //round 1 = 1 numero, round 2 = 2 numeros e assim por diante
            
            List<int> resposta = gerados.GetRange(0, i); // Armazena os numeros que o jogador digitou
        
            //testando se as respostas estão corretas

            //esse if é pra validar o acerto e caso negativo incrementar a falha e voltar um round

            //if (!subsequencia.SequenceEqual(resposta)){
                falha++;
            //    i--; //aqui decrementa o i pra repetir o round            
            //}
            
            Debug.Log("Dentro do for, testando as respostas Round " + i);
            Debug.Log("Subsequencia: " + string.Join(", ", subsequencia)); // Exibe a sequência atual no console
            Debug.Log("Resposta: " + string.Join(", ", resposta)); // Exibe a sequência atual no console
            Debug.Log(subsequencia.SequenceEqual(resposta)); // Exibe no console se as sequências são iguais

            //fim teste respostas

            resposta.Clear();// O player precisa entrar com todas as cores em cada round, não só a última
        }
    }


}
