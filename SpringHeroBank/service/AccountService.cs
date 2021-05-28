using System;
using SpringHeroBank2.entity;
using SpringHeroBank2.helper;
using SpringHeroBank2.model;

namespace SpringHeroBank2.service
{
    public class AccountService
    {
        private Random _random = new Random();
        private MD5Helper _md5Helper = new MD5Helper();
        private AccountModel _accountModel = new AccountModel();


        public void CreateAccountService(Account account)
        {
            var salt = _random.Next(100000000, 999999999).ToString();
            //tạo ra mã thẻ ngân hàng
            var accountNumber = _random.Next(1000, 9999).ToString() + _random.Next(1000, 9999).ToString() + _random.Next(1000, 9999).ToString() + _random.Next(1000, 9999).ToString();
            //mã hóa mật khẩu
            var passwordHash = _md5Helper.PasswordHash(account.Password, salt);
            //tạo mới đối tượng người dùng với thông tin đầy đủ
            var accountCreate = new Account()
            {
                FullName = account.FullName,
                Email = account.Email,
                Phone = account.Phone,
                BirthDay = account.BirthDay,
                PasswordHash = passwordHash,
                Salt = salt,
                AccountNumber = accountNumber,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            
            // Console.WriteLine($"FullName : {accountCreate.FullName}");
            // Console.WriteLine($"Email : {accountCreate.Email}");
            // Console.WriteLine($"Phone : {accountCreate.Phone}");
            // Console.WriteLine($"BirthDay : {accountCreate.BirthDay}");
            // Console.WriteLine($"PasswordHash : {accountCreate.PasswordHash}");
            // Console.WriteLine($"Salt : {accountCreate.Salt}");
            // Console.WriteLine($"AccountNumber : {accountCreate.AccountNumber}");
            // Console.WriteLine($"CreatedAt : {accountCreate.CreatedAt}");
            // Console.WriteLine($"UpdatedAt : {accountCreate.UpdatedAt}");
            
            //chuyển thông tin người dùng vừa tạo qua model để đưa vào database
            var created = _accountModel.CreateNewAccount(accountCreate);
            if (created)
            {
                Console.WriteLine($"\nTạo mới tài khoản thành công mã số tài khoản của bạn là : {accountNumber}\n");
            }
            else
            {
                Console.WriteLine("\nTạo tài khoản thất bại vui lòng kiểm tra lại kết nối !\n");
            }
        }
    }
}