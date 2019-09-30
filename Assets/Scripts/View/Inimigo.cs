using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inimigo : MonoBehaviour
{
    float velocidade = 3;
    private bool direito;

    private void Start()
    {
        direito = true;
    }

    void Update()
    {
        Movimentar(velocidade);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cantos")
        {
            Direcao();
        }
    }

       
    private Vector2 VerificarDireito()
    {
        return direito ? Vector2.right : Vector2.left;
    }


    private void Movimentar(float vel)
    {
        transform.Translate(VerificarDireito() * (vel * Time.deltaTime));
    }


    private void Direcao()
    {
        direito = !direito;
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

}