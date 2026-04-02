using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel.Communication;

namespace TARpe24MobiilirakendusedAiron;

public partial class SopradeKontakt : ContentPage
{
    TableView tableview;
    SwitchCell sc;
    ImageCell ic;
    TableSection fotosection;

    EntryCell telefonCell;
    EntryCell emailCell;
    EntryCell sonumCell;

    string[] tervitused = new string[]
    {
        "Palju õnne!",
        "Häid pühi!",
        "Ilusat päeva!",
        "Edu sulle!",
        "Parimat soovides!"
    };

    public SopradeKontakt()
    {
        sc = new SwitchCell { Text = "Näita veel" };
        sc.OnChanged += Sc_OnChanged;

        ic = new ImageCell
        {
            ImageSource = ImageSource.FromFile("bob.jpg"),
            Text = "foto ei ole",
            Detail = "mingi kirjeldus vms kork teemus"
        };

        fotosection = new TableSection();

        // Sisestusväljad
        telefonCell = new EntryCell
        {
            Label = "Telefon",
            Placeholder = "Sisesta tel. number",
            Keyboard = Keyboard.Telephone
        };

        emailCell = new EntryCell
        {
            Label = "Email",
            Placeholder = "Sisesta email",
            Keyboard = Keyboard.Email
        };

        sonumCell = new EntryCell
        {
            Label = "Sõnum",
            Placeholder = "Sisesta sõnum"
        };

        // Nupud (ViewCell sees)
        var callBtn = new Button { Text = "HELISTA" };
        callBtn.Clicked += Helista_Clicked;

        var smsBtn = new Button { Text = "SAADA SMS" };
        smsBtn.Clicked += Saada_sms_Clicked;

        var emailBtn = new Button { Text = "SAADA EMAIL" };
        emailBtn.Clicked += Saada_email_Clicked;

        var greetBtn = new Button { Text = "ÕNNITLUS" };
        greetBtn.Clicked += Saada_tervitus_Clicked;

        var buttonLayout = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Children = { callBtn, smsBtn, emailBtn, greetBtn }
        };

        tableview = new TableView
        {
            Root = new TableRoot
            {
                new TableSection("Kontaktandmed:")
                {
                    telefonCell,
                    emailCell,
                    sonumCell,
                    sc
                },

                new TableSection("Lisavõimalused:")
                {
                    new ViewCell { View = buttonLayout }
                },

                fotosection
            }
        };

        Content = tableview;
    }

    // 
    private void Sc_OnChanged(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            fotosection.Title = "Foto:";
            fotosection.Add(ic);
            sc.Text = "Peida";
        }
        else
        {
            fotosection.Title = "";
            fotosection.Remove(ic);
            sc.Text = "Näita veel";
        }
    }

    // 
    private void Helista_Clicked(object sender, EventArgs e)
    {
        string phone = telefonCell.Text;

        if (!string.IsNullOrWhiteSpace(phone))
        {
            PhoneDialer.Open(phone);
        }
    }

    // 
    private async void Saada_sms_Clicked(object sender, EventArgs e)
    {
        string phone = telefonCell.Text;
        string message = sonumCell.Text;

        if (!string.IsNullOrWhiteSpace(phone))
        {
            SmsMessage sms = new SmsMessage(message, phone);

            if (Sms.Default.IsComposeSupported)
                await Sms.Default.ComposeAsync(sms);
        }
    }

    // 
    private async void Saada_email_Clicked(object sender, EventArgs e)
    {
        string email = emailCell.Text;
        string message = sonumCell.Text;

        if (string.IsNullOrWhiteSpace(email)) return;

        EmailMessage e_mail = new EmailMessage
        {
            Subject = "Tervitus",
            Body = message,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string> { email }
        };

        if (Email.Default.IsComposeSupported)
            await Email.Default.ComposeAsync(e_mail);
        else
            await DisplayAlert("Viga", "E-mail ei ole toetatud", "OK");
    }

    // 
    private async void Saada_tervitus_Clicked(object sender, EventArgs e)
    {
        var rnd = new Random();
        string juhuslik = tervitused[rnd.Next(tervitused.Length)];

        string phone = telefonCell.Text;

        if (!string.IsNullOrWhiteSpace(phone))
        {
            SmsMessage sms = new SmsMessage(juhuslik, phone);

            if (Sms.Default.IsComposeSupported)
                await Sms.Default.ComposeAsync(sms);
        }
    }
}