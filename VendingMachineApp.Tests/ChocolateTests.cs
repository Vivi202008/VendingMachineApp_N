using System;
using Xunit;
using VendingMachineApp.Data;
using VendingMachineApp.Model;
using Xunit.Abstractions;
using System.Linq;
using System.Collections.Generic;

namespace VendingMachineApp.Tests
{
    public class ChocolateTests
    {
        [Fact]
        public void ChocolateInfoCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            Product [] allproducts=vendingMachine.All;
            Product productBuy=Array.Find(allproducts, Product => Product.Id == 4);

            string expected = $"--------Chocolate-------\nId:4\nName:Lindt\nPrice:23\nAmount:20\nWeight:200 g\nType:Roll\n";

            //Act
            string result = productBuy.Info();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ChocolateUseeWorkCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            Product[] allproducts = vendingMachine.All;
            Product productBuy = Array.Find(allproducts, Product => Product.Id == 4);

            string expected = $"Lindt's usage is to eat directly after opening the package";

            //Act
            string result = productBuy.HowToUse();

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
