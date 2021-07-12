using System;
using Xunit;
using VendingMachineApp.Data;
using VendingMachineApp.Model;
using Xunit.Abstractions;
using System.Linq;
using System.Collections.Generic;

namespace VendingMachineApp.Tests
{
    public class VendingMachineTests

    {
        VendingMachine vendingMachine = new VendingMachine();

        [Fact]
        public void MoneyTypeAreCorrect()
        {
            //Arange
            int[] expectedMoneyType = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };
            int expectedLength = expectedMoneyType.Length;

            //Act
            int[] result = vendingMachine.MoneyType;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLength, result.Length);
            Assert.Equal(result, expectedMoneyType);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        public void MoneyTypeInputIsCorrect(int moneyTypeInput)
        {
            //Arrange
            bool result;

            //Act
            result = vendingMachine.MoneyTypeInputIsCorrect(moneyTypeInput);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3.0)]
        [InlineData(-4)]
        [InlineData(10000)]
        [InlineData(24)]
        [InlineData(-4.5)]
        public void MoneyTypeInputIsWrong(int moneyTypeInput)
        {
            //Arrange

            //Act
            ArgumentException result = Assert.Throws<ArgumentException>(() => vendingMachine.MoneyTypeInputIsCorrect(moneyTypeInput));

            //Assert
            Assert.Contains("Fel! The money inserted mustbe of a valid denomination! ( 1, 5, 10, 20, 50, 100, 500, 1000 kr )", result.Message);

        }

        [Fact]
        public void MoneyPoolIsRightAfterMoneyInsert()
        {
            //Arrange
            int moneyType = 50;
            int countOfMoneyType = 3;
            int expectMoneyPool = moneyType * countOfMoneyType;

            //Act
            int actualMoneyPool = vendingMachine.InsertMoney(moneyType, countOfMoneyType);

            //Assert
            Assert.Equal(expectMoneyPool, actualMoneyPool);

            //Arrange
            moneyType = 10;
            countOfMoneyType = 9;
            expectMoneyPool = moneyType * countOfMoneyType + expectMoneyPool;

            //Act
            actualMoneyPool = vendingMachine.InsertMoney(moneyType, countOfMoneyType);

            //Assert
            Assert.Equal(expectMoneyPool, actualMoneyPool);
        }

        [Fact]
        public void NoMoneyToBuy()
        {
            //Arrange
            vendingMachine.MoneyPool = 0;
            int idUserChoose =3;
            int numberUserInput = 4;

            //Act
            ArgumentException result = Assert.Throws<ArgumentException>(() => vendingMachine.Purchase(idUserChoose, numberUserInput));

            //Assert
            Assert.Contains("You have no money in pool. Please insert money first.", result.Message);
        }

        [Fact]
        public void InputInvalidProductId()
        {
            //Arrange
            vendingMachine.idFromUser = 88;

            //Act
            bool result =  vendingMachine.IdIputCorrect(vendingMachine.idFromUser);

            //Assert
            Assert.False ( result);
        }

        [Fact]
        public void Buy3Water()
        {
            //Arrange
            //Coke coke = new Coke(1, "Cocacola", 10, 10, 300, "In can");
            //Product productCoke = new Coke(2, "Pepsi", 10, 9, 500, "In bottle");
            //Chocolate chocolate = new Chocolate(4, "Cocacola", 20, 23, 200, "Roll");
            //Water water = new Water(3, "Cocacola", 15, 10, 1000, "Mineral");
            //Toy toy = new Toy(5, "Boll", 1, 53, 3, "Plast");

            //Product[] allProduct = new Product[5] { coke, productCoke, chocolate, water, toy };
            vendingMachine.MoneyPool = 100;
            int idUserChoose = 3;
            int numberUserInput =4;
            int expectMoneyPool = 100 -4 * 10;
            

            //Act            
            vendingMachine.Purchase(idUserChoose,numberUserInput );

            //Assert
          //  Assert.Equal("Cocacola", vendingMachine.selectedProduct.Name);
            Assert.Equal(expectMoneyPool, vendingMachine.MoneyPool);
        }

        [Fact]

        public void EndTransactionWorkCorrectly()
        {
            //Arrange;
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.MoneyPool = 725;
            Dictionary<int, int> expectedChange = new Dictionary<int, int> { { 500, 1 }, { 100, 2 }, { 20, 1 }, { 5, 1 } };
           
            //Act
            Dictionary<int, int> result = vendingMachine.EndTransaction(725);

            //Assert
            Assert.Equal(expectedChange, result);
        }


    }
}
