using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using SpringHeroBank.entity;
using SpringHeroBank.helper;
using SpringHeroBank.service;

namespace SpringHeroBank.model
{
    public class AcountModel
    {
        private ConnectionHelper conn = new ConnectionHelper();
        private Service _service = new Service();
        private Acount _user = null;
        private Boolean islogin = false;

        public void store(Acount user)
        {
            MySqlConnection connection = conn.Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText =
                    "INSERT INTO users(userName,email,password,salt,phoneNumber,cardNumber,birthDay,createdAt,updatedAt) VALUES (?userName,?email,?password,?salt,?phoneNumber,?cardNumber,?birthDay,?createdAt,?updatedAt)";
                cmd.Parameters.Add("?userName", MySqlDbType.VarChar).Value = user.userName;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = user.email;
                cmd.Parameters.Add("?password", MySqlDbType.VarChar).Value = user.password;
                cmd.Parameters.Add("?salt", MySqlDbType.VarChar).Value = user.salt;
                cmd.Parameters.Add("?phoneNumber", MySqlDbType.VarChar).Value = user.phoneNumber;
                cmd.Parameters.Add("?cardNumber", MySqlDbType.VarChar).Value = user.cardNumber;
                cmd.Parameters.Add("?birthDay", MySqlDbType.VarChar).Value = user.birthDay;
                cmd.Parameters.Add("?createdAt", MySqlDbType.DateTime).Value = user.createdAt;
                cmd.Parameters.Add("?updatedAt", MySqlDbType.DateTime).Value = user.updatedAt;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Tạo mới tài khoản thành công !");
                Console.WriteLine($"Mã thẻ ngân hàng của bạn là : {user.cardNumber}\n");
            }
            catch (MySqlException err)
            {
                Console.WriteLine("Tạo mới tài khoản thất bại vui lòng thử lại\n");
            }
        }

        public Acount login(string acount, string pass)
        {
            MySqlConnection connection = conn.Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText = $"SELECT * from users WHERE email = '{acount}'";
                MySqlDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var salt = result["salt"];
                        var hashPass = _service.HashPassword(pass, salt.ToString());
                        if (hashPass.Equals(result["password"].ToString()))
                        {
                            result.Close();
                            cmd.CommandText = $"SELECT * from users WHERE password = '{hashPass}'";
                            MySqlDataReader userData = cmd.ExecuteReader();
                            while (userData.Read())
                            {
                                var userName = userData["userName"].ToString();
                                var email = userData["email"].ToString();
                                var password = userData["password"].ToString();
                                var salT = userData["salt"].ToString();
                                var balance = float.Parse(userData["balance"].ToString());
                                var phoneNumber = userData["phoneNumber"].ToString();
                                var cardNumber = userData["cardNumber"].ToString();
                                var birthday = userData["birthday"].ToString();
                                var createdAt = DateTime.Parse(userData["createdAt"].ToString());
                                var updatedAt = DateTime.Parse(userData["updatedAt"].ToString());
                                this._user = new Acount(userName, email, password, salT, phoneNumber, cardNumber,
                                    birthday, createdAt, updatedAt);
                                this._user.balance = balance;
                                userData.Close();
                                Console.WriteLine("\nĐăng nhập thành công\n");
                                return this._user;
                            }
                        }
                        else
                        {
                            islogin = false;
                        }
                    }

                    if (!islogin)
                    {
                        Console.WriteLine("\nSai mật khẩu\n");
                    }

