namespace TARpe24MobiilirakendusedAiron
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            // Loogika 1: Muuda teksti vastavalt arvule
            if (count == 1)
                CounterBtn.Text = $"Vajutatud {count} kord";
            else
                CounterBtn.Text = $"Vajutatud {count} korda";

            // Loogika 2: Pööra pilti iga vajutusega 15 kraadi
            BotImage.Rotation += 15;

            // Loogika 3: Muuda Labeli teksti
            CounterBtn.Text = $"Nuppu on vajutatud kokku: {count}";
            var random = new Random();
            var randomColor = Color.FromRgb(
                random.Next(0, 256), // Red
                random.Next(0, 256), // Green
                random.Next(0, 256)  // Blue
            );
            if (count >= 10)
            {
                BotImage.IsVisible = false; // Peidab pildi
                CounterBtn.Text = "Pilt kadus ära! Vajuta Reset.";
            }

            // Rakendame värvi teisele nupule või taustale
            ResetBtn.BackgroundColor = randomColor;

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private void OnResetClicked(object? sender, EventArgs e)
        {
            count = 0;
            CounterBtn.Text = "Vajuta mind";
            CounterBtn.Text = "Alustame uuesti!";

            BotImage.Rotation = 0; // Pilt läheb otseks tagasi
            BotImage.IsVisible = true; // Toob pildi tagasi
        }


    }
}
