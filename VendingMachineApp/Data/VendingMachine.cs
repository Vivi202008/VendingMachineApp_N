using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineApp.Model;

namespace VendingMachineApp.Data
{
    public class VendingMachine : Ivending
    {
        readonly int[] moneyType = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };

        static int moneyPool = 0;

        public int idFromUser, numberOfProduct;

        static Product selectedProduct;

        Product[] productInVending = AllProduct();

        public int[] MoneyType { get { return moneyType; } }

        public int MoneyPool { get { return moneyPool; } set { moneyPool = value; } }


        //o InsertMoney, add money to the pool.
        public int GetNumberFromUser(string forWhat)
        {
            bool inputRight = false;
            int number = 0;
            string userInput;

            do
            {
                try
                {
                    Console.Write(forWhat + "Enter  number: ");
                    userInput = Console.ReadLine();
                    number = int.Parse(userInput);
                    if (number > 0)
                    {
                        inputRight = true;
                    }
                    else
                    {
                        Console.WriteLine("Fel! Input a number greater than 0.");
                    }
                }
                catch
                {
                    Console.Write("Fel. Enter a right number.");
                    inputRight = false;
                }
            } while (!inputRight);

            return number;
        }

        public int InsertMoney(int moneyTypeInput, int moneyCounts)
        {
            moneyPool += moneyTypeInput * moneyCounts;
            ShowMoneyLeft();
            return moneyPool;
        }

        public int GetMoneyType()
        {
            int moneyTypeInput;
            bool inputRight;
            do
            {
                moneyTypeInput = GetNumberFromUser("Input in fixed denominations:1kr, 5kr, 10kr, 20kr, 50kr, 100kr, 500kr and 1000kr. ");
                inputRight = MoneyTypeInputIsCorrect(moneyTypeInput);


            } while (!inputRight);

            return moneyTypeInput;
        }

        public bool MoneyTypeInputIsCorrect(int moneyTypeInput)
        {
            bool inputRight;
            if (Array.Find(moneyType, money => money == moneyTypeInput) == 0)
            {
                throw new ArgumentException("Fel! The money inserted mustbe of a valid denomination! ( 1, 5, 10, 20, 50, 100, 500, 1000 kr )\n");
            }
            else
            {
                inputRight = true;
            }
            return inputRight;
        }

        public int GetCountOfMoneyType()
        {
            int moneyCounts = GetNumberFromUser("Input count of the money");
            return moneyCounts;
        }

        //o Purchase, to buy any mumber of a product.
        public void Purchase()
        {
            //User has no money i moneyPool
            if (moneyPool == 0)
            {
                throw new ArgumentException("You have no money in pool. Please insert money first.");
                //return;
            }

            //Show info about all products in vengding machine.
            foreach (Product product in productInVending)
            {
                Console.WriteLine($"Id:{product.Id}\tName:{product.Name}\tPrice:{product.Price}\t  Amount:{product.Amount}");
            }

            //User input Id of product and amount that user will buy thid product.
            int userInput = GetNumberFromUser("Choose the Id of produkt.  ");
            idFromUser = GetIdFromUser(userInput);


            //No product left.
            if (selectedProduct.Amount == 0)
            {
                Console.WriteLine("The product is no more left.");
                return;
            }


            numberOfProduct = GetNumberFromUser("Input the count of the produkt.  ");


            //check if the moneypool is enough to buy product and if the product is enough

            if (selectedProduct.Amount < numberOfProduct && selectedProduct.Amount > 0)
            {
                Console.WriteLine("The product is only {0} left, so you buy {0}.", selectedProduct.Amount);
                numberOfProduct = selectedProduct.Amount;
            }

            if (selectedProduct.Price * numberOfProduct > moneyPool)
            {
                Console.WriteLine("Your money is not enough to buy {0} that you will", selectedProduct.Name);
                numberOfProduct = moneyPool / selectedProduct.Price;

            }

            moneyPool = moneyPool - selectedProduct.Price * numberOfProduct;
            selectedProduct.Amount = selectedProduct.Amount - numberOfProduct;

            Console.WriteLine("--------Result--------------");
            Console.WriteLine("You buy {0} {1} st.", selectedProduct.Name, numberOfProduct);
            Console.WriteLine("A total of {0} kr was spent on this shopping", selectedProduct.Price * numberOfProduct);
            ShowMoneyLeft();

            if (numberOfProduct > 0)
            {
                Console.WriteLine(selectedProduct.HowToUse());

                Console.WriteLine(selectedProduct.Info());
            }


        }

        public int GetIdFromUser(int userInput)
        {
            selectedProduct = Array.Find(productInVending, Product => Product.Id == userInput);
            while (selectedProduct == null)
            {
                if (selectedProduct == null)
                { throw new ArgumentException("Fel! The Id inputed must be of a valid produktId! \n"); }
                userInput = GetNumberFromUser("Choose the Id of produkt.  ");
                selectedProduct = Array.Find(productInVending, Product => Product.Id == userInput);


            }

            return userInput;
        }

        //o ShowAll, show all products.
        public void ShowAll()
        {
            foreach (Product product in productInVending)
            {
                Console.WriteLine(product.Info());
            }
        }

        //o Show user's money left.
        public void ShowMoneyLeft()
        {
            Console.WriteLine($"Youe money is still {moneyPool} kr left.");
        }


        //o EndTransaction, returns money left in appropriate amount of change(Dictionary).
        public string EndTransaction()
        {
            string outPrintChange="";
            ShowMoneyLeft();
            if (moneyPool != 0)
            {

            Dictionary<int, int> change = new Dictionary<int, int>();

            outPrintChange = "Please take your change, it is made up of ";
            int countMoney = 0;

            for (int i = moneyType.Length - 1; i >= 0; i--)
            {
                countMoney = moneyPool / moneyType[i];
                change.Add(moneyType[i], countMoney);
                moneyPool = moneyPool % moneyType[i];
                if (countMoney > 0)
                    outPrintChange = outPrintChange + countMoney + " st " + moneyType[i] + "-kr notes ";
            }

            Console.WriteLine(outPrintChange);
            }


            return outPrintChange;
        }



        static Product[] AllProduct()
        {
            Coke coke = new Coke(1, "Cocacola", 10, 10, 300, "In can");
            Product productCoke = new Coke(2, "Pepsi", 10, 9, 500, "In bottle");
            Chocolate chocolate = new Chocolate(4, "Lindt", 20, 23, 200, "Roll");
            Water water = new Water(3, "Cocacola", 15, 10, 1000, "Mineral");
            Toy toy = new Toy(5, "Boll", 1, 53, 3, "Plast");

            Product[] allProduct = new Product[5] { coke, productCoke, chocolate, water, toy };
            return allProduct;

        }

        //public Product AddProduct(string name, int price, int amount, int ageLimitOrWeightOrVolume, string matieralOrType) 
        //{
        //    int currentLength = productInVending.Length;// allProduct nuvarande längd

        //    int id =productInVending[currentLength-1].Id +1;
        //    Product addProduct = new Water (id,name,price,amount,ageLimitOrWeightOrVolume,matieralOrType); // önskad Product

        //    Array.Resize(ref productInVending, currentLength + 1); // Increase the size of Array when add new Product object
        //    productInVending[currentLength] = addProduct;
        //    return addProduct;
        //}

    }
}
