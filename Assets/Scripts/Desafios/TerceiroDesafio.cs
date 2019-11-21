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
    public class TerceiroDesafio : MonoBehaviour
    {
        public static int id3;
        public TMPro.TMP_InputField[] complete;
        private string[] campos;
        public int[] pontos = new int[6];


        public void Comparacao()
        {
            //respostas
            campos = new string[6] { "brasil", "quinze", "capitanias", "transmitidas", "filho", "donatários" };
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
            ObjetoDesafioTerc ObjTerc = new ObjetoDesafioTerc();

            ObjTerc.campo1 = pontos[0];
            ObjTerc.campo2 = pontos[1];
            ObjTerc.campo3 = pontos[2];
            ObjTerc.campo4 = pontos[3];
            ObjTerc.campo5 = pontos[4];
            ObjTerc.campo6 = pontos[5];

            var json = JsonUtility.ToJson(ObjTerc);

            StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/desafio3/", json));


            Debug.Log(json);
        }

        IEnumerator PostRequest(string url, string json)
        {
            //instanciação de uma requisição web do tipo post na url da API
            var envio = new UnityWebRequest(url, "POST");
            //Convertendo json em bytes
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            //instanciando em raw o array de bytes
            envio.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            envio.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            //definindo o "type" do requisição no header
            envio.SetRequestHeader("Content-Type", "application/json");
            //retorna o id do desafio
            yield return envio.SendWebRequest();

            //verifica se possui erro se conexão
            if (envio.isNetworkError)
            {
                //exibe o erro no console
                Debug.Log("ERRO: " + envio.error);
            }
            else
            {
                id3 = int.Parse(envio.downloadHandler.text);
                //exibe o id do desafio no console
                Debug.Log(id3);
                //envia as informações
            }

        }


    }
}

