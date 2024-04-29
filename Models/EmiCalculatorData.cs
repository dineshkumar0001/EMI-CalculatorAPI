namespace EmiCalculator.Models
{
   public class EmiCalculatorData
     {
            public int Id { get; set; }
            public int UserId { get; set; }
            public decimal LoanAmount { get; set; }
            public decimal InterestRate { get; set; }
            public int LoanTermMonths { get; set; }
            public DateTime CreatedAt { get; set; }
     }
}

