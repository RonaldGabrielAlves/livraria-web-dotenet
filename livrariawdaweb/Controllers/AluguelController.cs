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
    public class AluguelController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public AluguelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select aluguel.idalug, aluguel.livroalu, livros.nomeliv, aluguel.clientealu, clientes.nomecli, DATE_FORMAT(aluguel.dataalu,'%Y-%m-%d')as dataalu,
                    DATE_FORMAT(aluguel.dataprevdev,'%Y-%m-%d') as dataprevdev, 
                    DATE_FORMAT(aluguel.datadev,'%Y-%m-%d') as datadev, 
                    case
                    when (aluguel.datadev <= aluguel.dataprevdev) and (aluguel.datadev != 0) then 'Entregue no Prazo'
                    when aluguel.dataprevdev < aluguel.datadev then 'Atrasado'
                    when aluguel.datadev = 0 then 'Não Devolvido'
                    end as statusalug
                    from aluguel join livros on livros.idliv = aluguel.livroalu join clientes on clientes.idcli = aluguel.clientealu order by aluguel.idalug desc
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
        public JsonResult Post(Aluguel alu)
        {
            string query = @"
                    insert into aluguel (livroalu, clientealu, dataalu, dataprevdev, datadev) values
                    (@livroalu, @clientealu, @dataalu, @dataprevdev, @datadev);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@livroalu", alu.livroalu);
                    myCommand.Parameters.AddWithValue("@clientealu", alu.clientealu);
                    myCommand.Parameters.AddWithValue("@dataalu", alu.dataalu);
                    myCommand.Parameters.AddWithValue("@dataprevdev", alu.dataprevdev);
                    myCommand.Parameters.AddWithValue("@datadev", alu.datadev);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Adicionado com Sucesso!");

        }

        [HttpPut]
        public JsonResult Put(Aluguel alu)
        {
            string query = @"
                    update aluguel set
                    livroalu = @livroalu,
                    clientealu = @clientealu,
                    dataalu = @dataalu,
                    dataprevdev = @dataprevdev,
                    datadev = @datadev
                    where idalug = @idalug;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@idalug", alu.idalug);
                    myCommand.Parameters.AddWithValue("@livroalu", alu.livroalu);
                    myCommand.Parameters.AddWithValue("@clientealu", alu.clientealu);
                    myCommand.Parameters.AddWithValue("@dataalu", alu.dataalu);
                    myCommand.Parameters.AddWithValue("@dataprevdev", alu.dataprevdev);
                    myCommand.Parameters.AddWithValue("@datadev", alu.datadev);
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
                    delete from aluguel
                    where idalug =@idalug;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClientesAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@idalug", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deletado com Sucesso!");

        }

    }
}
