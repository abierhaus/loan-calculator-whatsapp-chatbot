using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using loan_calculator_whatsapp_chatbot.Contracts;

namespace loan_calculator_whatsapp_chatbot.Services
{
    public interface ILoanRequestService
    {
    
        Task<ResponseModel> Process(MessageModel message);
    }

    public class LoanRequestService : ILoanRequestService
    {
        /// <summary>
        ///     Path to the local database
        /// </summary>
        public const string DatabasePath = @"Filename=App_Data\LiteDb.db";

        /// <summary>
        ///     Table name for the messages
        /// </summary>
        public const string ResponseTable = "Response";


        public LoanRequestService(ILoanCalculatorService loanCalculatorService)
        {
            LoanCalculatorService = loanCalculatorService;
        }


        public ILoanCalculatorService LoanCalculatorService { get; set; }


        /// <summary>
        /// Process
        /// 1. If new request come in respond with welcome message and ask for cost of the property
        /// 2. Save input for cost of property and ask for term
        /// 3. Save term and start loan calculation
        /// 4. Return loan rate
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel> Process(MessageModel message)
        {
            var responseModel = new ResponseModel {Message = message};

            if (string.IsNullOrEmpty(message.From))
            {
                responseModel.Body = "No number provided";
                return responseModel;
            }

            //Get existing one
            var responsesForNumber = await GetByNumber(message.From);
            //Get last one
            var lastOpenResponse = responsesForNumber.OrderByDescending(x => x.Message.CreatedDate).FirstOrDefault();


            if (lastOpenResponse == null || lastOpenResponse.Status == ResponseModel.StatusEnum.Finished || message.Body.Contains("Start over"))
            {
                responseModel.Status = ResponseModel.StatusEnum.Step0;
                responseModel.Body =
                    "Hello and welcome to the real estate loan calculator. How much does the property cost that you like to finance?";
            }
            else
            {
                switch (lastOpenResponse.Status)
                {
                    case ResponseModel.StatusEnum.Step0:
                        responseModel.Loan = new LoanModel();
                        responseModel.Loan.PurchasePrice = await ParseInputAsDecimal(message.Body);
                        responseModel.Status = ResponseModel.StatusEnum.Step1;
                        responseModel.Body = "Please provide us with your desired terms in years";
                        break;

                    case ResponseModel.StatusEnum.Step1:
                        responseModel.Loan = lastOpenResponse.Loan;
                        responseModel.Loan.LoanTermYears = await ParseInputAsDecimal(message.Body);
                        responseModel.Rate = LoanCalculatorService.CalculatePayment(responseModel.Loan);
                        responseModel.Status = ResponseModel.StatusEnum.Finished;
                        responseModel.Body = $"Your monthly rate is {responseModel.Rate}";
                        break;
                }
            }


            //Insert result
            await InsertAsync(responseModel);

            return responseModel;
        }

        private async Task<decimal> ParseInputAsDecimal(string input)
        {
            var num = decimal.Parse(new string(input.Where(char.IsDigit).ToArray()));

            return num;
        }

        private async Task InsertAsync(ResponseModel response)
        {
            using var db = new LiteDatabase(DatabasePath);
            var col = db.GetCollection<ResponseModel>(ResponseTable);
            col.Insert(response);
        }

        private async Task<List<ResponseModel>> GetByNumber(string number)
        {
            using var db = new LiteDatabase(DatabasePath);
            var col = db.GetCollection<ResponseModel>(ResponseTable);
            col.EnsureIndex(x => x.Message.From);


            var results = col.Query()
                .Where(x => x.Message.From == number)
                .ToList();
        
            return results;
        }
    }
}