using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SalvarPosic : MonoBehaviour
{
    string nomeCenaAtual;
    void Awake()
    {
        nomeCenaAtual = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey(nomeCenaAtual + "X") && PlayerPrefs.HasKey(nomeCenaAtual + "Y") && PlayerPrefs.HasKey(nomeCenaAtual + "Z"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat(nomeCenaAtual + "X"), 
                                             PlayerPrefs.GetFloat(nomeCenaAtual + "Y"), 
                                             PlayerPrefs.GetFloat(nomeCenaAtual + "Z"));
        }
    }

    public void SalvarLocalizacao()
    {
        PlayerPrefs.SetFloat(nomeCenaAtual + "X", transform.position.x);
        PlayerPrefs.SetFloat(nomeCenaAtual + "Y", transform.position.y);
        PlayerPrefs.SetFloat(nomeCenaAtual + "Z", transform.position.z);
    }
}