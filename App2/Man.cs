using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App2
{

    public class Man
    {
        public const int START_AMOUNT_MONEY = 10000;
        public const int MIN_MONEY_VALUE = 0;
        private Timer timerMan;
        const int TIMER_INTERVAL_MAN = 100000;
        private int money;
        public const int MONEY_NEW_PERIOD = 7500;

        public const int PRICE_HEALTH = 75;
        public const int PRICE_HUNGER = 50;
        public const int PRICE_BOREDOM = 15;
        public const int PRICE_TOULET = 20;

        public delegate void PropertyManChangeHandler(int newValue);
        public event PropertyManChangeHandler MoneyChanged;


        public Man()
        {
            Money = START_AMOUNT_MONEY;
            TimerCallback tm = new TimerCallback(DeteriorationOverTimeForMan);
            timerMan = new Timer(tm, null, TIMER_INTERVAL_MAN, TIMER_INTERVAL_MAN);
        }
        public void DeteriorationOverTimeForMan(object obj)
        {
            Money += MONEY_NEW_PERIOD;
        }
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                if (money < MIN_MONEY_VALUE) { money = MIN_MONEY_VALUE; }
                if (MoneyChanged != null)
                {
                    MoneyChanged(money);
                }
            }
        }
        public void Pay(int Price)
        {
            Money -= Price;
        }
    }
}

