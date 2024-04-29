using EmiCalculator.Models;

namespace EmiCalculator.Interfaces
{
    public interface IEmiCalculatorDataService
    {
        public List<EmiCalculatorData> GetEmiCalculatorDataDetails();
        public EmiCalculatorData GetEmiCalculatorDataDetails(int id);
        public EmiCalculatorData AddEmiCalculatorData(EmiCalculatorData emicalculatordata);
        public EmiCalculatorData UpdateEmiCalculatorData(EmiCalculatorData emicalculatordata);
        public bool DeleteEmiCalculatorData(int id);
    }
}
