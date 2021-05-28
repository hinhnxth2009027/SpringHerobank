using System;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto;
using SpringHeroBank2.entity;
using SpringHeroBank2.model;
using SpringHeroBank2.service;

namespace SpringHeroBank2.controller
{
    public class AccountController
    {
        private TransactionModel _transactionModel = new TransactionModel();
        private TransactionService _transactionService = new TransactionService();

        public Account Recharge(Account account)
        {
            Console.WriteLine("\nNhập vào số tiền bạn muốn nạp :");
            var money = double.Parse(Console.ReadLine());
            if (money <= 0)
            {
                Console.WriteLine($"\nKhông thể nạp ${money} vào ví , yêu cầu nạp tối thiểu từ $1\n");
                return account;
            }
            else
            {
                Console.WriteLine("\nXác nhận nạp tiền\nChọn 1 để tiếp tục\nChọn 2 để bỏ qua\n");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine($"\nĐã nạp thành công ${money} vào tài khoản\n");
                    return _transactionModel.Recharge(account.AccountNumber, money, money + account.Balance);
                }
                else
                {
                    return account;
                }
            }
        }

        public Account Withdrawal(Account account)
        {
            Console.WriteLine("\nNhập vào số tiền bạn muốn rút :");
            var money = double.Parse(Console.ReadLine());
            if (money > account.Balance)
            {
                Console.WriteLine($"\nKhông thể rút ${money} vì tài khoản của bạn không đủ\n");
                return account;
            }
            else
            {
                Console.WriteLine("\nXác nhận rút tiền\nChọn 1 để tiếp tục\nChọn 2 để bỏ qua\n");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine($"\nĐã rút thành công ${money} từ tài khoản\n");
                    return _transactionModel.Withdrawal(account.AccountNumber, money, account.Balance - money);
                }
                else
                {
                    return account;
                }
            }
        }

        public Account Transfer(Account account)
        {
            Account user = null;
            Console.WriteLine("\nNhập mã người nhận");
            var recipientCode = Console.ReadLine();

            if (recipientCode == account.AccountNumber)
            {
                Console.WriteLine("\nBạn không thể tự chuyển tiền cho chính mình !\n");
                return account;
            }
            else
            {
                var checkAccount = _transactionService.CheckUserExistence(recipientCode);
                if (checkAccount == null)
                {
                    Console.WriteLine($"\nKhông tìm thấy thông tin người dùng nào tương ứng với : {recipientCode}\n");
                    return account;
                }
                else
                {
                    Console.WriteLine(
                        $"\nTìm thấy người dùng : {checkAccount.FullName} tương ứng với : mã số : {checkAccount.AccountNumber}\n");
                    Console.WriteLine("Chọn 1 để tiếp tục \nChọn 2 để bỏ qua\n");
                    var choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        Console.WriteLine("\nNhập vào số tiền bạn muốn chuyển");
                        var money = double.Parse(Console.ReadLine());
                        if (money > account.Balance)
                        {
                            Console.WriteLine("\n Tài khoản của bạn không đủ\n");
                            return account;
                        }
                        Console.WriteLine("\nNhập vào message bạn muốn gửi");
                        var mess = Console.ReadLine();
                        return _transactionModel.Transfer(account.AccountNumber, checkAccount.AccountNumber, money,
                            mess);
                    }
                    else
                    {
                        Console.WriteLine("\nĐã hủy giao dịch");
                        return account;
                    }
                }
            }
        }

        public void ShowTransactionHistory(Account account)
        {
            var transactions = _transactionModel.TransactionHistory(account.AccountNumber);
            if (transactions.Count == 0)
            {
                Console.WriteLine("\nBạn chưa có giao dịch nào\n");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    transaction.ToString();
                }
            }
        }

        public void ShowInformation(Account account)
        {
            Console.WriteLine("\n\n||======================| Information |====================||");
            Console.WriteLine($"- Người dùng : {account.FullName}");
            Console.WriteLine($"- Số dư tài khoản : ${account.Balance}");
            Console.WriteLine($"- Mã số tài khoản : {account.AccountNumber}");
            Console.WriteLine($"- Số điện thoại : {account.Phone}");
            Console.WriteLine($"- Email : {account.Email}");
            Console.WriteLine($"- Bảo mật : {account.PasswordHash}");
            Console.WriteLine($"- Ngày sinh : {account.BirthDay}");
            Console.WriteLine($"- Ngày tham gia : {account.CreatedAt}");
            Console.WriteLine("||======================| Information |====================||\n\n");
        }
    }
}