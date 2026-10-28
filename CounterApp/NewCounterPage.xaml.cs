using CounterApp.Models;
using CounterApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace CounterApp;

public partial class NewCounterPage : ContentPage
{
	private ObservableCollection<Counter> counters;
	public NewCounterPage(ObservableCollection<Counter> countersList)
	{
		InitializeComponent();

		counters = countersList;
	}

    private async void OnClickedSave(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert(
                "Error",
                "Enter counter name.",
                "OK");
            return;
        }

        if (!int.TryParse(InitialValueEntry.Text,
                          out int startValue))
        {
            await DisplayAlert(
                "Error",
                "Enter correct integer.",
                "OK");
            return;
        }

        Counter newCounter = new Counter
        {
            Name = name,
            Value = startValue
        };

        counters.Add(newCounter);

        await CounterStorage.SaveAsync(counters);

        await Navigation.PopAsync();
    }
}