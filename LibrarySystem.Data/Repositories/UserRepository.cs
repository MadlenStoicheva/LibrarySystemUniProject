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
    public class UserRepository
    {
        private string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> GetAll()
        {
            List<User> result = new List<User>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM Users";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["Id"];
                        user.username = (string)reader["Username"];
                        user.password = (string)reader["Password"];
                        user.firstName = (string)reader["First Name"];
                        user.lastName = (string)reader["Last Name"];
                        user.isAdmin = (bool)reader["Is Admin"];

                        result.Add(user);
                    }
                }
            }

            return result;
        }

        public void Insert(User user)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Users (username, password, firstName, lastName, isAdmin)
    VALUES ( @username, @password,@firstName, @lastName, @isAdmin)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@username";
                parameter.Value = user.username;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@password";
                parameter.Value = user.password;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@firstName";
                parameter.Value = user.firstName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@lastName";
                parameter.Value = user.lastName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@isAdmin";
                parameter.Value = user.isAdmin;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }
        public User GetById(int id)
        {
            User result = new User();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT * FROM Users WHERE Id=@Id";

                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {

                        User user = new User();
                        user.Id = (int)reader["Id"];
                        user.username = (string)reader["Username"];
                        user.password = (string)reader["Password"];
                        user.firstName = (string)reader["First Name"];
                        user.lastName = (string)reader["Last Name"];
                        user.isAdmin = (bool)reader["Is Admin"];

                        result = user;
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        private void Update(User user)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Users SET username=@username, password=@password,firstName=@firstName, lastName=@lastName, isAdmin=@isAdmin  WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@username";
            parameter.Value = user.username;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@password";
            parameter.Value = user.password;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@firstName";
            parameter.Value = user.lastName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@lastName";
            parameter.Value = user.lastName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@isAdmin";
            parameter.Value = user.isAdmin;
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

        public void Save(User user)
        {
            if (user.Id > 0)
            {
                Update(user);
            }
            else
            {
                Insert(user);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM Users WHERE Id=@Id";

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

        public void Delete(User user)
        {
            Delete(user.Id);
        }
    }
}