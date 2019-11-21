using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Educagame2D.Objetos;
using System.Text;

namespace Educagame2D.Desafios
{
    public class SegundoDesafio : MonoBehaviour
    {

        public static int id2;
        public Text valor1;
        public Text valor2;
        public Text opr;
        public InputField resultado;
        public Button Enviar;
        private int op;
        private int result;
        private int i = 0;
        private string[] operacoes;
        private int[] pontos = new int[6];


        void Start()
        {

            i = 0;
            Multiplicar();
        }

        public void Multiplicar()
        {
            if (i < 6)
            {
                operacoes = new string[3] { "+", "-", "x" };
                op = UnityEngine.Random.Range(0, 3);
                opr.text = operacoes[op];

                if (op == 0)
                {
                    randomic(10, 30);
                    result = Int32.Parse(valor1.text) + Int32.Parse(valor2.text);
                }
                else if (op == 1)
                {
                    randomicMenor(20, 30, 1);
                    result = Int32.Parse(valor1.text) - Int32.Parse(valor2.text);

                }
                else if (op == 2)
                {
                    randomic(2, 10);
                    result = Int32.Parse(valor1.text) * Int32.Parse(valor2.text);

                }

                resultado.text = "";

            }
            else
            {
                Salvar();
                StartCoroutine(EnableCol(2.0f));
            }

        }

        IEnumerator EnableCol(float segundos)
        {
            yield return new WaitForSeconds(segundos);
            SceneManager.LoadScene("PrincipalCena");
        }


        void randomic(int x, int y)
        {
            valor1.text = UnityEngine.Random.Range(x, y).ToString();
            valor2.text = UnityEngine.Random.Range(x, y).ToString();
        }

        void randomicMenor(int x, int y, int z)
        {
            valor1.text = UnityEngine.Random.Range(x, y).ToString();
            valor2.text = UnityEngine.Random.Range(z, x).ToString();
        }

        public void zerar()
        {
            if (string.IsNullOrEmpty(resultado.text))
            {
                resultado.text = "00";
            }
        }

        public void ResultadoFinal()
        {

                if (result.ToString() == resultado.text)
                {
                    pontos[i] = 1;
                }
                else
                {
                    pontos[i] = 0;
                }

            i++;
            Multiplicar();
        }

        public void Salvar()
        {
            ObjetoDesafioSeg ObjSeg = new ObjetoDesafioSeg();

            ObjSeg.campo1 = pontos[0];
            ObjSeg.campo2 = pontos[1];
            ObjSeg.campo3 = pontos[2];
            ObjSeg.campo4 = pontos[3];
            ObjSeg.campo5 = pontos[4];
            ObjSeg.campo6 = pontos[5];

            var json = JsonUtility.ToJson(ObjSeg);

            StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/desafio2/", json));

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
                id2 = int.Parse(envio.downloadHandler.text);
                //exibe o id do desafio no console
                Debug.Log(id2);
            }

        }

 

    }
}