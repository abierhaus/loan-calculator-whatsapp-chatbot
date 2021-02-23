using System;
using System.Diagnostics;
using System.Threading.Tasks;
using loan_calculator_whatsapp_chatbot.Contracts;
using loan_calculator_whatsapp_chatbot.Services;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace loan_calculator_whatsapp_chatbot.Controllers
{
    public class Request : TwilioController
    {


        public ILoanRequestService LoanRequestService { get; set; }


        public Request(ILoanRequestService loanRequestService)
        {
            LoanRequestService = loanRequestService;
        }

        /// <summary>
        /// Method for receicing replies via WebHook. Configure https://yourdomain.com/ in your twilio console
        /// </summary>
        /// <param name="incomingMessage"></param>
        /// <returns></returns>
    
       public async Task<TwiMLResult> Index(SmsRequest incomingMessage)
        {    
            //Return message
            var messagingResponse = new MessagingResponse();


            try
            {
                //Convert input to message
                var message = new MessageModel()
                {
                    To = incomingMessage.To,
                    From = incomingMessage.From,
                    Body = incomingMessage.Body,
                    CreatedDate = DateTime.Now
                };

                //Process request
                var reponse = await LoanRequestService.Process(message);



                messagingResponse.Message(reponse.Body);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                messagingResponse.Message(e.Message);
            }
           

            return TwiML(messagingResponse);
        }
    }
}