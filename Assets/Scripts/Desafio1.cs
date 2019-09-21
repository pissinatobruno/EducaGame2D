using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Desafio1 : MonoBehaviour
{
    public TMPro.TMP_InputField[] antonimos;
    public TMPro.TMP_Text[] campos;
    public TMPro.TMP_Text acertos;
    

    public void Comparacao()
    {

        Debug.Log(antonimos[1].text);
        Debug.Log(campos[1]);

        /* for (int i = 0; i <= 6; i++)
        {
            if (antonimos[i] == campos[i])
            {

            }
        }*/
        
    }

    


}
