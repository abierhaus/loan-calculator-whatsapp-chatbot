using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using loan_calculator_whatsapp_chatbot.Contracts;

namespace loan_calculator_whatsapp_chatbot.Services
{
    public interface ILoanCalculatorService
    {
        /// <summary>
        /// Calculates the monthy payment amount based on current
        /// settings.
        /// </summary>
        /// <returns>Returns the monthly payment amount</returns>
        decimal CalculatePayment(LoanModel loan);
    }

    /// <summary>
    /// Service for calculation loans. Original taken from http://www.blackbeltcoder.com/Articles/algorithms/c-payment-calculator
    /// </summary>
    public class LoanCalculatorService : ILoanCalculatorService
    {
       
        /// <summary>
        /// Calculates the monthy payment amount based on current
        /// settings.
        /// </summary>
        /// <returns>Returns the monthly payment amount</returns>
        public decimal CalculatePayment(LoanModel loan)
        {
            decimal payment = 0;

            if (loan.LoanTermMonths > 0)
            {
                if (loan.InterestRate != 0)
                {
                    var rate = ((loan.InterestRate / 12) / 100);
                    var factor = (rate + (rate / (decimal) (Math.Pow((double) (rate + 1), (double) loan.LoanTermMonths) - 1)));
                    payment = (loan.LoanAmount *  factor);
                }
                else payment = (loan.LoanAmount / (decimal)loan.LoanTermMonths);
            }
            return Math.Round(payment, 2);
        }
    }
}

