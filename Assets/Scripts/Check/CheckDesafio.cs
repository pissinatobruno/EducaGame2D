using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckDesafio : MonoBehaviour
{
    [Header("Nome da cena a ser carregada")]

    public int cenaACarregar;



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(cenaACarregar);
        }

        
    }

}

