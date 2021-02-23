using System;

namespace loan_calculator_whatsapp_chatbot.Contracts
{
    public class MessageModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}