using System;
using DddInPractice.Logic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DddInPractice.Tests
{
    [TestClass]
    public class SnackMachineSpecs
    {
        [TestMethod]
        public void New_snack_machine_contains_no_money() 
        {
            // Arrange
            var snackMachine = new SnackMachine();

            // Assert
            snackMachine.MoneyInside.Should().Be(Money.None);
            snackMachine.MoneyInTransaction.Should().Be(Money.None);
        }

        [TestMethod]
        public void Return_money_empties_money_in_transaction()
        {
            // Arrange
            var snackMachine = new SnackMachine();
            
            // Act
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.ReturnMoney();

            // Assert
            snackMachine.MoneyInTransaction.Should().Be(Money.None);
        }

        [TestMethod]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            // Arrange
            var snackMachine = new SnackMachine();

            // Act
            snackMachine.InsertMoney(Money.Cent);
            snackMachine.InsertMoney(Money.Dollar);

            // Assert
            snackMachine.MoneyInTransaction.Should().Be(new Money(1, 0, 0, 1, 0, 0));
        }

        [TestMethod]
        public void Cannot_insert_more_than_one_coin_or_note_at_a_time()
        {
            // Arrange
            var snackMachine = new SnackMachine();
            
            // Act
            Money twoCent = Money.Cent + Money.Cent;
            Action action = () => snackMachine.InsertMoney(twoCent);

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Money_in_transaction_go_to_money_inside_after_purchase()
        {
            // Arrange
            var snackMachine = new SnackMachine();

            // Act
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.BuySnack();

            // Assert
            snackMachine.MoneyInTransaction.Should().Be(Money.None);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }
    }
}