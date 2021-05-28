using System;
using System.Collections.Generic;
using System.Text;
using SpringHeroBank2.entity;
using SpringHeroBank2.model;
using SpringHeroBank2.service;

namespace SpringHeroBank2.controller
{
    public class GuestController
    {
        private AccountService acountService = new AccountService();
        private AccountModel _accountModel = new AccountModel();


        public void CreateAccountController()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var _account = new Account();
            Console.WriteLine("Nhập đầy đủ họ tên:");
            _account.FullName = Console.ReadLine();
            Console.WriteLine("Nhập vào Email:");
            _account.Email = Console.ReadLine();
            Console.WriteLine("Nhập vào Số điện thoại:");
            _account.Phone = Console.ReadLine();
            Console.WriteLine("Nhập vào password:");
            _account.Password = Console.ReadLine();
            Console.WriteLine("Nhập vào ngày tháng năm sinh theo định dạng ( ngày-tháng-năm ):");
            _account.BirthDay = Console.ReadLine();
            //truyền các giá trị người dùng đã nhập vào service để tiếp tục sử lý đăng kí
            acountService.CreateAccountService(_account);
        }


        public Account LoginController()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\nVui lòng nhập vào Email hoặc mã số tài khoản");
            var account = Console.ReadLine();
            Console.WriteLine("\nNhập vào mật khẩu");
            var password = Console.ReadLine();
            var accountIsLogin = _accountModel.Login(account, password);
            return accountIsLogin;
        }
    }
}