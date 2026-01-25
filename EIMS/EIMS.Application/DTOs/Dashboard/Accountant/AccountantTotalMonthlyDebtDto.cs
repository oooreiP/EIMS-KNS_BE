namespace EIMS.Application.DTOs.Dashboard.Accountant
{
    public class AccountantTotalMonthlyDebtDto
    {
        public int TotalDebtors { get; set; }
        public decimal TotalUnpaidAmount { get; set; }
        public decimal TotalOverdueAmount { get; set; }
        public int OverdueCustomerCount { get; set; }
        public decimal LastMonthTotalDebt { get; set; }
        public double DebtGrowthPercent { get; set; }
        public decimal AverageDebtPerCustomer { get; set; }
        public AccountantDebtByUrgencyDto DebtByUrgency { get; set; } = new();
    }

    public class AccountantDebtByUrgencyDto
    {
        public decimal Critical { get; set; }
        public decimal High { get; set; }
        public decimal Medium { get; set; }
    }
}
