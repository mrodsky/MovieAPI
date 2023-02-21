using Microsoft.AspNetCore.Mvc;
using API;
using System.Text.Json;
using System.Data.OleDb;
using System.Net;

namespace API.Controllers
{
    public class CustomResponse
    {
        public string Message { get; set; }
        public string pgm { get; set; }
        public string user { get; set; }
        public DateTimeOffset timestamp { get; set; }

        public string? exMessage { get; set; }

        public List<MovieModel>? dbObjs {get;set;}
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        string pgmId = Utilites.GetConfigurationValue("pgmId");


        [HttpGet]
        [Route("read")]
        public IActionResult Read()
        {
            try
            {

                DataHelper dbHelper = new DataHelper();

                //read all records from accessdb.requests table
                var results = dbHelper.Read();
     
                    //we can seralize but we do not need to, CustomResponse will seralize for us
                     string json = JsonSerializer.Serialize(results);

                //create response class
                    var response = new CustomResponse
                    {
                        Message = "Response from Access DB",
                        pgm = pgmId,
                        user = Environment.UserName,
                        timestamp = Utilites.CentralTime(),
                        exMessage = null,
                        dbObjs = results
                    };


                //return the response obj with 202 (success)
                return new ObjectResult(response)
                {
                    StatusCode = 202
                };
            }

            catch (Exception e)
            {

                //if error return exception message back to the client (do not throw) w/ 401 code
                var response = new CustomResponse
                {
                    Message = $"An Error has occured...",
                    pgm = pgmId,
                    user = Environment.UserName,
                    timestamp = Utilites.CentralTime(),
                    exMessage = e.Message
                };

                return new ObjectResult(response)
                {
                    StatusCode = 401
                };
            }

        }



        [HttpGet]
        [Route("testHttpGet")]
        public IActionResult Get()
        {
            try
            {
                var response = new CustomResponse
                {
                    Message = "Retrieved Records from DB",
                    pgm = pgmId,
                    user = Environment.UserName,
                    timestamp = Utilites.CentralTime(),
                    exMessage = null
                };

                return new ObjectResult(response)
                {
                    StatusCode = 202
                };
            }

            catch (Exception e)
            {
                var response = new CustomResponse
                {
                    Message = $"An Error has occured...",
                    pgm = pgmId,
                    user = Environment.UserName,
                    timestamp = Utilites.CentralTime(),
                    exMessage = e.Message
                };

                return new ObjectResult(response)
                {
                    StatusCode = 401
                };
            }

        }


        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] JsonElement? payload)
        {

            try
            {
                string jsonString = payload?.ToString();
                Console.WriteLine(jsonString);
                var myModel = JsonSerializer.Deserialize<MovieModel>(jsonString);
              
                if(myModel != null)
                {
                    DataHelper dbHelper = new DataHelper();

                    dbHelper.Insert(myModel);
                }

                var response = new CustomResponse
                {
                    Message = "Added Record to DB",
                    pgm = pgmId,
                    user = Environment.UserName,
                    timestamp = Utilites.CentralTime(),
                    exMessage = null
                };

                return new ObjectResult(response)
                {
                    StatusCode = 202
                };

               
            }
            
            catch(Exception e)
            {

                var response = new CustomResponse
                {
                    Message = $"An Error has occured...",
                    pgm = pgmId,
                    user = Environment.UserName,
                    timestamp = Utilites.CentralTime(),
                    exMessage = e.Message
                };

                return new ObjectResult(response)
                {
                    StatusCode = 401
                };
               
            }

        }
    }
}
