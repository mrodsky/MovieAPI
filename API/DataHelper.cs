using System.Data.OleDb;
using System.Globalization;

namespace API
{
    public class DataHelper
    {
     
        public List<MovieModel> Read()
        {
            List<MovieModel> results = new List<MovieModel>();

            string connString = Utilites.GetConfigurationValue("ConnectionStrings:MyAccessDb");
            string pgmId = Utilites.GetConfigurationValue("pgmId");

            using (var connection = new OleDbConnection(connString))
            {
                connection.Open();
                string query = "SELECT * FROM movies";
                var command = new OleDbCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        var movie = new MovieModel()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Genre = reader.GetString(2),
                            Tags = reader.GetString(3),
                            Release = reader.GetDateTime(4).ToString(),
                            Director = reader.GetString(5),
                            Studio = reader.GetString(6),
                            Runtime = reader.GetInt32(7)
                        };
                        results.Add(movie);
                        /*
                        var column1Value = reader.GetInt32(0);
                        var column2Value = reader.GetString(1);
                        var column3Value = reader.GetString(2);
                        var column4Value = reader.GetString(3);
                        var column5Value = reader.GetString(4);
                        var column6Value = reader.GetString(5);

                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", column1Value.ToString(), column2Value.ToString(), 
                                                                      column3Value.ToString(), column4Value.ToString(), column5Value.ToString(),
                                                                      column6Value.ToString());

                        */
                    }
                }
               
            }
            return results;
        } //end of Read()

        public bool Insert(MovieModel data)
        {
            bool retval = true;

            // create connection
            string connString = Utilites.GetConfigurationValue("ConnectionStrings:MyAccessDb");
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                // open the connection
                connection.Open();

                // prepare the command and parameters
                string query = "INSERT INTO movies (title, genre, tags, release, director, studio, runtime) VALUES (@titles, @genre, @tags, @release, @director, @studio, @runtime)";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    DateTime date = DateTime.ParseExact(data.Release.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    command.Parameters.AddWithValue("@title", data.Title);
                    command.Parameters.AddWithValue("@genre", data.Genre);
                    command.Parameters.AddWithValue("@tags", data.Tags);
                    command.Parameters.AddWithValue("@release", date);
                    command.Parameters.AddWithValue("@director", data.Director);
                    command.Parameters.AddWithValue("@studio", data.Studio);
                    command.Parameters.AddWithValue("@runtime", data.Runtime);

                    // execute the command
                    command.ExecuteNonQuery();
                }
            }

            return retval;
        }
    }
}
