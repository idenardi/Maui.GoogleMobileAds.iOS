namespace Sample;

public partial class MainPage : ContentPage
{
    int count = 0;
#if IOS
    readonly Platforms.iOS.InterstitialAdService _interstitialAdService = new();
#endif

    public MainPage()
    {
        InitializeComponent();
#if IOS
        _interstitialAdService.Load();
#endif
    }

    private void OnInterstitialClicked(object sender, EventArgs e)
    {
#if IOS
        _interstitialAdService.Show();
#endif
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}