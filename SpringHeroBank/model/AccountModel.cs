using System;
using MySql.Data.MySqlClient;
using SpringHeroBank2.entity;
using SpringHeroBank2.helper;
using SpringHeroBank2.service;

namespace SpringHeroBank2.model
{
    public class AccountModel
    {
        private ConnectionHelper _connectionHelper = new ConnectionHelper();
        private MD5Helper _md5Helper = new MD5Helper();
        private Account _account = null;


        public Boolean CreateNewAccount(Account account)
        {
            var connection = _connectionHelper.Connection();
            var cmd = new MySqlCommand {Connection = connection};
            try
            {
                cmd.CommandText =
                    "INSERT INTO accounts(FullName,Email,Phone,PasswordHash,Salt,AccountNumber,Birthday,CreatedAt,UpdatedAt) VALUES(@FullName,@Email,@Phone,@PasswordHash,@Salt,@AccountNumber,@Birthday,@CreatedAt,@UpdatedAt)";
                cmd.Parameters.AddWithValue("@FullName", account.FullName);
                cmd.Parameters.AddWithValue("@Email", account.Email);
                cmd.Parameters.AddWithValue("@Phone", account.Phone);
                cmd.Parameters.AddWithValue("@PasswordHash", account.PasswordHash);
                cmd.Parameters.AddWithValue("@Salt", account.Salt);
                cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                cmd.Parameters.AddWithValue("@Birthday", account.BirthDay);
                cmd.Parameters.AddWithValue("@CreatedAt", account.CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", account.UpdatedAt);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                return false;
            }
        }


        public Account Login(string accountTxt, string password)
        {
            var connection = _connectionHelper.Connection();
            var cmd = new MySqlCommand() {Connection = connection};
            try
            {
                cmd.CommandText = $"SELECT * from accounts where Email = '{accountTxt}'";
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var salt = result["Salt"];
                        var passwordHash = _md5Helper.PasswordHash(password, salt.ToString());
                        if (passwordHash.Equals(result["PasswordHash"].ToString()))
                        {
                            result.Close();
                            cmd.CommandText = $"SELECT * from accounts where PasswordHash = '{passwordHash}'";
                            var accountData = cmd.ExecuteReader();
                            while (accountData.Read())
                            {
                                _account = new Account()
                                {
                                    FullName = accountData["FullName"].ToString(),
                                    Email = accountData["Email"].ToString(),
                                    Phone = accountData["Phone"].ToString(),
                                    PasswordHash = accountData["PasswordHash"].ToString(),
                                    Salt = accountData["Salt"].ToString(),
                                    Balance = double.Parse(accountData["Balance"].ToString()),
                                    AccountNumber = accountData["AccountNumber"].ToString(),
                                    BirthDay = accountData["Birthday"].ToString(),
                                    Status = int.Parse(accountData["Status"].ToString()),
                                    CreatedAt = DateTime.Parse(accountData["CreatedAt"].ToString()),
                                    UpdatedAt = DateTime.Parse(accountData["UpdatedAt"].ToString())
                                };
                                accountData.Close();
                                return _account;
                            }
                        }
                        else
                        {
                            result.Close();
                            Console.WriteLine("\nSai mật khẩu !\n");
                            _account = null;
                            return null;
                        }
                    }
                }
                else
                {
                    result.Close();
                    cmd.CommandText = $"SELECT * from accounts where AccountNumber = '{accountTxt}'";
                    var result2 = cmd.ExecuteReader();
                    if (result2.HasRows)
                    {
                        while (result2.Read())
                        {
                            var salt = result2["Salt"];
                            var passwordHash = _md5Helper.PasswordHash(password, salt.ToString());
                            if (passwordHash.Equals(result2["PasswordHash"].ToString()))
                            {
                                result2.Close();
                                cmd.CommandText = $"SELECT * from accounts where PasswordHash = '{passwordHash}'";
                                var accountData = cmd.ExecuteReader();
                                while (accountData.Read())
                                {
                                    _account = new Account()
                                    {
                                        FullName = accountData["FullName"].ToString(),
                                        Email = accountData["Email"].ToString(),
                                        Phone = accountData["Phone"].ToString(),
                                        PasswordHash = accountData["PasswordHash"].ToString(),
                                        Salt = accountData["Salt"].ToString(),
                                        Balance = double.Parse(accountData["Balance"].ToString()),
                                        AccountNumber = accountData["AccountNumber"].ToString(),
                                        BirthDay = accountData["Birthday"].ToString(),
                                        Status = int.Parse(accountData["Status"].ToString()),
                                        CreatedAt = DateTime.Parse(accountData["CreatedAt"].ToString()),
                                        UpdatedAt = DateTime.Parse(accountData["UpdatedAt"].ToString())
                                    };
                                    accountData.Close();
                                    return _account;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nSai mật khẩu !\n");
                                _account = null;
                                return null;
                            }
                        }
                    }
                    else
                    {
                        result2.Close();
                        Console.WriteLine($"\nKhông tìm thấy thông tin người dùng tương ứng với : {accountTxt}\n");
                        _account = null;
                        return null;
                    }
                }

                return _account;
            }
            catch (MySqlException e)
            {
                _account = null;
                return null;
            }
        }
    }
}