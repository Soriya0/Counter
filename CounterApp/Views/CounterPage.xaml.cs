namespace CounterApp.Views;

public partial class CounterPage : ContentPage
{
	private Models.Counter counter;

	public CounterPage()
	{
		InitializeComponent();

		counter = new Models.Counter();

        string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName = $"{Path.GetRandomFileName()}.counter.txt";

        LoadCounter(Path.Combine(appDataPath, randomFileName));
    }

    public CounterPage(string fileName) : this()
    {
        LoadCounter(fileName);
    }

    private void LoadCounter(string fileName)
	{
		counter = new Models.Counter();

		counter.Filename = fileName;

		if (File.Exists(fileName))
		{
			string[] lines = File.ReadAllLines(fileName);

			counter.Filename = fileName;
			counter.Name = lines.FirstOrDefault() ?? "";

			counter.Value =
				int.TryParse(lines.Skip(1).FirstOrDefault(),
				out int value) ? value : 0;

			NameLabel.Text = counter.Name;
			ValueLabel.Text = counter.Value.ToString();

			NewCounterFields.IsVisible = false;
			ExistingCounterFields.IsVisible = true;
		}

        BindingContext = counter;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
		string name = NameEntry.Text?.Trim() ?? "";

		if (string.IsNullOrWhiteSpace(name)) 
		{
			await DisplayAlert(
				"Error", "Enter counter name.", "OK");
			return;
		}

        if (!int.TryParse(InitialValueEntry.Text,
			out int initialValue))
        {
            await DisplayAlert(
                "Error", "Enter a valid number.", "OK");
            return;
        }

		counter.Name = name;
		counter.Value = initialValue;

        File.WriteAllText(counter.Filename,
			counter.Name + "\n" + counter.Value);

        await Navigation.PopAsync();
    }

    private void MinusButton_Clicked(object sender, EventArgs e)
    {
		counter.Value--;
		ValueLabel.Text = counter.Value.ToString();

        File.WriteAllText(counter.Filename,
			counter.Name + "\n" + counter.Value);
    }

    private void PlusButton_Clicked(object sender, EventArgs e)
    {
        counter.Value++;
        ValueLabel.Text = counter.Value.ToString();

        File.WriteAllText(counter.Filename,
			counter.Name + "\n" + counter.Value);
    }
}