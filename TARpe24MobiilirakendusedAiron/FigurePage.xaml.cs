namespace TARpe24MobiilirakendusedAiron;

public partial class FigurePage : ContentPage
{
    VerticalStackLayout vsl;
    HorizontalStackLayout hsl;
    BoxView boxView;
    Frame circleFrame;
    Frame triangleFrame;
    public List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };

    public FigurePage()
    {
        // Purple rounded square (BoxView with Frame)
        boxView = new BoxView
        {
            Color = Color.FromRgb(155, 89, 182), // Purple color
            WidthRequest = 200,
            HeightRequest = 200,
            CornerRadius = 30,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 0)
        };

        // Orange circle (Frame with circular BoxView)
        circleFrame = new Frame
        {
            WidthRequest = 200,
            HeightRequest = 200,
            CornerRadius = 100,
            BackgroundColor = Color.FromRgb(230, 126, 34), // Orange color
            Padding = 0,
            HasShadow = false,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 0)
        };

        // Green triangle (using Polygon or custom drawing)
        triangleFrame = new Frame
        {
            WidthRequest = 200,
            HeightRequest = 200,
            BackgroundColor = Colors.Transparent,
            Padding = 0,
            HasShadow = false,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 0),
            Content = new GraphicsView
            {
                Drawable = new TriangleDrawable(),
                WidthRequest = 200,
                HeightRequest = 200
            }
        };

        // Buttons at the bottom
        hsl = new HorizontalStackLayout
        {
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 0)
        };

        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[j],
                FontSize = 18,
                FontFamily = "Socafe 400",
                WidthRequest = 100,
                TextColor = Color.FromRgb(230, 126, 34), // Orange text
                BackgroundColor = Color.FromRgb(255, 248, 220), // Light cream background
                CornerRadius = 15,
                HeightRequest = 45,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }

        // Main vertical layout
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children = { boxView, circleFrame, triangleFrame, hsl },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        Content = vsl;
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0) // Tagasi
        {
            Navigation.PopAsync();
        }
        else if (nupp.ZIndex == 1) // Avaleht
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2) // Edasi
        {
            Navigation.PushAsync(new TextPage());
        }
    }
}

// Triangle drawable class
public class TriangleDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.FromRgb(46, 204, 113); // Green color

        PathF path = new PathF();
        // Draw triangle
        path.MoveTo(dirtyRect.Width / 2, 10); // Top point
        path.LineTo(dirtyRect.Width - 10, dirtyRect.Height - 10); // Bottom right
        path.LineTo(10, dirtyRect.Height - 10); // Bottom left
        path.Close();

        canvas.FillPath(path);
    }
}