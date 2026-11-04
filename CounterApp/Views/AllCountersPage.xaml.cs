namespace CounterApp.Views;

public partial class AllCountersPage : ContentPage
{
	public AllCountersPage()
	{
		InitializeComponent();

		BindingContext = new Models.AllCounters();
	}

    protected override void OnAppearing()
    {
		((Models.AllCounters)BindingContext).LoadCounters();
    }

	private async void Add_Clicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new CounterPage());
    }

    private async void countersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if (e.CurrentSelection.Count != 0)
		{
			var counter = (Models.Counter)e.CurrentSelection[0];

			countersCollection.SelectedItem = null;

			await Navigation.PushAsync(
				new CounterPage(counter.Filename));
		}
    }
}