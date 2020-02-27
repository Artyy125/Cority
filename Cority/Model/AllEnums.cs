using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cority.Model
{
    public class AllEnums
    {
        public enum Message
        {
            [Description("OK")]
            OK,
            [Description("Last input should be 0")]
            NoZero,
            [Description("at least 1 camp 1 expense count and 1 expense should be in the file.")]
            LessThanFourRecord,
            [Description("at least 1 camp and 1 expense count should be in the file")]
            OneExpenseNumberAndOneCampNumberIsRequired,
            [Description("at least 1 expense should be in the file")]
            OneExpenseIsRquired,
            [Description("number of camps is not equal with expenses records count")]
            CampAndExpenseCountNotEqual,
            [Description("number of expenses is not equal with sum of expense numbers in a camp")]
            ExpenseAndExpenseNumberCountNotEqual,
            [Description("Data should not be empy")]
            EmptyData

        }
    }
}
