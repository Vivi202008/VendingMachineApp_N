using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineApp.Model
{
    interface Ivending
    {
        //ShowAll
        public void ShowAll();

        //Purchase
        public void Purchase(int idFromUser, int numberOfProduct);

        //InsertMoney
        public int InsertMoney(int moneyType,int countOfMoneyType);

        //EndTranslation
        public Dictionary<int, int> EndTransaction(int moneyLeft);
    }
}
