using System;
using Xunit;
using VendingMachineApp.Data;
using VendingMachineApp.Model;
using Xunit.Abstractions;
using System.Linq;
using System.Collections.Generic;

namespace VendingMachineApp.Tests
{
    public class ToyTests
    {
        [Fact]
        public void ToyInfoCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            Product [] allproducts=vendingMachine.All;
            Product productBuy=Array.Find(allproducts, Product => Product.Id == 5);

            string expected = $"--------Toy-------\nId:5\nName:Ball\nPrice:53\nAmount:1\nMatieral:Plast\nAgeLimit:over 3 years old\n";

            //Act
            string result = productBuy.Info();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToyWorkCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            Product[] allproducts = vendingMachine.All;
            Product productBuy = Array.Find(allproducts, Product => Product.Id == 5);

            string expected = $"Ball's usage is to give chilrden older than 3 years old after opening the package";
        

            //Act
            string result = productBuy.HowToUse();

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
