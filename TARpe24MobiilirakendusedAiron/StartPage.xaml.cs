using System.Linq;
namespace TARpe24MobiilirakendusedAiron;

public partial class StartPage : ContentPage
{
    VerticalStackLayout vst;
    ScrollView sv;
    public List<ContentPage> Lehed = new List<ContentPage>() { new TextPage(), new FigurePage() };
    public List<string> leheNimed = new List<string>() { "Tekst", "Kujund" };
    
    public StartPage()
    {
        vst = new VerticalStackLayout() { Padding = 20, Spacing = 18 };
        
        for (int i = 0; i < Lehed.Count; i++)
        {
            int index = i; // Capture the loop variable
            
            Button nupp = new Button
            {
                Text = leheNimed[i],
                FontSize = 36,
                FontFamily = "Socafe 400",
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.Black,
                CornerRadius = 10,
                HeightRequest = 60,
                ZIndex = i
            };
            
            vst.Add(nupp);
            
            nupp.Clicked += (sender, e) =>
            {
                var valik = Lehed[index]; // Use the captured index
                Navigation.PushAsync(valik);
            };
        }
        
        sv = new ScrollView { Content = vst };
        Content = sv;
    }
}