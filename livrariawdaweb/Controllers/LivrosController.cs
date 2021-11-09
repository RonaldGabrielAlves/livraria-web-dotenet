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
    public class LivrosController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public LivrosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select livros.idliv, livros.nomeliv, livros.editliv, editoras.idedi, editoras.nomedi, livros.autorliv, DATE_FORMAT(livros.lcmliv,'%Y-%m-%d') as lcmliv, 
                    livros.qtdliv from livros join editoras on editoras.idedi = livros.editliv
            ";
            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
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
        public JsonResult Post(Livros liv)
        {
            string query = @"
                    insert into livros (nomeliv, editliv, autorliv, lcmliv, qtdliv) values
                    ( @nomeliv, @editliv, @autorliv, @lcmliv, @qtdliv);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@nomeliv", liv.nomeliv);
                    myCommand.Parameters.AddWithValue("@editliv", liv.editliv);
                    myCommand.Parameters.AddWithValue("@autorliv", liv.autorliv);
                    myCommand.Parameters.AddWithValue("@lcmliv", liv.lcmliv);
                    myCommand.Parameters.AddWithValue("@qtdliv", liv.qtdliv);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Adicionado com Sucesso!");

        }

        [HttpPut]
        public JsonResult Put(Livros liv)
        {
            string query = @"
                    update livros set
                    nomeliv = @nomeliv,
                    editliv = @editliv,
                    autorliv = @autorliv,
                    lcmliv = @lcmliv,
                    qtdliv = @qtdliv
                    where idliv = @idliv;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@idliv", liv.idliv);
                    myCommand.Parameters.AddWithValue("@nomeliv", liv.nomeliv);
                    myCommand.Parameters.AddWithValue("@editliv", liv.editliv);
                    myCommand.Parameters.AddWithValue("@autorliv", liv.autorliv);
                    myCommand.Parameters.AddWithValue("@lcmliv", liv.lcmliv);
                    myCommand.Parameters.AddWithValue("@qtdliv", liv.qtdliv);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Editado com Sucesso!");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from livros
                    where idliv =@idliv;
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
                    myCommand.Parameters.AddWithValue("@idliv", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                    return new JsonResult("Deletado com Sucesso!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException) { 
                        return new JsonResult("Não foi possível deletar. O livro possui alugueis cadastrados!");
                    }   
                }
            }

            

        }

    }
}
