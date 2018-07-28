using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        protected int _id;
        static int counter = 0;

        protected decimal _sum;
        protected int _percentage;

        protected int _days = 0;

        public decimal CurrentSum { get => _sum; }
        public int Percentage { get => _percentage; }
        public int Id { get => _id; }

        protected Account(decimal sum, int percentage)
        {
            this._sum = sum;
            this._percentage = percentage;
            this._id = ++counter;
        }

        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if ((handler != null) && (e != null)) { handler(this, e); }
        }

        protected virtual void OnOpened(AccountEventArgs e) => CallEvent(e, Opened);
        protected virtual void OnWithdrawed(AccountEventArgs e) => CallEvent(e, Withdrawed);
        protected virtual void OnAdded(AccountEventArgs e) => CallEvent(e, Added);
        protected virtual void OnClosed(AccountEventArgs e) => CallEvent(e, Closed);
        protected virtual void OnCalculated(AccountEventArgs e) => CallEvent(e, Calculated);

        public virtual void Put(decimal sum)
        {
            this._sum += sum;
            OnAdded(new AccountEventArgs($"На счет поступило {sum}", sum));
        }

        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (this._sum <= sum)
            {
                this._sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Сумма {sum} снята со счета {this._id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно денег на счете {this._id}", 0));
            }

            return result;
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый депозитный счет! Id счета {this._id}", this._sum));
        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Cчет {this._id} закрыт. Итогоавя сумма: {CurrentSum}", CurrentSum));
        }

        protected internal void IncrementDays() => this._days++; 

        protected internal virtual void Calculate()
        {
            decimal increment = this._sum * _percentage / 100;
            this._sum += increment;
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере: {increment}", increment));
        }
    }
}
