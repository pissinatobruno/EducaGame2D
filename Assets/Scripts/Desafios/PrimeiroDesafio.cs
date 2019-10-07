﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrimeiroDesafio : MonoBehaviour
{

    public TMPro.TMP_InputField[] antonimos;
    private string[] campos;
    public Text acertos;
    private int contador;
    public Text corretor;



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
                        contador++;
                    }
                    else
                    {
                        corretor.text = antonimos[i].text;
                    }

                    SceneManager.LoadScene(0);
                }

                acertos.text = contador.ToString();

    }



}
