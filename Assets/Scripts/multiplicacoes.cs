using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class multiplicacoes : MonoBehaviour
{

    public Text valor1;
    public Text valor2;
    public Text opr;
    public InputField resultado;
    public Button Enviar;
    private int numero;
    private int randomico;
    private int randomico2;
    private int rdop;
    private int op;
    private int numero2;
    private int result;
    private int i = 0;
    private string[] operacoes;


    void Start()
    {

    }

    public void Multiplicar()
    {
        try
        {
            operacoes = new string[3] { "+", "-", "x" };

                op = rdop;
                rdop = UnityEngine.Random.Range(0, 3);
                opr.text = operacoes[op];

                if (op == 0)
                {
                    randomic(30);
                    result = numero + numero2;
                }
                if (op == 1)
                {
                    randomic(20);
                    result = numero - numero2;
                }
                if (op == 2)
                {
                    randomic(10);
                    result = numero * numero2;
                }

                // if (result == Int32.Parse(resultado.text))
                //{ 
                    //Salver no banco
                    Debug.Log(result);
                    Debug.Log(rdop);
                    Debug.Log(op);
                    Debug.Log(Int32.Parse(resultado.text));


                //}

                result = 0;
                randomico = 0;
                randomico2 = 0;
                rdop = 0;
                resultado.text = "";
                i++;


          //  SceneManager.LoadScene(0);
        }catch(Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }


    private void randomic(int x)
    {
        numero = randomico;
        numero2 = randomico2;
        randomico = UnityEngine.Random.Range(1, x);
        randomico2 = UnityEngine.Random.Range(1, x);
        valor1.text = randomico.ToString();
        valor2.text = randomico2.ToString();
    }

}
