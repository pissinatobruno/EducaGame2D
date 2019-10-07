using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SegDesafio
{ 
    public class SegundoDesafio : MonoBehaviour
    {
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
                StartCoroutine(Salvar(pontos));
                SceneManager.LoadScene(0);
            }    
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



        public void ResultadoFinal()
        {
            if (result == Int32.Parse(resultado.text))
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

        public IEnumerator Salvar(int[] pontos)
        {

            //IDictionary<object, object> dictionary = new Dictionary<object, object>();
            //string json = MiniJSON.JSON.Serialize(dictionary);

            WWWForm form = new WWWForm();
                form.AddField("pontos[0]", pontos[0]);
                form.AddField("pontos[1]", pontos[1]);
                form.AddField("pontos[2]", pontos[2]);
                form.AddField("pontos[3]", pontos[3]);
                form.AddField("pontos[4]", pontos[4]);
                form.AddField("pontos[5]", pontos[5]);
                UnityWebRequest www = UnityWebRequest.Post("https://apieducagame20191006054314.azurewebsites.net/api/values", form);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
      

        }


    }

}