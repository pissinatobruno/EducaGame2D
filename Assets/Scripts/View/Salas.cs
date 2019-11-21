using Educagame2D.Objetos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Salas : MonoBehaviour
{
    List<string> ListaSalas = new List<string>();
    public TMP_Dropdown drop;
    public TMP_InputField txt_nome;
    public static int id_nomes;
    public static int id_salas;
    ObjetoSalas[] objetoSalas;

    void Start()
    {
        StartCoroutine(GetRequest("https://educagame.azurewebsites.net/api/salas/"));
    }


    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    IEnumerator GetRequest(string uri)
    { 
        UnityWebRequest envio = UnityWebRequest.Get(uri);
        yield return envio.SendWebRequest();

        if (envio.isNetworkError)
        {
            Debug.Log("Error While Sending: " + envio.error);
        }
        else
        {
            if (envio.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(envio.downloadHandler.data);
                string jsonString = fixJson(jsonResult);
                objetoSalas = JsonHelper.FromJson<ObjetoSalas>(jsonString);
                for (int i = 0; i < objetoSalas.Length; i++)
                {
                    ListaSalas.Add(objetoSalas[i].nome_sala);
                }
                drop.AddOptions(ListaSalas);
            }
        }
    }

    public void Salvar()
    {
        ObjetoAlunos objetoAlunos = new ObjetoAlunos();

        objetoAlunos.nome_aluno = txt_nome.text;

        var json = JsonUtility.ToJson(objetoAlunos);

        StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/alunos/", json));

        Debug.Log(json);

        StartCoroutine(EnableCol(2.0f));
    }

    IEnumerator EnableCol(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene("PrincipalCena");
    }

    IEnumerator PostRequest(string url, string json)
    {        
        var envio = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        envio.uploadHandler = new UploadHandlerRaw(jsonToSend);
        envio.downloadHandler = new DownloadHandlerBuffer();
        envio.SetRequestHeader("Content-Type", "application/json");
        yield return envio.SendWebRequest();

        if (envio.isNetworkError)
        {
            Debug.Log("ERRO: " + envio.error);
        }
        else
        {
            id_nomes = int.Parse(envio.downloadHandler.text);
            Debug.Log(id_nomes);
        }
    }

    public void GetcapId()
    {
        Debug.Log(objetoSalas[drop.value].id);
    }
}
