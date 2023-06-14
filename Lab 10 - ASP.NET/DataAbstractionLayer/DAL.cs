using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Lab_9___ASP.NET.Models;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Lab_9___ASP.NET.DataAbstractionLayer
{
    public class DAL
    {
        private string myConnectionString = "server=localhost;database=library;user=root;password=";
        public bool Login(string username, string password)
        {
            MySqlConnection conn;
            List<User> userList = new List<User>();
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM user WHERE Username = @Username AND Password = @Password";

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    User user = new User();
                    user.username = myreader.GetString("username");
                    userList.Add(user);
                }

                myreader.Close();
                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return userList.Count == 1;
        }

        public bool Register(string username, string password)
        {
            MySqlConnection conn;
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO user (Username, Password) VALUES (@Username, @Password)";

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                int command = cmd.ExecuteNonQuery();

                conn.Close();

                return command == 1;
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return false;
        }

        public List<string> GetGenres()
        {
            List<string> genreList = new List<string>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.ConnectionString = myConnectionString;
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT DISTINCT Genre FROM books";

                        using (MySqlDataReader myreader = cmd.ExecuteReader())
                        {
                            while (myreader.Read())
                            {
                                genreList.Add(myreader.GetString("Genre"));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return genreList;
        }


        public int GetUserIdByName(string username)
        {
            MySqlConnection conn;
            int id = 0;

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM user WHERE Username='" + username + "'";

                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    id = myreader.GetInt16("ID");
                }

                myreader.Close();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }

            return id;
        }


        public List<Book> GetAllBooks()
        {
            MySqlConnection conn;
            List<Book> bookList = new List<Book>();

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM books";

                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Book book = new Book();
                    book.Id = myreader.GetGuid("Id");
                    book.Title = myreader.GetString("Title");
                    book.Author = myreader.GetString("Author");
                    book.Pages = myreader.GetInt32("Pages");
                    book.Genre = myreader.GetString("Genre");
                    book.IsLent = myreader.GetBoolean("IsLent");

                    bookList.Add(book);
                }

                myreader.Close();
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return bookList;
        }

        public List<Book> GetBooksByGenre(string genre)
        {
            MySqlConnection conn;
            List<Book> bookList = new List<Book>();

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM books WHERE Genre = @Genre";

                cmd.Parameters.AddWithValue("@Genre", genre);

                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Book book = new Book();
                    book.Id = myreader.GetGuid("Id");
                    book.Title = myreader.GetString("Title");
                    book.Author = myreader.GetString("Author");
                    book.Pages = myreader.GetInt32("Pages");
                    book.Genre = myreader.GetString("Genre");
                    book.IsLent = myreader.GetBoolean("IsLent");

                    bookList.Add(book);
                }

                myreader.Close();
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return bookList;
        }

        public bool AddBook(Book book)
        {
            MySqlConnection conn;

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO books(Id, Title, Author, Pages, Genre, IsLent) VALUES (@Id, @Title, @Author, @Pages, @Genre, @IsLent)";

                cmd.Parameters.AddWithValue("@Id", book.Id);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Pages", book.Pages);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@IsLent", book.IsLent);

                int command = cmd.ExecuteNonQuery();

                conn.Close();

                return command == 1;
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return false;
        }
        public Guid GetBookIdByName(string title)
        {
            MySqlConnection conn;
            Guid bookId = Guid.Empty;

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Id FROM books WHERE Title = @Title";

                cmd.Parameters.AddWithValue("@Title", title);

                MySqlDataReader myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    bookId = myreader.GetGuid("Id");
                }

                myreader.Close();
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return bookId;
        }

        public bool DeleteBook(Guid id)
        {
            MySqlConnection conn;
            bool result = false;

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM books WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                result = rowsAffected > 0;
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }

            return result;
        }


        public bool UpdateBook(Book book)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(myConnectionString))
                    {
                        conn.Open();

                        string query = "UPDATE books SET Title = @Title, Author = @Author, Pages = @Pages, Genre = @Genre, IsLent = @IsLent WHERE Id = @Id";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", book.Id);
                            cmd.Parameters.AddWithValue("@Title", book.Title);
                            cmd.Parameters.AddWithValue("@Author", book.Author);
                            cmd.Parameters.AddWithValue("@Pages", book.Pages);
                            cmd.Parameters.AddWithValue("@Genre", book.Genre);
                            cmd.Parameters.AddWithValue("@IsLent", book.IsLent);

                            int result = cmd.ExecuteNonQuery();

                            return result == 1;
                        }
                    }
                }
                catch (MySqlException e)
                {
                    Console.Write(e.Message);
                    return false;
                }
            }
        }
    }
