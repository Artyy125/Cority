using Cority.Helper;
using Cority.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cority.Business
{
    public class Camping : ICamping
    {
        public OutputHandler ReadDataFromFile(string path)
        {
            OutputHandler result = new OutputHandler();
            result.inputData = new List<string>();
            try
            {
                if (File.Exists(path))
                {
                    using (var sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (!string.IsNullOrEmpty(line))
                            {

                                result.inputData.Add(line);
                            }

                        }
                    }
                }
                result.Message = inputIsValid(result.inputData);


            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public OutputHandler CalculateExpense(List<string> data)
        {
            OutputHandler result = new OutputHandler();
            result.inputData = new List<string>();
            try
            {
                result.Message = CampIsValid(data);
                double sumExpense = 0;
                int forCounter = 0;
                if (result.Message == AllEnums.Message.OK.ToDescriptionString())
                {
                    double averageExpenses = Math.Round((from x in data where x.IndexOf('.') != -1 select double.Parse(x)).Sum(v => Convert.ToDouble(v)) / int.Parse(data[0]),2);
                    foreach (var item in data)
                    {
                        forCounter += 1;
                        if (item.IndexOf('.') != -1)
                        {
                            sumExpense = sumExpense + double.Parse(item);
                        }
                        if (sumExpense > 0 && item.IndexOf('.') == -1 || forCounter == data.Count)
                        {
                            double participanteExpense = sumExpense - averageExpenses;
                            string finalResult = "";
                            if (participanteExpense > 0)
                            {
                                finalResult = string.Format("({0:C})", participanteExpense);

                            }
                            else
                            {
                                finalResult = string.Format("{0:C}", Math.Abs(participanteExpense));
                            }
                            result.inputData.Add(finalResult);
                            sumExpense = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return result;
        }
        private string inputIsValid(List<string> inputData)
        {
            try
            {
                if (inputData.LastOrDefault() != "0")
                {
                    return AllEnums.Message.NoZero.ToDescriptionString();
                }
                else if (inputData.Count < 4)
                {
                    return AllEnums.Message.LessThanFourRecord.ToDescriptionString();
                }
                else if (inputData.Where(r => r.IndexOf('.') != -1).FirstOrDefault() == null)
                {
                    return AllEnums.Message.OneExpenseIsRquired.ToDescriptionString();
                }
                else if (inputData.Count(r => r.IndexOf('.') == -1) < 2)
                {
                    return AllEnums.Message.OneExpenseNumberAndOneCampNumberIsRequired.ToDescriptionString();
                }
                else
                {
                    return AllEnums.Message.OK.ToDescriptionString(); ;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string CampIsValid(List<string> data)
        {
            if (data.Count == 0)
            {
                return AllEnums.Message.EmptyData.ToDescriptionString();
            }
            int sum = (from x in data where x.IndexOf('.') == -1 select int.Parse(x)).Sum();
            sum -= int.Parse(data[0]);
            int expenseCount = data.Count(r => r.IndexOf('.') != -1);
            try
            {
                if (int.Parse(data[0]) != data.Count(r => r.IndexOf('.') == -1) - 1)
                {
                    return AllEnums.Message.CampAndExpenseCountNotEqual.ToDescriptionString();
                }
                else if (sum != expenseCount)
                {
                    return AllEnums.Message.ExpenseAndExpenseNumberCountNotEqual.ToDescriptionString();
                }
                else
                {
                    return AllEnums.Message.OK.ToDescriptionString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
