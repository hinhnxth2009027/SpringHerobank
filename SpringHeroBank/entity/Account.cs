using System;

namespace SpringHeroBank2.entity
{
    public class Account
    {
        //id người dùng tự động được tạo và sinh ra trong database
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        
        //trường password này không xuất hiện trong database mà chỉ giúp ích trong quá trình khởi tạo đối tượng Account
        public string Password { get; set; }
        
        //mật khẩu đã mã hóa được tạo theo công thức ( mật khẩu người dùng nhập + muối qua hàm md5 )
        public string PasswordHash { get; set; }

        //muối được sinh ra ngẫu nhiên
        public string Salt { get; set; }

        //số dư của người dùng default = 0
        public double Balance { get; set; }

        //là mã số ngân hàng của người dùng bao gồm 16 chữ số được sinh ra ngẫu nhiên
        public string AccountNumber { get; set; }
        public string BirthDay { get; set; }

        //trạng thái tài khoản người dùng 1 là vẫn đang hoạt động , 0 đã bị thu hồi quyền hoạt động
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        
    }
}