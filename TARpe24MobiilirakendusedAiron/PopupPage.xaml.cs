
using System.Threading.Tasks;

namespace TARpe24MobiilirakendusedAiron;

public partial class PopupPage : ContentPage
{
	public PopupPage()
	{


		Button alertListButton = new Button
		{
			Text = "Mõistatus",
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

        Button alertMathButton = new Button
		{
			Text = "Korrutustabeli Test",
			VerticalOptions = LayoutOptions.Start,
			HorizontalOptions = LayoutOptions.Center
		};
		alertMathButton.Clicked += AlertMathButton_Clicked;

        Content = new VerticalStackLayout
        {
		Spacing = 20,
		Padding = new Thickness (0, 50, 0, 0),
		Children = {alertListButton, alertQuestButton, alertMathButton}
		};
    }


	private async void AlertListButton_Clicked(object? sender, EventArgs e)
	{
		string action = await DisplayActionSheetAsync("Mis hinde ma eest saan", "", "Loobu", "5", "4", "3");

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
		string result1 = await DisplayPromptAsync("Küsimus","Kuidas installida Opsec-i (all lowercase)", placeholder:"Kirjuta");

		if (result1 != null)
		{
			if (result1 == "sudo apt install opsec")
			await DisplayAlertAsync("Õige!", "Kirjutasid: " + result1, "OK");
			if (result1 == "")
                await DisplayAlertAsync("Vale!", "Sa ei kirjutanud midagi" + result1, "OK");
            else
                await DisplayAlertAsync("Vale!", "Kirjutasid : " + result1, "OK");
        }
	}
    private async void AlertMathButton_Clicked(object? sender, EventArgs e)
    {
        string[] questions =
        {
        "2 * 3 = ?",
        "4 * 5 = ?",
        "6 * 7 = ?",
        "8 * 9 = ?",
        "3 * 4 = ?"
    };

        string[] answers =
        {
        "6",
        "20",
        "42",
        "72",
        "12"
    };

        int correct = 0;
        List<string> resultList = new List<string>();

        for (int i = 0; i < questions.Length; i++)
        {
            string result = await DisplayPromptAsync(
                "Küsimus " + (i + 1),
                questions[i],
                placeholder: "Kirjuta vastus"
            );

            if (result == null || result == "")
            {
                resultList.Add($"Q{i + 1}: Vastamata");
                continue;
            }

            if (result == answers[i])
            {
                correct++;
                resultList.Add($"Q{i + 1}: Õige ({result})");
            }
            else
            {
                resultList.Add($"Q{i + 1}: Vale ({result})");
            }
        }

        string resultsText = string.Join("\n", resultList);

        await DisplayAlertAsync(
            "Test läbi!",
            $"Õigeid vastuseid: {correct}/5\n\nTulemused:\n{resultsText}",
            "OK"
        );
    }


}



