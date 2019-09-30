using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NegociosBD;
using ObjSegDesafio;

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
                Salvar();
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

        public void Salvar()
        {

            Negocios negocios = new Negocios();
            ObjetoDesafio2 objetoDes = new ObjetoDesafio2(); 
            objetoDes.campo1 = pontos[0];
            objetoDes.campo2 = pontos[1];
            objetoDes.campo3 = pontos[2];
            objetoDes.campo4 = pontos[3];
            objetoDes.campo5 = pontos[4];
            objetoDes.campo6 = pontos[5];
            bool retorno = negocios.Inserir(objetoDes);

            Debug.Log(objetoDes.campo1);
            Debug.Log(objetoDes.campo2);
            Debug.Log(objetoDes.campo3);
            Debug.Log(objetoDes.campo4);
            Debug.Log(objetoDes.campo5);
            Debug.Log(objetoDes.campo6);
            Debug.Log(retorno);

        }


    }

}