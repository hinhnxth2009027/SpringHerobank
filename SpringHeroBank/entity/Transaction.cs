using System;
using System.Text;

namespace SpringHeroBank2.entity
{
    public class Transaction
    {
        //id là mã giao dịch gồm 7 chữ số được sinh ra ngẫu nhiên trong quá trình thực hiện giao dịch
        public int Id { get; set; }


        //số tiền giao dịch
        public double Amount { get; set; }


        // phục vụ truy vấn trong database
        public string Code { get; set; }


        //mã của người gửi và người nhận trong giao dịch //nếu trong trường hợp nạp và rút tiền thì hai mã này đều là mã của chủ tài khoản

        public string SenderCode { get; set; }
        public string ReceiverCode { get; set; }


        //loại giao dịch //1 rút tiền //2 nạp tiền // 3 chuyển tiền
        public int Type { get; set; }


        //trong trường hợp rút và nạp thì message tự sinh ra // chuyển tiền cho phép nhập message
        public string Message { get; set; }


        //thông tin ngày tạo xửa và xóa giao dịch
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }


        public void ToString()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string trannTXT = "";
            if (this.Type == 1)
            {
                trannTXT = "Rút tiền";
            }
            else if (this.Type == 2)
            {
                trannTXT = "Nạp tiền";
            }
            else if (this.Type == 3)
            {
                trannTXT = "Chuyển tiền";
            }

            Console.WriteLine(
                $"|| Mã giao dịch : {Id} \t số tiền giao dịch ${Amount} \t người thực hiện : {SenderCode} \t người nhận : {ReceiverCode} \t loại giao dịch : {trannTXT}\n" +
                $"|| Message : {Message} \t ngày tạo : {CreateAt} \t ngày sửa : {UpdateAt}" +
                $"\n|| --------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
        }
    }
}