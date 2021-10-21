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
    public class UltlivroalugController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UltlivroalugController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select aluguel.livroalu, livros.nomeliv from ultlivroalug join aluguel join livros on livros.idliv = aluguel.livroalu where aluguel.idalug = (select max(idalug) from aluguel)
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
