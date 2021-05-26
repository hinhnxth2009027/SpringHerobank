using System;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using SpringHeroBank.entity;
using SpringHeroBank.helper;
namespace SpringHeroBank.service
{
    public class Service
    {
        private ConnectionHelper conn = new ConnectionHelper();
        public User receiver(string receiverCode)
        {
            MySqlConnection connection = conn.Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;

            try
            {
                cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{receiverCode}'";
                MySqlDataReader result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    User user = null;
                    while (result.Read())
                    {
                        var userName = result["userName"].ToString();
                        var email = result["email"].ToString();
                        var password = result["password"].ToString();
                        var salT = result["salt"].ToString();
                        var balance = float.Parse(result["balance"].ToString());
                        var phoneNumber = result["phoneNumber"].ToString();
                        var cardNumber1 = result["cardNumber"].ToString();
                        var birthday = result["birthday"].ToString();
                        var createdAt = DateTime.Parse(result["createdAt"].ToString());
                        var updatedAt = DateTime.Parse(result["updatedAt"].ToString());
                        user = new User(userName, email, password, salT, phoneNumber, cardNumber1, birthday, createdAt, updatedAt);
                    }
                    result.Close();
                    return user;
                }
                else
                {
                    result.Close();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public string HashPassword(string before_pass, string salt)
        {
            string pass_string = before_pass + salt;
            StringBuilder affter_pass = new StringBuilder();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(new UTF8Encoding().GetBytes(pass_string));
            for (int i = 0; i < bytes.Length; i++)
            {
                affter_pass.Append(bytes[i].ToString("x2"));
            }
            return affter_pass.ToString();
        }
    }
}