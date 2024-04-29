using EmiCalculator.Context;
using EmiCalculator.Interfaces;
using EmiCalculator.Models;

namespace EmiCalculator.Services
{
    public class EmiCalculatorDataService : IEmiCalculatorDataService

    {
        private readonly JwtContext _jwtContext;
        public EmiCalculatorDataService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }
        public EmiCalculatorData AddEmiCalculatorData(EmiCalculatorData emicalculatordata)
        {
            var emp = _jwtContext.EmiCalculatorData.Add(emicalculatordata);
            _jwtContext.SaveChanges();
            return emp.Entity;
        }

        public bool DeleteEmiCalculatorData(int id)
        {
            try
            {
                var emp = _jwtContext.EmiCalculatorData.SingleOrDefault(s => s.Id == id);
                if (emp == null)
                    throw new Exception("user not found");
                else
                {
                    _jwtContext.EmiCalculatorData.Remove(emp);
                    _jwtContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            { 
                return false;
            }
           
        }

        public List<EmiCalculatorData> GetEmiCalculatorDataDetails()
        {
            var emicalculatordatas = _jwtContext.EmiCalculatorData.ToList();
            return emicalculatordatas;
        }

        public EmiCalculatorData GetEmiCalculatorDataDetails(int id)
        {
            var emp = _jwtContext.EmiCalculatorData.SingleOrDefault(s => s.Id == id);
            return emp;
        }

        public EmiCalculatorData UpdateEmiCalculatorData(EmiCalculatorData emicalculatordata)
        {
           var updated =  _jwtContext.EmiCalculatorData.Update(emicalculatordata);
            _jwtContext.SaveChanges();
            return updated.Entity;
        }
    }
}
