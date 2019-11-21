using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Educagame2D.Desafios;
using UnityEngine.Networking;
using Educagame2D.Objetos;

public class Finalizacao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Salvar();
    }

    public void Salvar()
    {
        ObjetoSalasAlunos objetoSalasAlunos = new ObjetoSalasAlunos();

        objetoSalasAlunos.id_aluno = Salas.id_nomes;
        objetoSalasAlunos.id_sala = Salas.id_salas;
        objetoSalasAlunos.id_desafio1 = PrimeiroDesafio.id1;
        objetoSalasAlunos.id_desafio2 = SegundoDesafio.id2;
        objetoSalasAlunos.id_desafio3 = TerceiroDesafio.id3;
        objetoSalasAlunos.id_desafio4 = QuartoDesafio.id4;
        objetoSalasAlunos.id_desafio5 = QuintoDesafio.id5;
        objetoSalasAlunos.id_desafio6 = SextoDesafio.id6;
        objetoSalasAlunos.id_desafio7 = SetimoDesafio.id7;


        var json = JsonUtility.ToJson(objetoSalasAlunos);

        StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/SalasAlunos/", json));

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
    }
}
