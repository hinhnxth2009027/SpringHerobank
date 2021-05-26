using System;
using System.Text;
using SpringHeroBank.controller;
using SpringHeroBank.entity;

namespace SpringHeroBank.view
{
    public class ApplicationMenu
    {
        public Acount userLogin = null;

        public void menu()
        {
            GuestController guestController = new GuestController();
            AcountController acountController = new AcountController();
            Console.OutputEncoding = Encoding.UTF8;
            int choice;
            while (true)
            {
                if (userLogin == null)
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
                                userLogin = guestController.login();
                                break;
                            case 2:
                                guestController.createNewUser();
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
                        $"=====|| Spring Hero Bank ||===|| User : {userLogin.userName} ||===|| Balance : ${userLogin.balance} ||===|| phone : {userLogin.phoneNumber} ||===|| Card number : {userLogin.cardNumber} ||=====\n");
                    Console.WriteLine("||==================|| MENU ||==================||");
                    Console.WriteLine("|| Chọn 1 để nạp thêm tiền vào tài khoản        ||");
                    Console.WriteLine("|| Chọn 2 để rút tiền từ tài khoản              ||");
                    Console.WriteLine("|| Chọn 3 để chuyển tiền                        ||");
                    Console.WriteLine("|| Chọn 4 để xem lịch sử giao dịch              ||");
                    Console.WriteLine("|| Chọn 5 để xem thông tin profile              ||");
                    Console.WriteLine("|| Chọn 6 để đăng xuất                          ||");
                    Console.WriteLine("||==============================================||\n");
                    Console.WriteLine("\nLựa chọn của bạn là");
                    choice = int.Parse(Console.ReadLine());


                    if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6)
                    {
                        switch (choice)
                        {
                            case 1:
                                userLogin = acountController.recharge(userLogin);
                                break;
                            case 2:
                                userLogin = acountController.Withdrawal(userLogin);
                                break;
                            case 3:
                                userLogin = acountController.transfer(userLogin);
                                break;
                            case 4:
                                acountController.TransactionHistory(userLogin.cardNumber);
                                break;
                            case 5:
                                Console.WriteLine("\n");
                                Console.WriteLine(userLogin.ToString());
                                Console.WriteLine("\n");
                                break;
                            case 6:
                                userLogin = null;
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