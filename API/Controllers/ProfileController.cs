using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Domain;

namespace API.Controllers
{

    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProfileController(IConfiguration configuration){
            _configuration = configuration;
        }

        [HttpGet("{id}")]

        public JsonResult Get(string id){
            string query = @"SELECT a.UserName, n.Emri,n.Mbiemri, n.Rruga, n.Qyteti, n.NumriKontaktues
                            FROM Nxenesi n, AspNetUsers a
                            WHERE n.Profili=a.Id AND n.Profili=@id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DitariDatabase");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource)){
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query,myCon)){

                    myCommand.Parameters.Add("@id", SqlDbType.NVarChar);
                    myCommand.Parameters["@id"].Value = id;

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult Put(AccountProfile prof){
            string query = @"
           BEGIN TRANSACTION;
            UPDATE AspNetUsers SET UserName=@user WHERE Id=@id
                UPDATE Nxenesi SET Emri=@emri,Mbiemri=@mbiemri,Rruga=@rruga,Qyteti=@qyteti,NumriKontaktues=@numri WHERE Profili=@id
                    COMMIT;";
            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DitariDatabase");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource)){
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query,myCon)){

                    myCommand.Parameters.Add("@id", SqlDbType.NVarChar);
                    myCommand.Parameters["@id"].Value = prof.ProfileId;
                    myCommand.Parameters.AddWithValue("@user", prof.UserName);
                    myCommand.Parameters.AddWithValue("@emri", prof.Emri);
                    myCommand.Parameters.AddWithValue("@mbiemri", prof.Mbiemri);
                    myCommand.Parameters.AddWithValue("@rruga", prof.Rruga);
                    myCommand.Parameters.AddWithValue("@qyteti", prof.Qyteti);
                    myCommand.Parameters.AddWithValue("@numri", prof.NumriKontaktues);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated successfully");
        }
    }
    
}