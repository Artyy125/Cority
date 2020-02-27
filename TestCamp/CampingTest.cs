using Cority.Business;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestCamp
{
    public class CampingTest
    {
        private Mock<ICamping> campingMoq;
        private List<string> data;
        public CampingTest()
        {
            campingMoq = new Mock<ICamping>();
            data = new List<string>();
        }

        // I just added some sample tests. it's obvious that can add more test to cover all scenarios.

        [Fact]
        public void CheckEmptyData()
        {
            // Arrange
            // in case we need to use moq. here we don't need it
            campingMoq.Setup(r => r.CalculateExpense(data));

            //Act
            Camping camp = new Camping();
            var result = camp.CalculateExpense(data);

            //Assert
            Assert.NotEqual("OK", result.Message);
            Assert.Equal("Data should not be empy", result.Message);
        }

        [Fact]
        public void CheckCalculationResult()
        {
            // Arrange
            // in case we need to use moq. here we don't need it
            campingMoq.Setup(r => r.CalculateExpense(data));
            data = new List<string>
            {
                "5",
                "2",
                "10.10",
                "20.20",
                "1",
                "25.12",
                "4",
                "12.19",
                "23.14",
                "13.18",
                "7.04",
                "3",
                "21.00",
                "7.50",
                "6.10",
                "2",
                "12.00",
                "30.10",
                //"3",
                //"1",
                //"18.23",
                //"2",
                //"15.12",
                //"21.17",
                //"3",
                //"14.10",
                //"3.70",
                //"17.10",
                //"4",
                //"2",
                //"19.11",
                //"13.12",
                //"3",
                //"21.10",
                //"33.09",
                //"5.07",
                //"4",
                //"10.00",
                //"11.10",
                //"12.11",
                //"13.12",
                //"1",
                //"45.00",
                //"0"
            };

            //Act
            Camping camp = new Camping();
            var result = camp.CalculateExpense(data);

            //Assert
            Assert.Equal("OK", result.Message);
            Assert.Equal(5, result.inputData.Count);
            Assert.Equal("$7.23", result.inputData[0]);
        }

    [Fact]
        public void CamIsInvalid()
        {
            // Arrange
            // in case we need to use moq. here we don't need it
            campingMoq.Setup(r => r.CalculateExpense(data));

            // in the first record there is no camp number
            data = new List<string>
            {
                "5",
                "10.10",
                "20.20",
                "1",
                "25.12",
                "4",
                "12.19",
                "23.14",
                "13.18",
                "7.04",
                "3",
                "21.00",
                "7.50",
                "6.10",
                "2",
                "12.00",
                "30.10",
            };

            //Act
            Camping camp = new Camping();
            var result = camp.CalculateExpense(data);

            //Assert
            Assert.NotEqual("OK", result.Message);
        }
    }
}
