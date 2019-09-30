using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using ConexaoBanco;
using ObjSegDesafio;

namespace NegociosBD
{
    public class Negocios : MonoBehaviour
    {
        ConexaoBD acessoDados = new ConexaoBD();

        public bool Inserir(ObjetoDesafio2 objetoDes)
        {

            try
            {

                acessoDados.Teste();
                // limpa os parametros
                acessoDados.LimparParametros();

                // adiciona os novos parametros
                acessoDados.AdicionarParametros("@campo1", objetoDes.campo1);
                acessoDados.AdicionarParametros("@campo2", objetoDes.campo2);
                acessoDados.AdicionarParametros("@campo3", objetoDes.campo3);
                acessoDados.AdicionarParametros("@campo4", objetoDes.campo4);
                acessoDados.AdicionarParametros("@campo5", objetoDes.campo5);
                acessoDados.AdicionarParametros("@campo6", objetoDes.campo6);



                // executa o comando de inserção
                acessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "adicionar_verificacao");

                
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}