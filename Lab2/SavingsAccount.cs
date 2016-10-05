using System;

namespace eu.sig.training.ch04.v1
{
    public class SavingsAccount
    {
        public CheckingAccount RegisteredCounterAccount { get; set; }

        public Transfer makeTransfer(string counterAccount, Money    amount)
        {
            Validate(counterAccount);
            int sum = CalcSum(counterAccount);
            return LastValidate(counterAccount, amount, sum);
        }

        private Transfer LastValidate(string counterAccount, Money amount, int sum)
        {
            if (sum % 11 == 0)
            {
                // 2. Look up counter account and make transfer object:
                CheckingAccount acct = Accounts.FindAcctByNumber(counterAccount);
                Transfer result = new Transfer(this, acct, amount); // <2>
                                                                    // 3. Check whether withdrawal is to registered counter account:
                ValidateThis(result);
                return result;
            }
            else
            {
                throw new BusinessException("Invalid account number!!");
            }
        }

        private void ValidateThis(Transfer result)
        {
            if (result.CounterAccount.Equals(this.RegisteredCounterAccount))
            {
                return;
            }
            else
            {
                throw new BusinessException("Counter-account not registered!");
            }
        }

        private static void Validate(string counterAccount)
        {
            // 1. Assuming result is 9-digit bank account number, validate 11-test:
            if (String.IsNullOrEmpty(counterAccount) || counterAccount.Length != 9)
            {
                throw new BusinessException("Invalid account number!");
            }
        }

        private static int CalcSum(string counterAccount)
        {
            var sum = 0;
            for (int i = 0; i < counterAccount.Length; i++)
            {
                sum = sum + (9 - i) * (int)Char.GetNumericValue(counterAccount[i]);
            }

            return sum;
        }
    }
}
