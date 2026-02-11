namespace TARpe24MobiilirakendusedAiron;

public partial class TextPage : ContentPage
{
    Label lbl;
    Editor editor;
    HorizontalStackLayout hsl;
    public List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    VerticalStackLayout vsl;

    public TextPage()
    {
        lbl = new Label
        {
            Text = "Teksti",
            FontSize = 30,
            FontFamily = "Socafe 400",
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };

        editor = new Editor
        {
            Placeholder = "Kirjutle tekst...",
            PlaceholderColor = Colors.Red,
            FontSize = 18,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center,
        };

        editor.TextChanged += (sender, e) =>
        {
            lbl.Text = editor.Text;
        };

        hsl = new HorizontalStackLayout { Spacing = 20, HorizontalOptions = LayoutOptions.Center };

        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[j],
                FontSize = 15,
                FontFamily = "Socafe 400",
                WidthRequest = 100,
                TextColor = Colors.BlueViolet,
                BackgroundColor = Colors.LightGray,
                CornerRadius = 20,
                HeightRequest = 50,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { lbl, editor, hsl },
            HorizontalOptions = LayoutOptions.Center
        };

        Content = vsl;
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PopAsync();
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new FigurePage());
        }
    }
}