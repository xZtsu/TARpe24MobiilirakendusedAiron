namespace TARpe24MobiilirakendusedAiron;

public partial class ValgusfoorPage : ContentPage
{

        bool foorOn = false;

        public ValgusfoorPage()
        {
            InitializeComponent();
            LisaKlikid();

            KeelaKlikid();
        }

        void LisaKlikid()
        {
            RedLight.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (!foorOn) return;
                    StatusLabel.Text = "Seisa";
                })
            });

            YellowLight.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (!foorOn) return;
                    StatusLabel.Text = "Valmista";
                })
            });

            GreenLight.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (!foorOn) return;
                    StatusLabel.Text = "Sõida";
                })
            });
        }

        void OnSisseClicked(object sender, EventArgs e)
        {
            foorOn = true;

            RedLight.Color = Colors.Red;
            YellowLight.Color = Colors.Yellow;
            GreenLight.Color = Colors.Green;

            StatusLabel.Text = "Vali valgus";
            LubaKlikid();
        }

        void OnValjaClicked(object sender, EventArgs e)
        {
            foorOn = false;

            RedLight.Color = Colors.Gray;
            YellowLight.Color = Colors.Gray;
            GreenLight.Color = Colors.Gray;

            StatusLabel.Text = "Lülita esmalt foor sisse";
            KeelaKlikid();
        }

        void LubaKlikid()
        {
            RedLight.IsEnabled = true;
            YellowLight.IsEnabled = true;
            GreenLight.IsEnabled = true;
        }

        void KeelaKlikid()
        {
            RedLight.IsEnabled = false;
            YellowLight.IsEnabled = false;
            GreenLight.IsEnabled = false;
        }
    
}