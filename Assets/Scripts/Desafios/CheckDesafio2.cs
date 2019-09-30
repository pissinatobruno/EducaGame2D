using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckDesafio2 : MonoBehaviour
{

    public GameObject placa;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Desafio2");
            placa.SetActive(false);
        }
    }

}
