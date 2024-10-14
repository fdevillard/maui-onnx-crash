using Microsoft.ML.OnnxRuntime;

namespace maui_start_test;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		CounterBtn.Text = $"Loading...";
		CounterBtn.IsEnabled = false;

		SemanticScreenReader.Announce(CounterBtn.Text);

		await LoadModel();

		CounterBtn.Text = $"Model loaded";
		CounterBtn.IsEnabled = true;
		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async Task<byte[]> ReadModelFully()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("bidaf-11-int8.onnx");
        await using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        return ms.ToArray();
    }

    private async Task<InferenceSession> LoadModel()
    {
        var modelBytes = await ReadModelFully();
        return new InferenceSession(modelBytes);
    }
}

