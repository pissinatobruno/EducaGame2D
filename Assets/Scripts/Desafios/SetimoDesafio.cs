using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Educagame2D.Objetos;
using System.Text;
using TMPro;

namespace Educagame2D.Desafios
{
    public class SetimoDesafio : MonoBehaviour
    {
        public TMP_Text valoraq1;
        public TMP_Text valoraq2;
        public TMP_Text valorar1;
        public TMP_Text valorar2;
        public TMP_Text valorat1;
        public TMP_Text valorat2;
        public TMP_Text valorat3;
        public TMP_Text valorat4;
        public TMP_Text subtitulo;
        public TMP_Text altura;
        public TMP_InputField resultado;
        public TMP_InputField resultado2;
        public TMP_InputField resultado3;
        public Button Enviar;
        private int result;
        private int result2;
        private int result3;
        private int i = 0;
        private int[] pontos = new int[6];
        public static int id7;

        void Start()
        {
            Calcular();
        }

        public void Calcular()
        {
            randomicQuadrado(2, 10);
            result = Int32.Parse(valoraq1.text) * Int32.Parse(valoraq2.text);
            randomicRetangulo(2, 7, 12);
            result2 = Int32.Parse(valorar1.text) * Int32.Parse(valorar2.text);
            randomicTriangulo(2, 7, 12);
            result3 = (Int32.Parse(valorat1.text) * Int32.Parse(valorat2.text)) / 2;

        }

        public void Perimetro()
        {
            subtitulo.text = "ENCONTRE O PERIMETRO DOS ITENS A SEGUIR:";
            valorat2.text = "";
            altura.text = "";
            resultado.text = "";
            resultado2.text = "";
            resultado3.text = "";
            randomicQuadrado(2, 20);
            result = Int32.Parse(valoraq1.text) * 4;
            randomicRetangulo(2, 7, 12);
            result2 = (Int32.Parse(valorar1.text) * 2) + (Int32.Parse(valorar2.text) * 2);
            randomicTrianguloper(2, 7, 12);
            result3 = (Int32.Parse(valorat1.text) + Int32.Parse(valorat3.text) + Int32.Parse(valorat4.text));
        }

        void randomicQuadrado(int x, int y)
        {
            int vl = UnityEngine.Random.Range(x, y);
            valoraq1.text = vl.ToString();
            valoraq2.text = vl.ToString();
        }

        void randomicRetangulo(int x, int y, int z)
        {
            valorar1.text = UnityEngine.Random.Range(x, y).ToString();
            valorar2.text = UnityEngine.Random.Range(y, z).ToString();
        }

        void randomicTriangulo(int x, int y, int z)
        {
            int vlr = UnityEngine.Random.Range(x, y);
            int vlr2 = UnityEngine.Random.Range(y, z);

            if (vlr % 2 == 0 && vlr2 % 2 == 0)
            {
                valorat1.text = vlr.ToString();
                valorat2.text = vlr2.ToString();
            }
            else
            {
                randomicTriangulo(2, 7, 12);
            }
        }

        void randomicTrianguloper(int x, int y, int z)
        {
            valorat1.text = UnityEngine.Random.Range(x, y).ToString();
            valorat3.text = UnityEngine.Random.Range(y, z).ToString();
            valorat4.text = valorat3.text;
        }

        public void zerar()
        {
            if (string.IsNullOrEmpty(resultado.text))
            {
                resultado.text = "00";
            }
            if (string.IsNullOrEmpty(resultado2.text))
            {
                resultado2.text = "00";
            }
            if (string.IsNullOrEmpty(resultado3.text))
            {
                resultado3.text = "00";
            }
        }

        public void ResultadoFinal()
        {
            zerar();
            if (result.ToString() == resultado.text)
            {
                pontos[i] = 1;
                i++;
            }
            else
            {
                pontos[i] = 0;
                i++;
            }

            if (result2.ToString() == resultado2.text)
            {
                pontos[i] = 1;
                i++;
            }
            else
            {
                pontos[i] = 0;
                i++;
            }

            if (result3.ToString() == resultado3.text)
            {
                pontos[i] = 1;
                i++;
            }
            else
            {
                pontos[i] = 0;
                i++;
            }

            if (i < 6)
            {
                Perimetro();
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

        public void Salvar()
        {
            ObjetoDesafioSet ObjSet = new ObjetoDesafioSet();

            ObjSet.campo1 = pontos[0];
            ObjSet.campo2 = pontos[1];
            ObjSet.campo3 = pontos[2];
            ObjSet.campo4 = pontos[3];
            ObjSet.campo5 = pontos[4];
            ObjSet.campo6 = pontos[5];

            var json = JsonUtility.ToJson(ObjSet);

            StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/desafio7/", json));

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
                id7 = int.Parse(envio.downloadHandler.text);
                Debug.Log(id7);
            }
        }



    }
}