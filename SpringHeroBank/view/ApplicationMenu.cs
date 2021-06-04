using System;
using System.Text;
using SpringHeroBank2.controller;
using SpringHeroBank2.entity;
using SpringHeroBank2.service;

namespace SpringHeroBank2.view
{
    public class ApplicationMenu
    {
        public void ShowMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var accountController = new AccountController();
            var guestController = new GuestController();
            Account accountLogin = null;

            while (true)
            {
                int choice;
                if (accountLogin == null)
                {
                    Console.WriteLine("\n\n||============|| Spring Hero Bank ||============||");
                    Console.WriteLine("||  Chọn 1 để login                             ||");
                    Console.WriteLine("||  Chọn 2 để đăng kí                           ||");
                    Console.WriteLine("||  Chọn 3 để thoát trương trình                ||");
                    Console.WriteLine("||==============================================||");
                    Console.WriteLine("\nLựa chọn của bạn là");
                    choice = int.Parse(Console.ReadLine());

                    if (choice == 1 || choice == 2 || choice == 3)
                    {
                        switch (choice)
                        {
                            case 1:
                                accountLogin = guestController.LoginController();
                                break;
                            case 2:
                                guestController.CreateAccountController();
                                break;
                            case 3:
                                Console.WriteLine("bye bye !!!");
                                break;
                        }

                        if (choice == 3)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nVui lòng nhập vào các số tương ứng trong menu\n");
                    }
                }
                else
                {
                    Console.WriteLine(
                        $"=====|| Spring Hero Bank ||===|| User : {accountLogin.FullName} ||===|| Balance : ${accountLogin.Balance} ||===|| phone : {accountLogin.Phone} ||===|| Card number : {accountLogin.AccountNumber} ||=====\n");
                    Console.WriteLine("||==================|| MENU ||==================||");
                    Console.WriteLine("|| Chọn 1 để nạp thêm tiền vào tài khoản        ||");
                    Console.WriteLine("|| Chọn 2 để rút tiền từ tài khoản              ||");
                    Console.WriteLine("|| Chọn 3 để chuyển tiền                        ||");
                    Console.WriteLine("|| Chọn 4 để xem lịch sử giao dịch              ||");
                    Console.WriteLine("|| Chọn 5 để xem thông tin profile              ||");
                    Console.WriteLine("|| Chọn 6 để tra cứu giao dịch                  ||");
                    Console.WriteLine("|| Chọn 7 để đăng xuất                          ||");
                    Console.WriteLine("||==============================================||\n");
                    Console.WriteLine("\nLựa chọn của bạn là");
                    choice = int.Parse(Console.ReadLine());
                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6)
                    {
                        switch (choice)
                        {
                            case 1:
                                accountLogin = accountController.Recharge(accountLogin);
                                break;
                            case 2:
                                accountLogin = accountController.Withdrawal(accountLogin);
                                break;
                            case 3:
                                accountLogin = accountController.Transfer(accountLogin);
                                break;
                            case 4:
                                accountController.ShowTransactionHistory(accountLogin);
                                break;
                            case 5:
                                accountController.ShowInformation(accountLogin);
                                break;
                            case 7:
                                accountLogin = null;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nVui lòng nhập vào các số tương ứng trong menu\n");
                    }
                }
            }
        }
    }
}