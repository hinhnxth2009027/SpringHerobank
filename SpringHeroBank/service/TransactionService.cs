using System;
using MySql.Data.MySqlClient;
using SpringHeroBank2.entity;
using SpringHeroBank2.helper;

namespace SpringHeroBank2.service
{
    public class TransactionService
    {
        private Random _random = new Random();
        private ConnectionHelper _connectionHelper = new ConnectionHelper();

        public int GenerateRandomNumbers()
        {
            return int.Parse(_random.Next(10, 99).ToString() + _random.Next(10, 99).ToString() +
                             _random.Next(100, 999).ToString());
        }

        public Account CheckUserExistence(string accountNumber)
        {
            var connection = _connectionHelper.Connection();
            var cmd = new MySqlCommand() {Connection = connection};
            Account account = null;
            try
            {
                cmd.CommandText = $"SELECT * from accounts Where AccountNumber = '{accountNumber}'";
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        account = new Account()
                        {
                            FullName = result["FullName"].ToString(),
                            Email = result["FullName"].ToString(),
                            Phone = result["Phone"].ToString(),
                            AccountNumber = result["AccountNumber"].ToString(),
                            BirthDay = result["Birthday"].ToString()
                        };
                    }
                }

                result.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Hiện tại không có kết nối vui lòng thử lại sau");
            }

            return account;
        }
    }
}