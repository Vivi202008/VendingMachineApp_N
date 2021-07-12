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
        public void Purchase();

        //InsertMoney
        public int InsertMoney(int moneyType,int countOfMoneyType);

        //EndTranslation
        public string EndTransaction();
    }
}
