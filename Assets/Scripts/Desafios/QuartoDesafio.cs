using Educagame2D.Objetos;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Educagame2D.Desafios
{
    public class QuartoDesafio : MonoBehaviour
    {
        public static int id4;
        public TMPro.TMP_InputField[] complete;
        private string[] campos;
        public int[] pontos = new int[6];



        public void Comparacao()
        {
            //respostas
            campos = new string[6] { "Belo Horizonte", "Porto Alegre", "Cuiabá", "Fortaleza", "Teresina", "Belém" };
            //percorre os txt verificando se está correto
            for (int i = 0; i <= 5; i++)
            {
                //verifica se esta correto
                if ((complete[i].text.ToUpper()) == (campos[i].ToUpper()))
                {
                    //inserir no banco
                    pontos[i] = 1;
                }
                else
                {
                    pontos[i] = 0;
                }


            }

            Salvar();

            StartCoroutine(EnableCol(2.0f));
        }

        IEnumerator EnableCol(float segundos)
        {
            yield return new WaitForSeconds(segundos);
            SceneManager.LoadScene("PrincipalCena");
        }


        public void Salvar()
        {
            ObjetoDesafioQuart ObjQuart = new ObjetoDesafioQuart();

            ObjQuart.campo1 = pontos[0];
            ObjQuart.campo2 = pontos[1];
            ObjQuart.campo3 = pontos[2];
            ObjQuart.campo4 = pontos[3];
            ObjQuart.campo5 = pontos[4];
            ObjQuart.campo6 = pontos[5];

            var json = JsonUtility.ToJson(ObjQuart);

            StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/desafio4/", json));

            Debug.Log(json);

        }

        IEnumerator PostRequest(string url, string json)
        {
            //instanciação de uma requisição web do tipo post na url da API
            var envio = new UnityWebRequest(url, "POST");
            //Convertendo json em bytes
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            //instanciando em raw o array de bytes
            envio.uploadHandler = new UploadHandlerRaw(jsonToSend);
            //instanciando uma função para captura de retorno
            envio.downloadHandler = new DownloadHandlerBuffer();
            //definindo o "type" do requisição no header
            envio.SetRequestHeader("Content-Type", "application/json");
            //envia as informações
            yield return envio.SendWebRequest();
            //verifica se possui erro se conexão
      
            if (envio.isNetworkError)
            {
                //exibe o erro no console
                Debug.Log("ERRO: " + envio.error);
            }
            else
            {
                //retorna o id do desafio
                id4 = int.Parse(envio.downloadHandler.text);
                //exibe o id do desafio no console
                Debug.Log(id4);
            }

        }


    }
}

