using Educagame2D.Objetos;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

namespace Educagame2D.Desafios
{

    public class SextoDesafio : MonoBehaviour
    {
        public static int id6;
        public string[] complete = new string[6];
        public TMP_Dropdown[] campos;
        public int[] pontos = new int[6];
        public int[] b = new int[6];

        private void Start()
        {
            for (int i = 0; i < 6; i++)
            {
                b[i] = 3;
            }
        }

        public void Dropdown_IndexChanged()
        {
            for (int i = 0; i < 6; i++)
            {
                b[i] = campos[i].value;
            }
        }


        public void Comparacao()
        {
            complete = new string[6] { "Oxítona", "Paroxítona", "Proparoxítona", "Proparoxítona", "Paroxítona", "Oxítona" };

            // percorre os txt verificando se está correto
            for (int i = 0; i <= 5; i++)
            {
                if (b[i] != 3)
                {
                    if (campos[i].options[b[i]].text.ToString() == complete[i].ToUpper())
                    {
                        //inserir no banco
                        pontos[i] = 1;
                        Debug.Log(pontos[i]);
                    }
                    else
                    {
                        pontos[i] = 0;
                        Debug.Log(pontos[i]);
                    }
                }
            }

            Salvar();

            SceneManager.LoadScene(0);
        }

        IEnumerator EnableCol(float segundos)
        {
            yield return new WaitForSeconds(segundos);
            SceneManager.LoadScene("PrincipalCena");
        }

        public void Salvar()
        {
            ObjetoDesafioSex ObjSext = new ObjetoDesafioSex();

            ObjSext.campo1 = pontos[0];
            ObjSext.campo2 = pontos[1];
            ObjSext.campo3 = pontos[2];
            ObjSext.campo4 = pontos[3];
            ObjSext.campo5 = pontos[4];
            ObjSext.campo6 = pontos[5];

            var json = JsonUtility.ToJson(ObjSext);

            StartCoroutine(PostRequest("https://educagame.azurewebsites.net/api/desafio6/", json));

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
            else
            {
                //retorna o id do desafio
                id6 = int.Parse(envio.downloadHandler.text);
                //exibe o id do desafio no console
                Debug.Log(id6);
            }

        }


    }
}

