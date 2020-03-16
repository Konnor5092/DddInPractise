using System;
using System.Linq;

namespace DddInPractice.Logic
{
    public class SnackMachine : Entity
    {
        public Money MoneyInside{ get; private set; } = Money.None;
        public Money MoneyInTransaction { get; private set; } = Money.None;

        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = Money.None;
        }

        public void InsertMoney(Money money) 
        {
            Money []allowedValues = {
                Money.Cent, Money.TenCent, Money.Quarter,
                Money.Dollar, Money.FiveDollar, Money.TwentyDollar
            };
            
            if (!allowedValues.Contains(money)) 
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public void ReturnMoney() 
        {
            MoneyInTransaction = Money.None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = Money.None;
        }
    }
}