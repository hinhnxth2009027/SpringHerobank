using System;

namespace SpringHeroBank.entity
{
    public class DealHistory
    {
        public int Id { get; set; }
        public string time { get; set; }
        public int status { get; set; } //1 giao dich thanh cong //0 da huy giao dich
        public string TransactionContent { get; set; }
        public string cardUser { get; set; }
        public DealHistory(string time, int status, string transactionContent)
        {
            this.time = time;
            this.status = status;
            TransactionContent = transactionContent;
        }
        public override string ToString()
        {
            Console.WriteLine($"");
            return null;
        }
    }
}