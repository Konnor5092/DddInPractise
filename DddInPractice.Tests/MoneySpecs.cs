using System;
using DddInPractice.Logic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DddInPractice.Tests
{
    [TestClass]
    public class MoneySpecs
    {
        [TestMethod]
        public void Sum_of_two_moneys_produces_correct_result() 
        {
            // Arrange
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            Money sum = money1 + money2;

            // Assert
            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [TestMethod]
        public void Two_money_instances_equal_if_contains_the_same_amount()
        {
            // Arrange
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            // Assert
            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [TestMethod]
        public void Two_money_instances_do_not_equal_if_contain_different_money_amount()
        {
            // Arrange
            Money dollar = new Money(0, 0, 0, 1, 0, 0);
            Money hundredCents = new Money(100, 0, 0, 0, 0, 0);

            // Assert
            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [TestMethod]
        [DataRow(-1, 0, 0, 0, 0, 0)]
        [DataRow(0, -2, 0, 0, 0, 0)]
        [DataRow(0, 0, -3, 0, 0, 0)]
        [DataRow(0, 0, 0, -4, 0, 0)]
        [DataRow(0, 0, 0, 0, -5, 0)]
        [DataRow(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value(
            int oneCentCount, 
            int tenCentCount, 
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount) 
        {
            // Arrange 
            Action action = () => new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            // Act + Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0, 0, 0, 0)]
        [DataRow(1, 0, 0, 0, 0, 0, 0.01)]
        [DataRow(1, 2, 0, 0, 0, 0, 0.21)]
        [DataRow(1, 2, 3, 0, 0, 0, 0.96)]
        [DataRow(1, 2, 3, 4, 0, 0, 4.96)]
        [DataRow(1, 2, 3, 4, 5, 0, 29.96)]
        [DataRow(1, 2, 3, 4, 5, 6, 149.96)]
        [DataRow(11, 0, 0, 0, 0, 0, 0.11)]
        [DataRow(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_is_calculated_correctly(
            int oneCentCount, 
            int tenCentCount, 
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            double expectedAmount) 
        {
            // Arrange
            Money money = new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            // Act
            decimal amount = Convert.ToDecimal(expectedAmount);

            // Assert
            money.Amount.Should().Be(amount);
        }

        [TestMethod]
        public void Subtraction_of_two_moneys_produces_correct_result() 
        {
            // Arrange
            Money money1 = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            Money result = money1 - money2;

            // Assert
            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [TestMethod]
        public void Cannot_subtract_more_than_exists() 
        {
            // Arrange
            Money money1 = new Money(0, 1, 0, 0, 0, 0);
            Money money2 = new Money(1, 0, 0, 0, 0, 0);

            // Act 
            Action action = () => 
            {
                Money result = money1 - money2;
            };

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}