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
    public class PrimeiroDesafio : MonoBehaviour
    {
        public static int id1;
        public TMPro.TMP_InputField[] antonimos;
        private string[] campos;
        public int[] pontos = new int[6];



        public void Comparacao()
        {
            //respostas
            campos = new string[6] { "bonito", "infeliz", "gordo", "mau", "calor", "triste" };
            //percorre os txt verificando se está correto
            for (int i = 0; i <= 5; i++)
            {
                //verifica se esta correto
                if (antonimos[i].text.ToUpper() == campos[i].ToUpper())
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
            ObjetoDesafioPri ObjPri = new ObjetoDesafioPri();

            ObjPri.campo1 = pontos[0];
            ObjPri.campo2 = pontos[1];
            ObjPri.campo3 = pontos[2];
            ObjPri.campo4 = pontos[3];
            ObjPri.campo5 = pontos[4];
            ObjPri.campo6 = pontos[5];

            var json = JsonUtility.ToJson(ObjPri);

            StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/desafio1/", json));

            Debug.Log(json);

        }

        IEnumerator PostRequest(string url, string json)
        {
    
            var envio = new UnityWebRequest(url, "POST");
            
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            
            envio.uploadHandler = new UploadHandlerRaw(jsonToSend);
            
            envio.downloadHandler = new DownloadHandlerBuffer();
            
            envio.SetRequestHeader("Content-Type", "application/json");
            
            yield return envio.SendWebRequest();
            
            if (envio.isNetworkError)
            {
                
                Debug.Log("ERRO: " + envio.error);
            }
            else
            {
                
                id1 = int.Parse(envio.downloadHandler.text);
                
                Debug.Log(id1);
            }


        }
    }
}

