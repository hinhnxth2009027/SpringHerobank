using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SpringHeroBank.entity;
using SpringHeroBank.model;
using SpringHeroBank.service;
namespace SpringHeroBank.controller
{
    public class AcountController
    {
        private AcountModel _acountModel = new AcountModel();
        private Service _service = new Service();
        public Acount recharge(Acount user)
        {
            Console.WriteLine("Nhập vào số tiền bạn muốn nạp");
            double money = Double.Parse(Console.ReadLine());
            int choice;
            Console.WriteLine("Xác nhận giao dịch chọn 1");
            Console.WriteLine("Hủy giao dịch chọn 2\n");
            choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                return _acountModel.recharge(user.cardNumber, money);
                ;
            }
            else
            {
                return user;
            }
        }
        public Acount Withdrawal(Acount user)
        {
            Console.WriteLine("Nhập vào số tiền bạn muốn rút");
            double money = Double.Parse(Console.ReadLine());
            if (money > user.balance)
            {
                Console.WriteLine("Tài khoản không đủ để thực hiện rút tiền\n");
                return user;
            }
            else
            {
                int choice;
                Console.WriteLine("Xác nhận giao dịch chọn 1");
                Console.WriteLine("Hủy giao dịch chọn 2\n");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    return _acountModel.Withdrawal(user.cardNumber, money);
                }
                else
                {
                    return user;
                }
            }
        }
        public Acount transfer(Acount user)
        {
            Console.WriteLine("Nhập vào mã người nhận");
            string recipientCode = Console.ReadLine();
            Acount checkUser = _service.receiver(recipientCode);
            if (checkUser == null)
            {
                Console.WriteLine($"\nKhông tìm thấy người dùng nào với mã thẻ : {recipientCode} !\n");
                return user;
            }
            else if (user.cardNumber.Equals(checkUser.cardNumber))
            {
                Console.WriteLine("\nBạn không thể tự chuyển tiền cho chính mình !\n");
                return user;
            }
            else
            {
                Console.WriteLine("\n Đây có phải người dùng bạn tìm kiếm");
                Console.WriteLine(
                    $"\n name : {checkUser.userName} \t phone : {checkUser.phoneNumber} \t card number : {checkUser.cardNumber}\n\n");
                Console.WriteLine("Chọn 1 để tiếp tục");
                Console.WriteLine("Chọn 2 để bỏ qua\n");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine(
                        $"Nhập vào số tiền bạn muốn chuyển tới : {checkUser.userName} với mã số tài khoản : {checkUser.cardNumber}\n");
                    double money = double.Parse(Console.ReadLine());
                    if (money > user.balance)
                    {
                        Console.WriteLine("\nTài khoản của bạn không đủ để tiếp tục giao dịch\n");
                        return user;
                    }
                    else
                    {
                        Acount userr = _acountModel.transfer(user.cardNumber, checkUser.cardNumber, money);
                        if (userr != null)
                        {
                            return userr;
                        }
                        else
                        {
                            return user;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Đã ngừng giao dịch");
                    return user;
                }
            }
        }
        public void TransactionHistory(string cardNumber)
        {
            List<entity.TransactionHistory> transactionHistories = _acountModel.TransactionList(cardNumber);
            if (transactionHistories.Count == 0)
            {
                Console.WriteLine("\n===========================");
                Console.WriteLine("Bạn chưa có giao dịch nào");
                Console.WriteLine("===========================\n");
            }
            else
            {
                Console.WriteLine("\n===========================================================================================================================================================");
                foreach (TransactionHistory User in transactionHistories)
                {
                    Console.WriteLine(User.ToString());
                }
                Console.WriteLine("===========================================================================================================================================================\n");
            }
        }
    }
}