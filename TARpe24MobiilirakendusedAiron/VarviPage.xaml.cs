using Microsoft.Maui.Layouts;
namespace TARpe24MobiilirakendusedAiron;

public partial class VarviPage : ContentPage
{

    Slider sliderR;
    Slider sliderG;
    Slider sliderB;
    Label lblR;
    Label lblG;
    Label lblB;
    Stepper stepper;
    Frame colorFrame;
    AbsoluteLayout al;
    public VarviPage()
    {
        //InitializeComponent();

        al = new AbsoluteLayout();
        Label title = new Label
        {
            Text = "RGB mudel",
            FontSize = 30,
            HorizontalOptions = LayoutOptions.Center
        };
        AbsoluteLayout.SetLayoutBounds(title, new Rect(0.5, 0.05, -1, -1));
        AbsoluteLayout.SetLayoutFlags(title, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(title);

        Frame boxR = CreateColorBox(Colors.Red);
        Frame boxG = CreateColorBox(Colors.Green);
        Frame boxB = CreateColorBox(Colors.Blue);

        AbsoluteLayout.SetLayoutBounds(boxR, new Rect(0.15, 0.15, 30, 30));
        AbsoluteLayout.SetLayoutFlags(boxR, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(boxG, new Rect(0.15, 0.25, 30, 30));
        AbsoluteLayout.SetLayoutFlags(boxG, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(boxB, new Rect(0.15, 0.35, 30, 30));
        AbsoluteLayout.SetLayoutFlags(boxB, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(boxR);
        al.Children.Add(boxG);
        al.Children.Add(boxB);

        Frame CreateColorBox(Color color)
        {
            return new Frame
            {
                WidthRequest = 30,
                HeightRequest = 30,
                BackgroundColor = color,
                CornerRadius = 0,
            };
        }
        sliderR = CreateSlider();
        sliderG = CreateSlider();
        sliderB = CreateSlider();
        lblR = CreateValueLabel();
        lblG = CreateValueLabel();
        lblB = CreateValueLabel();

        AddSliderRow(sliderR, lblR, 0.15);
        AddSliderRow(sliderG, lblG, 0.25);
        AddSliderRow(sliderB, lblB, 0.35);
        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 50,
            Increment = 5,
            Value = 0,
        };
        stepper.ValueChanged += OnColorChanged;
        AbsoluteLayout.SetLayoutBounds(stepper, new Rect(0.5, 0.45, -1, -1));
        AbsoluteLayout.SetLayoutFlags(stepper, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(stepper);

        colorFrame = new Frame
        {
            WidthRequest = 200,
            HeightRequest = 200,
            BackgroundColor = Colors.Black,
        };

        AbsoluteLayout.SetLayoutBounds(colorFrame, new Rect(0.5, 0.75, -1, -1));
        AbsoluteLayout.SetLayoutFlags(colorFrame, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(colorFrame);
        Content = al;
    }
    Slider CreateSlider()
    {
        var slider = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
        };
        slider.ValueChanged += OnColorChanged;
        return slider;
    }
    Label CreateValueLabel()
    {
        return new Label
        {
            Text = "0",
            FontSize = 18,
            FontFamily = "Luffio",
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Start
        };
    }
    void AddSliderRow(Slider slider, Label label, double y)
    {
        AbsoluteLayout.SetLayoutBounds(slider, new Rect(0.5, y, 300, -1));
        AbsoluteLayout.SetLayoutFlags(slider, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(slider);
        AbsoluteLayout.SetLayoutBounds(label, new Rect(0.9, y, -1, -1));
        AbsoluteLayout.SetLayoutFlags(label, AbsoluteLayoutFlags.PositionProportional);
        al.Children.Add(label);
    }
    void OnColorChanged(object sender, EventArgs e)
    {
        int r = (int)sliderR.Value;
        int g = (int)sliderG.Value;
        int b = (int)sliderB.Value;
        lblR.Text = r.ToString();
        lblG.Text = g.ToString();
        lblB.Text = b.ToString();
        colorFrame.BackgroundColor = Color.FromRgb(r, g, b);

        colorFrame.CornerRadius = (float)stepper.Value;
    }
}