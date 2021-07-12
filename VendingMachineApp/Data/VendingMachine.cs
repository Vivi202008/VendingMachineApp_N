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
        //Product[] productInVending;

        public Product[] All { get { return productInVending ; } }

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
        public void Purchase(int idFromUser,int numberOfProduct)
        {
            //User has no money i moneyPool
            if (moneyPool == 0)
            {
                throw new ArgumentException("You have no money in pool. Please insert money first.");
                //return;
            }

            selectedProduct = Array.Find(productInVending, Product => Product.Id == idFromUser );

            //No product left.
            if (selectedProduct.Amount == 0)
            {
                Console.WriteLine("The product is no more left.");
                return;
            }

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

            //Caclulate money och amountofproduct left
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
//User input Id of product and amount that user will buy thid product.
    public int NumberOfProduct()
        {
 int numberOfProduct = GetNumberFromUser("Input the count of the produkt.  ");
            return numberOfProduct;
        }
       
        public int IdFromUser()
        {  
            int idFromUser = GetNumberFromUser("Choose the Id of produkt.  ");
            bool idInputCorrect = IdIputCorrect(idFromUser);
            while(!idInputCorrect)
            {
                idFromUser = GetNumberFromUser("Choose the Id of produkt.  ");
                idInputCorrect = IdIputCorrect(idFromUser);
            }

            return idFromUser;
        }



        public bool IdIputCorrect(int userInput)
        {
            bool idInputCorrect=false;
            selectedProduct = Array.Find(productInVending, Product => Product.Id == userInput);
            if (selectedProduct == null)
            {
                Console.WriteLine("Fel! The Id inputed must be of a valid produktId! \n");
            }
            else
            {
                idInputCorrect = true;
            }
            return idInputCorrect;
        }

        //o ShowAll, show all products.
        public void ShowAll()
        {
            foreach (Product product in productInVending)
            {
                Console.WriteLine(product.Info());
            }
            //Show info about all products in vengding machine.
            foreach (Product product in productInVending)
            {
                Console.WriteLine($"Id:{product.Id}\tName:{product.Name}\tPrice:{product.Price}\t  Amount:{product.Amount}");
            }
        }

        //o Show user's money left.
        public void ShowMoneyLeft()
        {
            Console.WriteLine($"Youe money is still {moneyPool} kr left.");
        }


        //o EndTransaction, returns money left in appropriate amount of change(Dictionary).
        public Dictionary<int, int> EndTransaction(int moneyLeft)
        {
            string outPrintChange;
            Dictionary<int, int> change = new Dictionary<int, int>();
            if (moneyLeft > 0)
            {
                outPrintChange = "Please take your change, it is made up of ";
                int countMoney;

                for (int i = moneyType.Length - 1; i >= 0; i--)
                {
                    countMoney = moneyLeft / moneyType[i];

                    moneyLeft = moneyLeft % moneyType[i];
                    if (countMoney > 0)
                    {
                        change.Add(moneyType[i], countMoney);
                        outPrintChange = outPrintChange + countMoney + " st " + moneyType[i] + "-kr notes ";
                    }
                }
                Console.WriteLine(outPrintChange);
            }
            return change;
        }



        public static Product[]  AllProduct()
        {
            Coke coke = new Coke(1, "Cocacola", 10, 10, 300, "In can");
            Product productCoke = new Coke(2, "Pepsi", 10, 9, 500, "In bottle");
            Water water = new Water(3, "Water", 15, 10, 1000, "Mineral");
            Chocolate chocolate = new Chocolate(4, "Lindt", 20, 23, 200, "Roll");
            Toy toy = new Toy(5, "Ball", 1, 53, 3, "Plast");

            Product[] productsInVending = new Product[5] { coke, productCoke, chocolate, water, toy };
            return productsInVending;

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
