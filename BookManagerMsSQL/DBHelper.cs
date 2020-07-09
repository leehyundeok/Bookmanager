using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager
{
    class DBHelper
    {
        public static SqlConnection conn = new SqlConnection();
        public static DataSet ds;
        public static DataTable dt_book;
        public static DataTable dt_user;

        public static void ConnectDB()
        {
            conn.ConnectionString = string.Format("Data Source=({0}); " +
                    "Initial Catalog = {1};" +
                    "Integrated Security = {2};" +
                    "Timeout = 3"
                    , "local", "MYDB1", "SSPI");
            conn = new SqlConnection(conn.ConnectionString);
            conn.Open();
        }

        public static void selectQuery_Book(string Isbn = "")
        {
            ConnectDB();

            //SQL 명령어 선언
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (Isbn == "")
            {
                cmd.CommandText = "SELECT * FROM BookTable";

                SqlDataAdapter da = new SqlDataAdapter(cmd); //select 구문이 들어감
                ds = new DataSet();
                da.Fill(ds, "BookTable");

                dt_book = ds.Tables[0];

            }
            else
            {
                cmd.CommandText = "SELECT * FROM BookTable Where Isbn = " + Isbn;
                SqlDataAdapter da = new SqlDataAdapter(cmd); //select 구문이 들어감
                ds = new DataSet();
                da.Fill(ds, "BookTable");

                dt_book = ds.Tables[0]; //select 결과값을 DataTable에 넣는다.
            }

            conn.Close(); //연결 해제
        }

        public static void selectQuery_User(int Id = -1)
        {
            ConnectDB();

            //SQL 명령어 선언
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (Id < 0)
            {
                cmd.CommandText = "SELECT * FROM UserTable";

                //DataAdapter와 DataSet으로 DB table 불러오기
                //DataSet은 메모리상의 하나의 DB 객체이며 DataTable은 메모리상의 하나의 테이블 객체
                SqlDataAdapter da = new SqlDataAdapter(cmd); //select 구문이 들어감
                ds = new DataSet();
                da.Fill(ds, "UserTable");

                dt_user = ds.Tables[0]; //select 결과값을 DataTable에 넣는다.
                

            }
            else
            {
                cmd.CommandText = "SELECT * FROM UserTable Where Id = " + Id;

                //DataAdapter와 DataSet으로 DB table 불러오기
                //DataSet은 메모리상의 하나의 DB 객체이며 DataTable은 메모리상의 하나의 테이블 객체
                SqlDataAdapter da = new SqlDataAdapter(cmd); //select 구문이 들어감
                ds = new DataSet();
                da.Fill(ds, "UserTable");

                dt_user = ds.Tables[0]; //select 결과값을 DataTable에 넣는다.
            }

            conn.Close(); //연결 해제
        }

        public static void updateQuery(string isbn, int userId = 0, bool doBorrow = false)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string sqlcommand;
                if (doBorrow) //대출
                {
                    string userName = dt_user.Rows[0]["Name"].ToString();
                    sqlcommand = "Update BookTable set UserId = @p1, UserName = @p2, isBorrowed = 1, BorrowedAt = @p3 where Isbn = @p4";

                    cmd.Parameters.AddWithValue("@p1", userId);
                    cmd.Parameters.AddWithValue("@p2", userName);
                    cmd.Parameters.AddWithValue("@p3", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@p4", isbn);
                }
                else //반납
                {
                    sqlcommand = "Update BookTable set UserId = null, UserName = null, isBorrowed = 0, BorrowedAt = null where Isbn = @p1";
                    cmd.Parameters.AddWithValue("@p1", isbn);
                }
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception eee)
            {

            }

        }

        public static void selectQuery_BookForm(string Isbn = "")
        {
            ConnectDB();

            //SQL 명령어 선언
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (Isbn == "")
            {
                cmd.CommandText = "SELECT Isbn, Name, Publisher, Page, UserId, isBorrowed, BorrowedAt FROM BookTable";

                SqlDataAdapter da = new SqlDataAdapter(cmd); //select 구문이 들어감
                ds = new DataSet();
                da.Fill(ds, "BookTable");

                dt_book = ds.Tables[0]; //select 결과값을 DataTable에 넣는다.
               

            }
            else
            {
                cmd.CommandText = "SELECT Isbn, Name, Publisher, Page, UserId, isBorrowed, BorrowedAt  FROM BookTable Where Isbn = " + Isbn;

                //DataAdapter와 DataSet으로 DB table 불러오기
                //DataSet은 메모리상의 하나의 DB 객체이며 DataTable은 메모리상의 하나의 테이블 객체
                SqlDataAdapter da = new SqlDataAdapter(cmd); //select 구문이 들어감
                ds = new DataSet();
                da.Fill(ds, "BookTable");

                dt_book = ds.Tables[0]; //select 결과값을 DataTable에 넣는다.
            }

            conn.Close(); //연결 해제
        }

        public static void insertQuery_BookForm(string isbn, string bookName, string publisher, string page)
        {

            try
            {
                ConnectDB();
                string sqlcommand = "Insert Into BookTable (Isbn, Name, Publisher, Page, isBorrowed) values (@p1, @p2, @p3, @p4, 0)";
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", isbn);
                cmd.Parameters.AddWithValue("@p2", bookName);
                cmd.Parameters.AddWithValue("@p3", publisher);
                cmd.Parameters.AddWithValue("@p4", page);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception e)
            {
            }
        }

        public static void updateQuery_BookForm(string bookName, string publisher, string page, string isbn)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string sqlcommand;
                //Isbn, Name, Publisher, Page,
                sqlcommand = "Update BookTable set Name = @p1, Publisher = @p2, Page = @p3 where Isbn = @p4";

                cmd.Parameters.AddWithValue("@p1", bookName);
                cmd.Parameters.AddWithValue("@p2", publisher);
                cmd.Parameters.AddWithValue("@p3", page);
                cmd.Parameters.AddWithValue("@p4", isbn);

                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception eee)
            {
            }

        }

        public static void deleteQuery_BookForm(string isbn)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string sqlcommand;
                //Isbn, Name, Publisher, Page,
                sqlcommand = "Delete BookTable where Isbn = @p1";

                cmd.Parameters.AddWithValue("@p1", isbn);

                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception eee)
            {
            }

        }
        public static void updateQuery_Book_UserForm(string id, string name, bool isRemove = false)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string sqlcommand;
                //Isbn, Name, Publisher, Page,
                if (isRemove)
                {
                    sqlcommand = "Update BookTable set UserName = null, isBorrowed = 0, BorrowedAt = null where UserId = @p1";
                    cmd.Parameters.AddWithValue("@p1", id);
                }
                else
                {
                    sqlcommand = "Update BookTable set UserName = @p1 where UserId = @p2";
                    cmd.Parameters.AddWithValue("@p1", name);
                    cmd.Parameters.AddWithValue("@p2", id);
                }


                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception eee)
            {
            }
        }

        public static void updateQuery_User_UserForm(string name, string id)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string sqlcommand;
                //Isbn, Name, Publisher, Page,
                sqlcommand = "Update UserTable set Name = @p1 where Id = @p2";

                cmd.Parameters.AddWithValue("@p1", name);
                cmd.Parameters.AddWithValue("@p2", id);

                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception eee)
            {
            }
        }

        public static void deleteQuery_UserForm(string id)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string sqlcommand;
                //Isbn, Name, Publisher, Page,
                sqlcommand = "Delete UserTable where Id = @p1";

                cmd.Parameters.AddWithValue("@p1", id);

                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception eee)
            {
            }

        }

        public static void insertQuery_User_UserForm(string id, string name)
        {

            try
            {
                ConnectDB();
                string sqlcommand = "Insert Into UserTable (Id, Name) values (@p1, @p2)";
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", id);
                cmd.Parameters.AddWithValue("@p2", name);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); //쿼리 실행
                conn.Close();
            }
            catch (Exception e)
            {
            }
        }



    }
}
