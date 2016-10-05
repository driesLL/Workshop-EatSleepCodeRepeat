using System;
namespace eu.sig.training.ch04.v1
{
    public class CheckingAccount
    {
        private int transferLimit = 100;

        public Transfer MakeTransfer(String counterAccount, Money amount)
        {
            CheckGreaterThanLimit(amount);
            ValidateLength(counterAccount);
            int sum = CalcSum(counterAccount);
            return LastCheck(counterAccount, amount, sum);
        }

        private Transfer LastCheck(string counterAccount, Money amount, int sum)
        {
            if (sum % 11 == 0)
            {
                // 3. Look up counter account and make transfer object:
                CheckingAccount acct = Accounts.FindAcctByNumber(counterAccount);
                Transfer result = new Transfer(this, acct, amount);
                return result;
            }
            else
            {
                throw new BusinessException("Invalid account number!");
            }
        }

        private static int CalcSum(string counterAccount)
        {
            int sum = 0;
            for (int i = 0; i < counterAccount.Length; i++)
            {
                sum = sum + (9 - i) * (int)Char.GetNumericValue(counterAccount[i]);
            }

            return sum;
        }

        private static void ValidateLength(string counterAccount)
        {
            // 2. Assuming result is 9-digit bank account number, validate 11-test:
            if (String.IsNullOrEmpty(counterAccount) || counterAccount.Length != 9)
            {
                throw new BusinessException("Invalid account number!");
            }
        }

        private void CheckGreaterThanLimit(Money amount)
        {
            // 1. Check withdrawal limit:
            if (amount.GreaterThan(this.transferLimit))
            {
                throw new BusinessException("Limit exceeded!");
            }
        }
    }
}