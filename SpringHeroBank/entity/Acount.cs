using System;
using Google.Protobuf.WellKnownTypes;

namespace SpringHeroBank.entity
{
    public class Acount
    {
        //tài khoản
        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        //mật khẩu đã mã hóa và muối mã hóa
        public string password { get; set; }
        public string salt { get; set; }
        //số dư tài khoản
        public float balance { get; set; }
        //các thông tin khác
        public string phoneNumber { get; set; }
        public string cardNumber { get; set; }
        public string birthDay { get; set; }
        public int status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime deletedAt { get; set; }
        //constructor create user
        public Acount(string userName, string email, string password, string salt, string phoneNumber, string cardNumber, string birthDay, DateTime createdAt, DateTime updatedAt)
        {
            this.userName = userName;
            this.email = email;
            this.password = password;
            this.salt = salt;
            this.phoneNumber = phoneNumber;
            this.cardNumber = cardNumber;
            this.birthDay = birthDay;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
        public override string ToString()
        {
            return $"User name : {this.userName} | balance : {this.balance} | email: {this.email} | phone number: {this.phoneNumber} | birthday: {this.birthDay} | card number: {this.cardNumber} | password:{this.password} | salt: {this.salt} | createdAt: {this.createdAt} | updatedAt: {this.updatedAt}";
        }
    }
}