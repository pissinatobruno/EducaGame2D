using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    public GameObject plataformaMovel;
    public float velocidade;
    public Transform pontoAtual;
    public Transform[] pontos;
    public int ponto;
    
    // Start is called before the first frame update
    void Start()
    {
        pontoAtual = pontos[ponto];
    }

    // Update is called once per frame
    void Update()
    {
        Mobilidade();
    }

    public void Mobilidade()
    {
        plataformaMovel.transform.position =
            Vector3.MoveTowards(plataformaMovel.transform.position, pontoAtual.position, Time.deltaTime * velocidade);

        if (plataformaMovel.transform.position == pontoAtual.position)
        {
            ponto++;

            if (ponto == pontos.Length)
            {
                ponto = 0;
            }

            pontoAtual = pontos[ponto];
        }
    }

}
