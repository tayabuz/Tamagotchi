using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {    
            settings = new Settings(true,Settings.DifficultyLevel.Easy);
            this.InitializeComponent();
            settings.DifficultyChanged += ChangeDifficulty;
            ChangeDifficulty(settings.Difficulty);
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        public Settings settings;
        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                frame.Navigate(typeof(MainPage), settings);
                e.Handled = true; // указываем, что событие обработано
            }
        }
        private void checkBox_showTooltip_Checked(object sender, RoutedEventArgs e)
        {
            settings.IsHintVisible = true;
        }
        private void checkBox_showTooltip_Unchecked(object sender, RoutedEventArgs e)
        {
            settings.IsHintVisible = false;
        }
        private void sliderDifficultyLevels_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sliderDifficultyLevels.Value == 0)
            {
                settings.Difficulty = Settings.DifficultyLevel.Easy;
            }
            if (sliderDifficultyLevels.Value == 1)
            {
                settings.Difficulty = Settings.DifficultyLevel.Normal;
            }
            if (sliderDifficultyLevels.Value == 2)
            {
                settings.Difficulty = Settings.DifficultyLevel.Hard;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Frame frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }
        private async void ChangeDifficulty(Settings.DifficultyLevel newDifficulty)
        {
            if (sliderDifficultyLevels.Value == 0)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    showDifficultyLevel.Text = "Уровень сложности: Легкий";
                });
            }
            if (sliderDifficultyLevels.Value == 1)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    showDifficultyLevel.Text = "Уровень сложности: Нормальный";
                });
            }
            if (sliderDifficultyLevels.Value == 2)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    showDifficultyLevel.Text = "Уровень сложности: Максимальный";
                });
            }

        }
    }
}
