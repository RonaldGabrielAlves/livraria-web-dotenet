using livrariawdaweb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace livrariawdaweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ClientesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select idcli, nomecli, enderecocli, cidadecli, emailcli from clientes 
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand myCommand=new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Clientes cli)
        {
            string query = @"
                    insert into clientes (nomecli, enderecocli, cidadecli, emailcli) values
                    (@nomecli, @enderecocli, @cidadecli, @emailcli);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    try{
                    myCommand.Parameters.AddWithValue("@nomecli", cli.nomecli);
                    myCommand.Parameters.AddWithValue("@enderecocli", cli.enderecocli);
                    myCommand.Parameters.AddWithValue("@cidadecli", cli.cidadecli);
                    myCommand.Parameters.AddWithValue("@emailcli", cli.emailcli);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                    return new JsonResult("Adicionado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException) { 
                        return new JsonResult("Não foi possível adicionar.  Email já existente, tente com outro email!");
                    }
                }
            }


        }

        [HttpPut]
        public JsonResult Put(Clientes cli)
        {
            string query = @"
                    update clientes set
                    nomecli = @nomecli,
                    enderecocli = @enderecocli,
                    cidadecli = @cidadecli,
                    emailcli = @emailcli
                    where idcli = @idcli;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    try{
                    myCommand.Parameters.AddWithValue("@idcli", cli.idcli);
                    myCommand.Parameters.AddWithValue("@nomecli", cli.nomecli);
                    myCommand.Parameters.AddWithValue("@enderecocli", cli.enderecocli);
                    myCommand.Parameters.AddWithValue("@cidadecli", cli.cidadecli);
                    myCommand.Parameters.AddWithValue("@emailcli", cli.emailcli);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                    return new JsonResult("Editado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException) { 
                        return new JsonResult("Não foi possível Editar.  Email já existente, tente com outro email!");
                    }
                }
            }


        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from clientes
                    where idcli =@idcli;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    try{
                    myCommand.Parameters.AddWithValue("@idcli", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                    return new JsonResult("Deletado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException) { 
                        return new JsonResult("Não foi possível deletar.  O cliente possui alugueis cadastrados!");
                    }
                }
            }


        }

    }
}
