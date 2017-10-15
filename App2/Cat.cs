using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App2
{
    public class Cat
    {
        public const int MAX_VALUE = 100;
        public const int MIN_VALUE = 0;
        private int health;
        private int toulet;
        private int boredom;
        private int hunger;
        const int GOOD_CONDITION = 70;
        const int BAD_CONDITION = 30;
        const int UP_NUMBER = 1;
        const int DOWN_NUMBER = 1;
        private Timer timer;
        const int TIMER_INTERVAL = 3000;

        public delegate void PropertyChangeHandler(int newValue);
        public event PropertyChangeHandler HealthChanged;
        public event PropertyChangeHandler HungerChanged;
        public event PropertyChangeHandler BoredomChanged;
        public event PropertyChangeHandler TouletChanged;

        public delegate void StateChangeHandler(State newState);
        public event StateChangeHandler ConditionChanged;

        public Cat()
        {
            Health = MAX_VALUE;
            Toulet = MAX_VALUE;
            Boredom = MAX_VALUE;
            Hunger = MAX_VALUE;
            TimerCallback tm = new TimerCallback(DeteriorationOverTime);
            timer = new Timer(tm, null, 2000, TIMER_INTERVAL);


        }
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
                if (health < MIN_VALUE) { health = MIN_VALUE; }
                if (health > MAX_VALUE) { health = MAX_VALUE; }
                if (HealthChanged != null)
                {
                    HealthChanged(health);
                }
                if(Condition == State.Dead){ if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Bad) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Normal) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Good) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
            }
        }
        public int Toulet
        {
            get
            {
                return toulet;
            }
            set
            {
                toulet = value;
                if (toulet < MIN_VALUE) { toulet = MIN_VALUE; }
                if (toulet > MAX_VALUE) { toulet = MAX_VALUE; }
                if (TouletChanged != null)
                {
                    TouletChanged(toulet);
                }
                if (Condition == State.Dead) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Bad) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Normal) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Good) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
            }
        }
        public int Boredom
        {
            get
            {
                return boredom;
            }
            set
            {
                boredom = value;
                if (boredom < MIN_VALUE) { boredom = MIN_VALUE; }
                if (boredom > MAX_VALUE) { boredom = MAX_VALUE; }
                if (BoredomChanged != null)
                {
                    BoredomChanged(boredom);
                }
                if (Condition == State.Dead) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Bad) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Normal) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Good) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
            }
        }
        public int Hunger
        {
            get
            {
                return hunger;
            }
            set
            {
                hunger = value;
                if (hunger < MIN_VALUE) { hunger = MIN_VALUE; }
                if (hunger > MAX_VALUE) { hunger = MAX_VALUE; }
                if (HungerChanged != null)
                {
                    HungerChanged(hunger);
                }
                if (Condition == State.Dead) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Bad) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Normal) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
                if (Condition == State.Good) { if (ConditionChanged != null) { ConditionChanged(Condition); } }
            }
        }
        public enum State { Good, Normal, Bad, Dead }
        public State Condition
        {
            get
            {

                if ((Health >= GOOD_CONDITION) && (Toulet >= GOOD_CONDITION) && (Boredom >= GOOD_CONDITION) && (Hunger >= GOOD_CONDITION))
                {
                    return State.Good;
                }
                else
                {
                    var state = State.Normal;
                    int countForDeath = 0;
                    if ((Health <= BAD_CONDITION) && (Health > MIN_VALUE)) { countForDeath++; }
                    if ((Toulet <= BAD_CONDITION) && (Toulet > MIN_VALUE)) { countForDeath++; }
                    if ((Boredom <= BAD_CONDITION) && (Boredom > MIN_VALUE)) { countForDeath++; }
                    if ((Hunger <= BAD_CONDITION) && (Hunger > MIN_VALUE)) { countForDeath++; }

                    if (countForDeath >= 1) { state = State.Bad; }
                    if (countForDeath >= 3) { state= State.Dead; }

                    if ((Health == MIN_VALUE) || (Toulet == MIN_VALUE) || (Boredom == MIN_VALUE) || (Hunger == MIN_VALUE)) { state= State.Dead; }

                    return state;
                }
            }

        }
        public void upHealthValue()
        {
            Health = Health + UP_NUMBER;
        }
        public void upTouletValue()
        {
            Toulet = Toulet + UP_NUMBER*2;
        }
        public void upBoredomValue()
        {
            Boredom = Boredom + UP_NUMBER*5;
        }
        public void upHungerValue()
        {
            Hunger = Hunger + UP_NUMBER*3;
        }
        public void DeteriorationOverTime(object obj)
        {
            Health = Health - DOWN_NUMBER;
            Toulet = Toulet - DOWN_NUMBER*2;
            Hunger = Hunger - DOWN_NUMBER*3;
            Boredom = Boredom - DOWN_NUMBER*5;
        }
        public bool isCatAlive(State Condition)
        {
            if (Condition == State.Dead) { return false; }
            else { return true; }
        }

        public void Kill()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

    }
    
}
