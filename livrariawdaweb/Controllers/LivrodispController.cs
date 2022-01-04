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
    public class LivrodispController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public LivrodispController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                  select livros.idliv, livros.nomeliv, livros.qtdliv, count(aluguel.livroalu) as nalugueis, (livros.qtdliv - count(aluguel.livroalu)) as restante from livrodisp join aluguel join livros on livros.idliv = aluguel.livroalu group by aluguel.livroalu
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

    }
}
