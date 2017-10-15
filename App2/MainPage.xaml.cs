using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Cat cat;
        Man man;
        public MainPage()
        {
            this.InitializeComponent();
            StartNewGame();
        }

        private void upHealthLevel_Click(object sender, RoutedEventArgs e)
        {
            if (cat.Health < Cat.MAX_VALUE)
            {
                man.Pay(Man.PRICE_HEALTH);
                cat.upHealthValue();
                showHealthLevel.Value = cat.Health;
            }

        }
        private void upHungerLevel_Click(object sender, RoutedEventArgs e)
        {
            if (cat.Hunger < Cat.MAX_VALUE)
            {
                man.Pay(Man.PRICE_HUNGER);
                cat.upHungerValue();
                showHungerLevel.Value = cat.Hunger;
            }  
        }

        private void upBoredomLevel_Click(object sender, RoutedEventArgs e)
        {
            if (cat.Boredom < Cat.MAX_VALUE)
            {
                man.Pay(Man.PRICE_BOREDOM);
                cat.upBoredomValue();
                showBoredomLevel.Value = cat.Boredom;
            }
        }
        private void upTouletLevel_Click(object sender, RoutedEventArgs e)
        {
            if(cat.Toulet < Cat.MAX_VALUE)
            {
                man.Pay(Man.PRICE_TOULET);
                cat.upTouletValue();
                showTouletLevel.Value = cat.Toulet;
            }
        }
        private void startNewGame_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
            StartNewGame();
        }

        private void StartNewGame()
        {
            cat = new Cat();
            man = new Man();
            man.MoneyChanged += ChangeMoney;
            cat.HealthChanged += ChangeHealth;
            cat.HungerChanged += ChangeHunger;
            cat.BoredomChanged += ChangeBoredom;
            cat.TouletChanged += ChangeToulet;
            cat.ConditionChanged += ChangeCondition;
            upHealthLevel.IsEnabled = true;
            upHungerLevel.IsEnabled = true;
            upBoredomLevel.IsEnabled = true;
            upTouletLevel.IsEnabled = true;

            showHealthLevel.Maximum = Cat.MAX_VALUE;
            showHealthLevel.Minimum = Cat.MIN_VALUE;

            showHungerLevel.Maximum = Cat.MAX_VALUE;
            showHungerLevel.Minimum = Cat.MIN_VALUE;

            showBoredomLevel.Maximum = Cat.MAX_VALUE;
            showBoredomLevel.Minimum = Cat.MIN_VALUE;

            showTouletLevel.Maximum = Cat.MAX_VALUE;
            showTouletLevel.Minimum = Cat.MIN_VALUE;

            ChangeHealth(cat.Health);
            ChangeHunger(cat.Hunger);
            ChangeBoredom(cat.Boredom);
            ChangeToulet(cat.Toulet);
            ChangeMoney(man.Money);
            ChangeCondition(cat.Condition);

        }
        private void EndGame()
        {
            cat.Kill();
            upHealthLevel.IsEnabled = false;
            upHungerLevel.IsEnabled = false;
            upBoredomLevel.IsEnabled = false;
            upTouletLevel.IsEnabled = false;
            cat.HealthChanged -= ChangeHealth;
            cat.HungerChanged -= ChangeHunger;
            cat.BoredomChanged -= ChangeBoredom;
            cat.TouletChanged -= ChangeToulet;
            cat.ConditionChanged -= ChangeCondition;
            man.MoneyChanged -= ChangeMoney;
        }


        private async void ChangeHealth(int newHealth)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                printConditionMessageHealth.Text = "Очки здоровья: " + newHealth.ToString();
                showHealthLevel.Value = newHealth;
            });
        }
        private async void ChangeHunger(int newHunger)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                printConditionMessageHunger.Text = "Очки голода: " + newHunger.ToString();
                showHungerLevel.Value = newHunger;
            });
        }
        private async void ChangeToulet(int newToulet)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                printConditionMessageToulet.Text = "Очки туалета: " + newToulet.ToString();
                showTouletLevel.Value = newToulet;
            });
        }
        private async void ChangeBoredom(int newBoredom)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                printConditionMessageBoredom.Text = "Очки скуки: " + newBoredom.ToString();
                showBoredomLevel.Value = newBoredom;
            });
        }
        private async void ChangeMoney(int newMoney)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                printConditionMessageMoney.Text = "Количество денег: " + newMoney.ToString();
            });
        }
        

        private async void ChangeCondition(Cat.State newState)
        {
            if (newState == Cat.State.Dead)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    printStateMessage.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    printStateMessage.Text = "Состояние: Умер";
                    EndGame();
                });
               
            }
            if (newState == Cat.State.Bad)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    printStateMessage.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                    printStateMessage.Text = "Состояние: Плохо";
                });
            }
            if (newState == Cat.State.Normal)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    printStateMessage.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                    printStateMessage.Text = "Состояние: Нормальное";
                });
            }
            if (newState == Cat.State.Good)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    printStateMessage.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                    printStateMessage.Text = "Состояние: Хорошее";
                });
            }

        }


    }
}
