

namespace TARpe24MobiilirakendusedAiron;

public partial class Lumememm : ContentPage
{
    private bool isTantsiRunning = false;
    public Lumememm() => InitializeComponent();

    private void OPC_Change(object sender, ValueChangedEventArgs e)
    {
        double opacity = e.NewValue;
        pea.Opacity = opacity;
        keha.Opacity = opacity;
        keha2.Opacity = opacity;
        lumeMemm.Opacity = opacity;
    }
    private async void Nupp_Clicked(object sender, EventArgs e)
    {
        string valitudTegevus = tegevusPicker.SelectedItem as string;
        double kiirus = stepperKiirus.Value;
        uint kiirusMs = (uint)(1000 / kiirus);

        switch (valitudTegevus)
        {
            case "Peida lumememm":
                tegevusLabel.Text = "Tegevus: Peida lumememm";
                await lumeMemm.FadeTo(0, kiirusMs);
                lumeMemm.IsVisible = false;
                break;

            case "Näita lumememm":
                tegevusLabel.Text = "Tegevus: Näita lumememm";
                lumeMemm.IsVisible = true;
                await lumeMemm.FadeTo(1, kiirusMs);
                break;

            case "Muuda värvi":
                tegevusLabel.Text = "Tegevus: Muuda värvi";
                Color[] varvid = { Colors.Blue, Colors.Red, Colors.Green};
                Random rnd = new Random();
                Color uusVarv = varvid[rnd.Next(varvid.Length)];
                pea.BackgroundColor = uusVarv;
                keha.BackgroundColor = uusVarv;
                keha2.BackgroundColor = uusVarv;
                break;

            case "Sulata":
                tegevusLabel.Text = "Tegevus: Sulata";
                await lumeMemm.ScaleTo(0, kiirusMs * 2);
                lumeMemm.IsVisible = false;
                lumeMemm.Scale = 1;
                break;

            case "Tantsi":
                tegevusLabel.Text = "Tegevus: Tantsi";
                    isTantsiRunning = true;
                for (int i = 0; i < 2 && isTantsiRunning; i++)
                {
                    await lumeMemm.TranslateTo(-30, 0, kiirusMs / 2);
                    await lumeMemm.TranslateTo(30, 0, kiirusMs / 2);
                    await lumeMemm.TranslateTo(0, 0, kiirusMs / 2);
                }
                break;




        }
    }
}