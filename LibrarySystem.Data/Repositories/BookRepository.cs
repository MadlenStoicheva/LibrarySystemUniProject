using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data
{
    public class BookRepository
    {
        private string connectionString;

        public BookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Book> GetAll()
        {
            List<Book> result = new List<Book>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM Books";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.Id = (int)reader["Id"];
                        book.numberISBN = (string)reader["ISBN"];
                        book.title = (string)reader["Title"];
                        book.author = (string)reader["Author"];
                        book.publishingHouse = (string)reader["Publishing House"];
                        book.releaseDate = (DateTime)reader["Release Date"];
                        book.availability = (int)reader["Availability"];

                        result.Add(book);
                    }
                }
            }

            return result;
        }

        public void Insert(Book book)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Books (numberISBN, title, author, publishingHouse, releaseDate, availability)
    VALUES (@numberISBN, @title, @author, @publishingHouse, @releaseDate, @availability)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@numberISBN";
                parameter.Value = book.numberISBN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@title";
                parameter.Value = book.title;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@author";
                parameter.Value = book.author;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@publishingHouse";
                parameter.Value = book.publishingHouse;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@releaseDate";
                parameter.Value = book.releaseDate;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@availability";
                parameter.Value = book.availability;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }

        public Book GetById(int id)
        {
            Book result = new Book();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT * FROM Books WHERE Id=@Id";

                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.Id = (int)reader["Id"];
                        book.numberISBN = (string)reader["numberISBN"];
                        book.title = (string)reader["Title"];
                        book.author = (string)reader["Author"];
                        book.publishingHouse = (string)reader["Publishing House"];
                        book.releaseDate = (DateTime)reader["Release Date"];
                        book.availability = (int)reader["Availability"];

                        //if (!(reader["Description"] is DBNull))
                        //{
                        //    department.Description = (string)reader["Description"];
                        //}

                        result = book;
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        private void Update(Book book)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Books SET numberISBN=@numberISBN, title=@title, author=@author, publishingHouse=@publishingHouse, releaseDate=@releaseDate, availability=@availability,  WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@numberISBN";
            parameter.Value = book.numberISBN;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@title";
            parameter.Value = book.title;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@author";
            parameter.Value = book.author;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@publishingHouse";
            parameter.Value = book.publishingHouse;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@releaseDate";
            parameter.Value = book.releaseDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@availability";
            parameter.Value = book.availability;
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

        public void Save(Book book)
        {
            if (book.Id > 0)
            {
                Update(book);
            }
            else
            {
                Insert(book);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM Books WHERE Id=@Id";

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

        public void Delete(Book book)
        {
            Delete(book.Id);
        }
    }
}
