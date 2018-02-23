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
    public class ReaderRepository
    {
        private string connectionString;

        public ReaderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Reader> GetAll()
        {
            List<Reader> result = new List<Reader>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM Readers";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Reader readers = new Reader();
                        readers.Id = (int)reader["Id"];
                        readers.firstName = (string)reader["First Name"];
                        readers.lastName = (string)reader["Last Name"];
                        readers.readerCard = (string)reader["Reader Card"];
                        readers.expireDate = (DateTime)reader["Expire Date"];

                        result.Add(readers);
                    }
                }
            }

            return result;
        }

        public void Insert(Reader reader)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Readers (firstName, lastName, readerCard, expireDate)
    VALUES (@firstName, @lastName, @readerCard, @expireDate)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@firstName";
                parameter.Value = reader.firstName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@lastName";
                parameter.Value = reader.lastName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@readerCard";
                parameter.Value = reader.readerCard;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@expireDate";
                parameter.Value = reader.expireDate;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }

        public Reader GetById(int id)
        {
            Reader result = new Reader();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT * FROM Readers WHERE Id=@Id";

                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Reader readers = new Reader();
                        readers.Id = (int)reader["Id"];
                        readers.firstName = (string)reader["First Name"];
                        readers.lastName = (string)reader["Last Name"];
                        readers.readerCard = (string)reader["Reader Card"];
                        readers.expireDate = (DateTime)reader["Expire Date"];

                        result = readers;
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        private void Update(Reader reader)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Readers SET firstName=@firstName, lastName=@lastName, readerCard=@readerCard, expireDate=@expireDate  WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@firstName";
            parameter.Value = reader.firstName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@lastName";
            parameter.Value = reader.lastName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@readerCard";
            parameter.Value = reader.readerCard;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@expireDate";
            parameter.Value = reader.expireDate;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Save(Reader reader)
        {
            if (reader.Id > 0)
            {
                Update(reader);
            }
            else
            {
                Insert(reader);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM Readers WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(Reader reader)
        {
            Delete(reader.Id);
        }
    }
}

