using System;
using System.Text;
using SpringHeroBank.entity;
using SpringHeroBank.model;
using SpringHeroBank.service;

namespace SpringHeroBank.controller
{
    public class GuestController
    {
        static Random random = new Random();
        private AcountModel _acountModel = new AcountModel();
        private Service _service = new Service();
        private User isLogin = null;
        
        public void createNewUser()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Vui lòng nhập tên đầy đủ");
            var userName = Console.ReadLine();
            var salt = random.Next(100000000, 900000000).ToString();
            Console.WriteLine("Vui lòng nhập email");
            var email = Console.ReadLine();
            Console.WriteLine("Nhập vào password");
            var pass_before_hash = Console.ReadLine();
            var password = _service.HashPassword(pass_before_hash, salt);
            // Console.WriteLine("Mật khẩu được mã hóa thành : " + password);
            Console.WriteLine("Nhập vào số điện thoại");
            var phoneNumber = Console.ReadLine();
            Console.WriteLine("Nhập vào ngày tháng năm sinh");
            var birthDay = Console.ReadLine();
            var cardNumber = random.Next(1000, 9000).ToString() + random.Next(1000, 9000).ToString() + random.Next(1000, 9000).ToString() + random.Next(1000, 9000).ToString();
            DateTime createdAt = DateTime.Now;
            DateTime updatedAt = DateTime.Now;
            User newUser = new User(userName, email, password, salt, phoneNumber, cardNumber, birthDay, createdAt, updatedAt);
            // Console.WriteLine(newUser.ToString());
            _acountModel.store(newUser);
        }
        public User login()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Vui lòng nhập vào email hoặc mã thẻ");
            var acount = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập password");
            var password = Console.ReadLine(); 
            isLogin = _acountModel.login(acount, password);
            if (isLogin!=null)
            {
                // Console.WriteLine(isLogin.ToString());
                return isLogin;
            }

            return null;
        }
    }
}