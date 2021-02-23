namespace loan_calculator_whatsapp_chatbot.Contracts
{
    public class LoanModel
    {

        public LoanModel()
        {
            //Set default interest rate here
            InterestRate = new decimal(1.05);

            //Down Payment
            DownPayment = 0;
        }

        /// <summary>
        /// The total purchase price of the item being paid for.
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// The total down payment towards the item being purchased.
        /// </summary>
        public decimal DownPayment { get; set; }

        /// <summary>
        /// Gets the total loan amount. This is the purchase price less
        /// any down payment.
        /// </summary>
        public decimal LoanAmount
        {
            get { return (PurchasePrice - DownPayment); }
        }

        /// <summary>
        /// The annual interest rate to be charged on the loan
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// The term of the loan in months. This is the number of months
        /// that payments will be made.
        /// </summary>
        public decimal LoanTermMonths { get; set; }

        /// <summary>
        /// The term of the loan in years. This is the number of years
        /// that payments will be made.
        /// </summary>
        public decimal LoanTermYears
        {
            get { return LoanTermMonths / 12; }
            set { LoanTermMonths = (value * 12); }
        }

    }
}