using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using System;

namespace ConexaoBanco{ 

    public class ConexaoBD : MonoBehaviour
    {
        public void Teste()
        {
            Debug.Log("Conexao ok");
        }
        // criar a conexão com o banco de dados
        private SqlConnection CriarConexao()
        {
            return new SqlConnection("Data Source=DESKTOP-QSPC4KE\\SQLEXPRESS;Initial Catalog=educagame;Integrated Security=True");
        
        }

        // parametros que vão para o banco de dados
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }



        // inserir, alterar e deletar
        public object ExecutarManipulacao(CommandType commandType, string nomeProcedure)
        {

            try
            {
                // criar a conexao com o banco de dados
                SqlConnection sqlConnection = CriarConexao();

                // abrir conexao
                sqlConnection.Open();

                // criar comando que vai para o banco de dados
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // colocar as informações dentro do comando que mandará essas informações para o banco 
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeProcedure;
                sqlCommand.CommandTimeout = 7200;  // em segundos

                // adicionar parametros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ExecutarConsulta(CommandType commandType, string nomeProcedure)
        {
            try
            {
                // criar a conexao com o banco de dados
                SqlConnection sqlConnection = CriarConexao();

                // abrir conexao
                sqlConnection.Open();

                // criar comando que vai para o banco de dados
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // colocar as informações dentro do comando que mandará essas informações para o banco 
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeProcedure;
                sqlCommand.CommandTimeout = 7200;  // em segundos

                // adicionar parametros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }


                // cria um adaptado
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                // DataTable vazia que receberá os dados que virão do banco de dados
                DataTable dataTable = new DataTable();

                // preenche o dataTable com os dados que vieram do banco 
                sqlDataAdapter.Fill(dataTable);

                // retorna o dataTable com os dados da consulta que foi feita no banco de dados
                return dataTable;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}