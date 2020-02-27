using Cority.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cority.Business
{
    public interface ICamping
    {
        OutputHandler ReadDataFromFile(string path);
        OutputHandler CalculateExpense(List<string> data);
    }
}
