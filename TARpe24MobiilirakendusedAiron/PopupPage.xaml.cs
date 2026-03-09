
using System.Threading.Tasks;

namespace TARpe24MobiilirakendusedAiron;

public partial class PopupPage : ContentPage
{
	public PopupPage()
	{


		Button alertListButton = new Button
		{
			Text = "Hinne",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
		alertListButton.Clicked += AlertListButton_Clicked;

		Button alertQuestButton = new Button
		{
			Text = "Küsimus",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
		alertQuestButton.Clicked += AlertQuestButton_Clicked;

        Content = new VerticalStackLayout
        {
		Spacing = 20,
		Padding = new Thickness (0, 50, 0, 0),
		Children = {alertListButton, alertQuestButton}
		};
    }


	private async void AlertListButton_Clicked(object? sender, EventArgs e)
	{
		string action = await DisplayActionSheetAsync("Mis hinde ma selle eest saan","", "Loobu", "5", "4", "3");

		if (action != null && action != "Loobu")
		{
			if (action == "5")
			await DisplayAlertAsync("Õige!", "Valisid õigesti: " + action, "OK");
			else
                await DisplayAlertAsync("Vale!", "Valisid vale vastuse: " + action, "OK");
        }
	}

	private async void AlertQuestButton_Clicked(object? sender, EventArgs e)
	{
		string result1 = await DisplayPromptAsync("Küsimus","Kuidas installida Opsec-i", placeholder:"Kirjuta");

		if (result1 != null)
		{
			if (result1 == "sudo apt install opsec")
			await DisplayAlertAsync("Õige!", "Kirjutasid: " + result1, "OK");
			if (result1 == null)
                await DisplayAlertAsync("Vale!", "Sa ei kirjutanud midagi" + result1, "OK");
            else
                await DisplayAlertAsync("Vale!", "Kirjutasid : " + result1, "OK");
        }
	}

		
}



