using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Repositories
{
    public class TakeABookRepository
    {
        private string connectionString;

        public TakeABookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<TakeABook> GetAll()
        {
            List<TakeABook> result = new List<TakeABook>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM TakeABooks";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        TakeABook takeABook = new TakeABook();
                        takeABook.Id = (int)reader["Id"];
                        takeABook.book = (Book)reader["Book"];
                        takeABook.reader = (Reader)reader["Reader"];
                        takeABook.dateTaken = (DateTime)reader["Date Taken"];
                        takeABook.dateForReturn = (DateTime)reader["Date For Return"];
                        takeABook.dateReturn = (DateTime)reader["dateReturn"];

                        result.Add(takeABook);
                    }
                }
            }

            return result;
        }

        public void Insert(TakeABook takeABook)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO TakeABooks (book, reader, dateTaken, dateForReturn, dateReturn)
    VALUES (@book, @reader, @dateTaken, @dateForReturn, @dateReturn)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@book";
                parameter.Value = takeABook.book;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@reader";
                parameter.Value = takeABook.reader;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@dateTaken";
                parameter.Value = takeABook.dateTaken;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@dateForReturn";
                parameter.Value = takeABook.dateForReturn;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@dateReturn";
                parameter.Value = takeABook.dateReturn;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }
    }
}
