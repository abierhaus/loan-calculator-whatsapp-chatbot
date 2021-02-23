using NUnit.Framework;
using loan_calculator_whatsapp_chatbot.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using loan_calculator_whatsapp_chatbot.Contracts;

namespace loan_calculator_whatsapp_chatbot.Services.Tests
{
    [TestFixture()]
    public class LoanRequestServiceTests
    {
        private ILoanRequestService _loanRequestService;
        private ILoanCalculatorService _loanCalculatorService;


        [SetUp]
        public void SetUp()
        {

            //Delete old temp database
            string databasePath = "\\App_Data\\LiteDb.db";

            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }


            _loanCalculatorService = new LoanCalculatorService();
            _loanRequestService = new LoanRequestService(_loanCalculatorService);


        }


        [Test()]
        public async Task Process_HappyPath_Returnmount()
        {
            string from = "whatsapp: +491";
         
            var loanStep0 = await _loanRequestService.Process(new MessageModel()
                { Body = "Hey", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep0);


            var loanStep1 = await _loanRequestService.Process(new MessageModel()
                { Body = "250000", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep1);

            var loanStep2 = await _loanRequestService.Process(new MessageModel()
                { Body = "20", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep2);

            Assert.GreaterOrEqual(loanStep2.Rate, 1);

        }

        [Test()]
        public async Task Process_ChaosInput_Returnmount()
        {
            string from = "whatsapp: +492";

            var loanStep0 = await _loanRequestService.Process(new MessageModel()
                { Body = "Hey", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep0);


            var loanStep1 = await _loanRequestService.Process(new MessageModel()
                { Body = "It is 250000€", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep1);

            var loanStep2 = await _loanRequestService.Process(new MessageModel()
                { Body = " 20 Years", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep2);

            Assert.GreaterOrEqual(loanStep2.Rate, 1);

        }

        [Test()]
        public async Task Process_StartOverInput_Returnmount()
        {
            string from = "whatsapp: +493";

            var loanStep0 = await _loanRequestService.Process(new MessageModel()
                { Body = "Hey", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep0);


            var loanStep1 = await _loanRequestService.Process(new MessageModel()
                { Body = "It is 250000€", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep1);

            //Use stop word to restart process
            var loanStepStartOver = await _loanRequestService.Process(new MessageModel()
                { Body = "Start over", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStepStartOver);


            loanStep1 = await _loanRequestService.Process(new MessageModel()
                { Body = "It is 250000€", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep1);

            var loanStep2 = await _loanRequestService.Process(new MessageModel()
                { Body = " 20 Years", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep2);

            Assert.GreaterOrEqual(loanStep2.Rate, 1);
        }


        [Test()]
        public async Task Process_TwoRequests_Returnmount()
        {
            string from = "whatsapp: +491";

            var loanStep0 = await _loanRequestService.Process(new MessageModel()
                { Body = "Hey", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep0);


            var loanStep1 = await _loanRequestService.Process(new MessageModel()
                { Body = "250000", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep1);

            var loanStep2 = await _loanRequestService.Process(new MessageModel()
                { Body = "20", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep2);

            Assert.GreaterOrEqual(loanStep2.Rate, 1);

             loanStep0 = await _loanRequestService.Process(new MessageModel()
                { Body = "Hey", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep0);


             loanStep1 = await _loanRequestService.Process(new MessageModel()
                { Body = "250000", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep1);

             loanStep2 = await _loanRequestService.Process(new MessageModel()
                { Body = "20", From = from, CreatedDate = DateTime.Now, To = "+1123654789" });

            Assert.IsNotNull(loanStep2);

            Assert.GreaterOrEqual(loanStep2.Rate, 1);

        }

    }
}