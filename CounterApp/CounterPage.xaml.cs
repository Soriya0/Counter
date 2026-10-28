using CounterApp.Models;
using System.Collections.ObjectModel;
using CounterApp.Services;
using System.Threading.Tasks;

namespace CounterApp;

public partial class CounterPage : ContentPage
{
	private Counter counter;
	private readonly ObservableCollection<Counter> counters;

	public CounterPage(Counter selectedCounter, ObservableCollection<Counter> countersList)
	{
		InitializeComponent();

		counter = selectedCounter;
		counters = countersList;

		NameLabel.Text = counter.Name;
		ValueLabel.Text = counter.Value.ToString();
	}

    private async void OnClickedMinus(object sender, EventArgs e)
    {
		counter.Value--;
		ValueLabel.Text = counter.Value.ToString();

		await CounterStorage.SaveAsync(counters);
    }

    private async void OnClickedPlus(object sender, EventArgs e)
    {
		counter.Value++;
		ValueLabel.Text = counter.Value.ToString();

		await CounterStorage.SaveAsync(counters);
    }
}