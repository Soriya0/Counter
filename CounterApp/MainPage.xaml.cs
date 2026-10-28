using CounterApp.Models;
using CounterApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CounterApp
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Counter> counters = new();
        public MainPage()
        {
            InitializeComponent();

            AllCounters.ItemsSource = counters;

            Loaded += OnPageLoaded;
        }

        private async void OnPageLoaded(object sender, EventArgs e)
        {
            Loaded -= OnPageLoaded;

            List<Counter> savedCounters =
                await CounterStorage.LoadAsync();

            foreach(Counter item in savedCounters)
            {
                counters.Add(item);
            }
        }

        private async void OnCounterSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Counter selectedCounter)
            {
                AllCounters.SelectedItem = null;

                await Navigation.PushAsync(
                    new CounterPage(selectedCounter, counters));
            }
        }

        private async void OnClickedAddCounter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new NewCounterPage(counters));
        }
    }
}
