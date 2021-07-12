using System;
using Xunit;
using VendingMachineApp.Data;
using VendingMachineApp.Model;
using Xunit.Abstractions;
using System.Linq;
using System.Collections.Generic;

namespace VendingMachineApp.Tests
{
    public class CokeTests
    {
        [Fact]
        public void ChocolateInfoCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            Product[] productInVending =vendingMachine.AllProdukt();
            vendingMachine.MoneyPool=100;
            Product product =  Array.Find(productInVending, Product => Product.Id == 4); ;

            string expected = $"1\tChocolate\t\t5kr";

            //Act
            string result = product.Info();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ChocolateUseeWorkCorrectly()
        {
            //Arrange;
            VendingMachine vendingMashine = new VendingMachine();
            vendingMashine.InsertMoney(500);
            Product product = vendingMashine.Purchase(1);

            string expected = "It is a snack. Can be eated! But don't eat too many.. They are not good for your teeth. ";

            //Act
            string result = product.Use();

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
