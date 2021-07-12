using System;
using Xunit;
using VendingMachineApp.Data;
using VendingMachineApp.Model;
using Xunit.Abstractions;
using System.Linq;
using System.Collections.Generic;

namespace VendingMachineApp.Tests
{
    public class WaterTests
    {
        [Fact]
        public void WaterInfoCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            Product [] allproducts=vendingMachine.All;
            Product productBuy=Array.Find(allproducts, Product => Product.Id == 3);

            string expected = $"--------Water-------\nId:3\nName:Water\nPrice:10\nAmount:15\nCapacity:1000 ml\nType:Mineral\n";

            //Act
            string result = productBuy.Info();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WaterUseCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            Product[] allproducts = vendingMachine.All;
            Product productBuy = Array.Find(allproducts, Product => Product.Id == 3);

            string expected = $"Water's usage is to open the cap and drink directly.";

            //Act
            string result = productBuy.HowToUse();

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