                    result.Close();
                }


                else
                {
                    result.Close();
                    cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{acount}'";
                    MySqlDataReader result2 = cmd.ExecuteReader();

                    if (result2.HasRows)
                    {
                        while (result2.Read())
                        {
                            var salt = result2["salt"];
                            var hashPass = _service.HashPassword(pass, salt.ToString());
                            if (hashPass.Equals(result2["password"].ToString()))
                            {
                                result2.Close();
                                cmd.CommandText = $"SELECT * from users WHERE password = '{hashPass}'";
                                MySqlDataReader userData = cmd.ExecuteReader();
                                while (userData.Read())
                                {
                                    var userName = userData["userName"].ToString();
                                    var email = userData["email"].ToString();
                                    var password = userData["password"].ToString();
                                    var salT = userData["salt"].ToString();
                                    var balance = float.Parse(userData["balance"].ToString());
                                    var phoneNumber = userData["phoneNumber"].ToString();
                                    var cardNumber = userData["cardNumber"].ToString();
                                    var birthday = userData["birthday"].ToString();
                                    var createdAt = DateTime.Parse(userData["createdAt"].ToString());
                                    var updatedAt = DateTime.Parse(userData["updatedAt"].ToString());
                                    this._user = new Acount(userName, email, password, salT, phoneNumber, cardNumber,
                                        birthday, createdAt, updatedAt);
                                    this._user.balance = balance;
                                    userData.Close();
                                    result2.Close();
                                    Console.WriteLine("\nĐăng nhập thành công\n");
                                    return this._user;
                                }
                            }
                            else
                            {
                                islogin = false;
                            }
                        }

                        if (!islogin)
                        {
                            Console.WriteLine("\nSai mật khẩu\n");
                            result2.Close();
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nKhông tìm thấy tài khoản nào tương ứng với : {acount}\n");
                        result2.Close();
                        return null;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("err roi ne " + e + "\n");
                throw;
            }

            return null;
        }

        public Acount recharge(string cardNumber, double money)
        {
            double new_balane = 0;
            MySqlConnection connection = conn.Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{cardNumber}'";
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    new_balane = money + double.Parse(result["balance"].ToString());
                }

                result.Close();
                cmd.CommandText = $"UPDATE users SET balance = {new_balane} WHERE cardNumber = '{cardNumber}'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{cardNumber}'";
                MySqlDataReader result2 = cmd.ExecuteReader();
                while (result2.Read())
                {
                    var userName = result2["userName"].ToString();
                    var email = result2["email"].ToString();
                    var password = result2["password"].ToString();
                    var salT = result2["salt"].ToString();
                    var balance = float.Parse(result2["balance"].ToString());
                    var phoneNumber = result2["phoneNumber"].ToString();
                    var cardNumber1 = result2["cardNumber"].ToString();
                    var birthday = result2["birthday"].ToString();
                    var createdAt = DateTime.Parse(result2["createdAt"].ToString());
                    var updatedAt = DateTime.Parse(result2["updatedAt"].ToString());

                    this._user = new Acount(userName, email, password, salT, phoneNumber, cardNumber1, birthday,
                        createdAt, updatedAt);
                    this._user.balance = balance;
                    Console.WriteLine("\nNạp tiền thành công\n");
                }

                result2.Close();
                cmd.CommandText =
                    $"INSERT INTO transactionhistory(cardUser,time,status,TransactionContent,transactionType) VALUES (?cardUser,?time,?status,?TransactionContent,?transactionType)";
                cmd.Parameters.Add("?cardUser", MySqlDbType.VarChar).Value = cardNumber;
                cmd.Parameters.Add("?time", MySqlDbType.VarChar).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("?status", MySqlDbType.Int64).Value = 1;
                cmd.Parameters.Add("?TransactionContent", MySqlDbType.VarChar).Value = $"Đã nạp ${money} vào tài khoản";
                cmd.Parameters.Add("?transactionType", MySqlDbType.VarChar).Value = "recharge";
                cmd.ExecuteNonQuery();
                return this._user;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Acount Withdrawal(string cardNumber, double money)
        {
            double new_balane = 0;
            MySqlConnection connection = conn.Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            try
            {
                cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{cardNumber}'";
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    new_balane = double.Parse(result["balance"].ToString()) - money;
                }

                result.Close();
                cmd.CommandText = $"UPDATE users SET balance = {new_balane} WHERE cardNumber = '{cardNumber}'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{cardNumber}'";
                MySqlDataReader result2 = cmd.ExecuteReader();
                while (result2.Read())
                {
                    var userName = result2["userName"].ToString();
                    var email = result2["email"].ToString();
                    var password = result2["password"].ToString();
                    var salT = result2["salt"].ToString();
                    var balance = float.Parse(result2["balance"].ToString());
                    var phoneNumber = result2["phoneNumber"].ToString();
                    var cardNumber1 = result2["cardNumber"].ToString();
                    var birthday = result2["birthday"].ToString();
                    var createdAt = DateTime.Parse(result2["createdAt"].ToString());
                    var updatedAt = DateTime.Parse(result2["updatedAt"].ToString());
                    this._user = new Acount(userName, email, password, salT, phoneNumber, cardNumber1,
                        birthday, createdAt, updatedAt);
                    this._user.balance = balance;
                    Console.WriteLine("\nRút tiền thành công\n");
                }

                result2.Close();
                cmd.CommandText =
                    $"INSERT INTO transactionhistory(cardUser,time,status,TransactionContent,transactionType) VALUES (?cardUser,?time,?status,?TransactionContent,?transactionType)";
                cmd.Parameters.Add("?cardUser", MySqlDbType.VarChar).Value = cardNumber;
                cmd.Parameters.Add("?time", MySqlDbType.VarChar).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("?status", MySqlDbType.Int64).Value = 1;
                cmd.Parameters.Add("?TransactionContent", MySqlDbType.VarChar).Value = $"Đã rút ${money} từ tài khoản";
                cmd.Parameters.Add("?transactionType", MySqlDbType.VarChar).Value = "Withdrawal";
                cmd.ExecuteNonQuery();
                return this._user;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public Acount transfer(string senderCode, string recipientCode, double money)
        {
            double new_balane_sender = 0;
            double new_balane_recipient = 0;
            MySqlConnection connection = conn.Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                // người chuyển tiền
                cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{senderCode}'";
                MySqlDataReader result1 = cmd.ExecuteReader();
                while (result1.Read())
                {
                    new_balane_sender = double.Parse(result1["balance"].ToString()) - money;
                }

                result1.Close();
                cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{recipientCode}'";
                MySqlDataReader result2 = cmd.ExecuteReader();
                while (result2.Read())
                {
                    new_balane_recipient = double.Parse(result2["balance"].ToString()) + money;
                }

                result2.Close();
                Console.WriteLine(
                    $"Xác nhận chuyển tiền tới : {recipientCode}\n Chọn 1 để tiếp tục \n Chọn 2 để hủy\n");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    cmd.CommandText =
                        $"UPDATE users SET balance = {new_balane_sender} WHERE cardNumber = '{senderCode}'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =
                        $"UPDATE users SET balance = {new_balane_recipient} WHERE cardNumber = '{recipientCode}'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"SELECT * from users WHERE cardNumber = '{senderCode}'";
                    MySqlDataReader result3 = cmd.ExecuteReader();
                    while (result3.Read())
                    {
                        var userName = result3["userName"].ToString();
                        var email = result3["email"].ToString();
                        var password = result3["password"].ToString();
                        var salT = result3["salt"].ToString();
                        var balance = float.Parse(result3["balance"].ToString());
                        var phoneNumber = result3["phoneNumber"].ToString();
                        var cardNumber1 = result3["cardNumber"].ToString();
                        var birthday = result3["birthday"].ToString();
                        var createdAt = DateTime.Parse(result3["createdAt"].ToString());
                        var updatedAt = DateTime.Parse(result3["updatedAt"].ToString());
                        this._user = new Acount(userName, email, password, salT, phoneNumber, cardNumber1,
                            birthday, createdAt, updatedAt);
                        this._user.balance = balance;
                    }

                    result3.Close();
                    cmd.CommandText =
                        $"INSERT INTO transactionhistory(cardUser,time,status,TransactionContent,transactionType) VALUES (?cardUser,?time,?status,?TransactionContent,?transactionType)";
                    cmd.Parameters.Add("?cardUser", MySqlDbType.VarChar).Value = senderCode;
                    cmd.Parameters.Add("?time", MySqlDbType.VarChar).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("?status", MySqlDbType.Int64).Value = 1;
                    cmd.Parameters.Add("?TransactionContent", MySqlDbType.VarChar).Value = $"Đã chuyển tới {recipientCode} ${money}";
                    cmd.Parameters.Add("?transactionType", MySqlDbType.VarChar).Value = $"transfer";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =
                        $"INSERT INTO transactionhistory(cardUser,time,status,TransactionContent,transactionType) VALUES (?cardUser1,?time1,?status1,?TransactionContent1,?transactionType1)";
                    cmd.Parameters.Add("?cardUser1", MySqlDbType.VarChar).Value = recipientCode;
                    cmd.Parameters.Add("?time1", MySqlDbType.VarChar).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("?status1", MySqlDbType.Int64).Value = 1;
                    cmd.Parameters.Add("?TransactionContent1", MySqlDbType.VarChar).Value = $"Đã nhận được ${money} từ {senderCode}";
                    cmd.Parameters.Add("?transactionType1", MySqlDbType.VarChar).Value = $"transfer";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Chuyển khoản thành công");
                }
                else
                {
                    cmd.CommandText =
                        $"INSERT INTO transactionhistory(cardUser,time,status,TransactionContent,transactionType) VALUES (?cardUser,?time,?status,?TransactionContent,?transactionType)";
                    cmd.Parameters.Add("?cardUser", MySqlDbType.VarChar).Value = senderCode;
                    cmd.Parameters.Add("?time", MySqlDbType.VarChar).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("?status", MySqlDbType.Int64).Value = 0;
                    cmd.Parameters.Add("?TransactionContent", MySqlDbType.VarChar).Value = $"Chuyển tới {recipientCode} ${money}";
                    cmd.Parameters.Add("?transactionType", MySqlDbType.VarChar).Value = $"transfer";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Đã hủy giao dịch\n");
                    this._user = null;
                }

                transaction.Commit();
                return this._user;
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
                Console.WriteLine("Xảy ra lỗi giao dịch này đã được bỏ qua");
                throw;
            }
        }


        public List<TransactionHistory> TransactionList(string cardNumber)
        {
            List<TransactionHistory> transactionHistories = new List<TransactionHistory>();

            MySqlConnection connection = conn.Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * from transactionhistory WHERE cardUser = '{cardNumber}'";
            MySqlDataReader result = cmd.ExecuteReader();
            List<TransactionHistory> dealHistories = new List<TransactionHistory>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    int status = int.Parse(result["status"].ToString());
                    string statusTXT;
                    if (status == 1)
                    {
                        statusTXT = "Giao dịch thành công";
                    }
                    else
                    {
                        statusTXT = "Đã hủy giao dịch";
                    }

                    var time = result["time"].ToString();
                    var cardUser = result["cardUser"].ToString();
                    var TransactionContent = result["TransactionContent"].ToString();
                    var transactionType = result["transactionType"].ToString();
                    TransactionHistory transactionHistory = new TransactionHistory(time,transactionType,statusTXT,TransactionContent,cardUser);
                    transactionHistories.Add(transactionHistory);
                }
            }
            result.Close();
            return transactionHistories;
        }
    }
}