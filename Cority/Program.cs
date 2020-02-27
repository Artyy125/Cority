using Cority.Business;
using Cority.Helper;
using Cority.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cority
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ICamping, Camping>()
                .BuildServiceProvider();

            string inputFilePath = @"c:\cority\campExpense.txt";
            string outputFilePath = @"c:\cority\campExpense.txt.out";
            serviceProvider
            .GetService<ILoggerFactory>();

            var camping = serviceProvider.GetService<ICamping>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
            try
            {
                

                OutputHandler result = camping.ReadDataFromFile(inputFilePath);
                List<string> camp = new List<string>();
                List<string> output = new List<string>();
                if (result.Message == AllEnums.Message.OK.ToDescriptionString())
                {
                    
                    for (int i = 0; i <= result.inputData.Count - 1; i++)
                    {
                        if (i == 0 || i == 1 ||
                            (result.inputData[i].IndexOf('.') != -1 || (i < result.inputData.Count - 1 && result.inputData[i + 1].IndexOf('.') != -1)))
                        {
                            camp.Add(result.inputData[i]);
                        }
                        else if ((result.inputData[i].IndexOf('.') == -1 && (i < result.inputData.Count - 1 && result.inputData[i + 1].IndexOf('.') == -1)) || i == result.inputData.Count -1)
                        {
                            OutputHandler participantExpense = camping.CalculateExpense(camp);
                            if (participantExpense.Message == AllEnums.Message.OK.ToDescriptionString())
                            {
                                output.AddRange(participantExpense.inputData);
                                output.Add("");
                            }
                            camp.Clear();
                            camp.Add(result.inputData[i]);
                        }
                        else
                        {
                            camp.Add(result.inputData[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid data");
                }
                if (output.Count > 0)
                {
                    using (StreamWriter outputFile = new StreamWriter(outputFilePath))
                    {
                        foreach (string line in output)
                            outputFile.WriteLine(line);
                    }
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            
        }
    }
}
