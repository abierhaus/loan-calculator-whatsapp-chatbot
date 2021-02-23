using System;

namespace loan_calculator_whatsapp_chatbot.Contracts
{
    public class ResponseModel
    {
        public MessageModel Message { get; set; }
        
        public LoanModel Loan { get; set; }

        public decimal Rate { get; set; }


        public string Body { get; set; }

        public StatusEnum Status { get; set; }

        public enum StatusEnum
        {
            Step0 = 0,
            Step1 = 1,
            Step2 = 2,
            Finished = 3
        }
    }
}