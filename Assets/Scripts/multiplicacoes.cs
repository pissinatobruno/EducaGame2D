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
        Multiplicar();

    }

    public void Multiplicar()
    {
        operacoes = new string[3] { "+", "-", "x"};
        do
        {
            i++;
            numero = randomico;
            numero2 = randomico2;
            op = rdop;
            rdop = UnityEngine.Random.Range(0, 3);
            randomico = UnityEngine.Random.Range(1, 10);
            randomico2 = UnityEngine.Random.Range(1, 10);
            opr.text = operacoes[rdop];
            valor1.text = randomico.ToString();
            valor2.text = randomico2.ToString();


            if (op == 0)
            {
                result = numero + numero2;
            }else if(op == 1)
            {
                result = numero - numero2;
            }else if(op == 2){
                result = numero * numero2;
            };

            if (result == Convert.ToInt32(resultado.text))
            {
                //Salver no banco
                Debug.Log(result);
                Debug.Log(op);
                Debug.Log(Convert.ToInt32(resultado.text));

            }

            result = 0;
            randomico = 0;
            randomico2 = 0;
            rdop = 0;
            resultado.text = "";
    
        } while (i <= 8);

        SceneManager.LoadScene(0);
    }


}
