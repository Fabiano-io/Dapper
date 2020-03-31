using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoDapper.Entities;

namespace ProjetoDapper.Repository
{
    public class ContatoRepository: IContatoRepository
    {
        IConfiguration _configuration;
        public ContatoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("ContatoConnection").Value;
            return connection;
        }
        public int Add(Contato contato)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Contatos(Nome, Email) VALUES(@Nome, @Email); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                    count = con.Execute(query, contato);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }
        public int Delete(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Contatos WHERE Id =" + id;
                    count = con.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }
        public int Edit(Contato contato)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE Contatos SET Nome = @Nome, Email = @Email WHERE Id = " + contato.Id;
                    count = con.Execute(query, contato);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }
        public Contato Get(int id)
        {
            var connectionString = this.GetConnection();
            Contato contato = new Contato();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Contatos WHERE Id =" + id;
                    contato = con.Query<Contato>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return contato;
            }
        }
        public List<Contato> GetContatos()
        {
            var connectionString = this.GetConnection();
            List<Contato> contatos = new List<Contato>();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Contatos";
                    contatos = con.Query<Contato>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return contatos;
            }
        }
    }
}
