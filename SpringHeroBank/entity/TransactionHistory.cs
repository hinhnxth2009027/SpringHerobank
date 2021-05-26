using System;

namespace SpringHeroBank.entity
{
    public class TransactionHistory
    {
        public int id { get; set; }
        public string time { get; set; }
        public string transactionType { get; set; }
        public string status { get; set; } //1 giao dich thanh cong //0 da huy giao dich
        public string transactionContent { get; set; }
        public string cardUser { get; set; }


        public TransactionHistory(string time, string transactionType, string status, string transactionContent, string cardUser)
        {
            this.time = time;
            this.transactionType = transactionType;
            this.status = status;
            this.transactionContent = transactionContent;
            this.cardUser = cardUser;
        }
        public override string ToString()
        {
            return $"time : {this.time} || transaction type : {this.transactionType} \t||\t status : {this.status} \t||\t message : {this.transactionContent}";
        }
    }
}