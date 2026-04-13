using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TARpe24MobiilirakendusedAiron
{
    // 1. Andmemudel
    public class Telefon
    {
        public string Nimetus { get; set; }
        public string Tootja { get; set; }
        public int Hind { get; set; }
        public string Pilt { get; set; }
    }

    // 2. Põhileht
    public class ListViewPage : ContentPage
    {
        // Globaalsed muutujad, et neile pääseks ligi ka sündmuste töötlejates
        ObservableCollection<Telefon> telefons;
        ListView list;
        Entry entryNimetus, entryTootja, entryHind, entryPilt;

        public ListViewPage()
        {
            Title = "Telefonide haldus";

            // Andmebaasi algväärtustamine
            telefons = new ObservableCollection<Telefon>
            {
                new Telefon { Nimetus="Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349, Pilt="Galaxy.png" },
                new Telefon { Nimetus="Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399, Pilt="Xiaomi5GNE.png" },
                new Telefon { Nimetus="iPhone 17", Tootja="Apple", Hind=1179, Pilt="iPhone17.png" }
            };

            // Tekstilahtrite loomine
            entryNimetus = new Entry { Placeholder = "Telefoni mudel (nt iPhone 18)" };
            entryTootja = new Entry { Placeholder = "Tootja (nt Apple)" };
            entryHind = new Entry { Placeholder = "Hind (täisarv)", Keyboard = Keyboard.Numeric };
            entryPilt = new Entry { Placeholder = "Pildi failinimi (valikuline)" };

            // Nuppude loomine
            Button btnLisa = new Button
            {
                Text = "Lisa telefon",
                BackgroundColor = Colors.LightGreen // Xamarinis kasuta: Color.LightGreen
            };
            btnLisa.Clicked += BtnLisa_Clicked;

            Button btnKustuta = new Button
            {
                Text = "Kustuta valitud telefon",
                BackgroundColor = Colors.LightPink // Xamarinis kasuta: Color.LightPink
            };
            btnKustuta.Clicked += BtnKustuta_Clicked;

            // ListView loomine ja kujundamine
            list = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = telefons,
                SelectionMode = ListViewSelectionMode.Single
            };

            // Seome valimise sündmuse
            list.ItemTapped += List_ItemTapped;

            // Kuidas iga rida (telefon) välja näeb
            list.ItemTemplate = new DataTemplate(() =>
            {

                // -- 1. Loome pildi --
                Image imgPilt = new Image
                {
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Aspect = Aspect.AspectFit,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 10, 0) // Paremalt veeris, kui pilt on vasakul
                };
                imgPilt.SetBinding(Image.SourceProperty, "Pilt"); // Seome pildi failinimega

                // -- 2. Loome tekstid --
                Label lblNimetus = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center };
                lblNimetus.SetBinding(Label.TextProperty, "Nimetus");

                Label lblTootja = new Label { TextColor = Colors.Gray, VerticalOptions = LayoutOptions.Center }; // Xamarin: Color.Gray
                lblTootja.SetBinding(Label.TextProperty, "Tootja");

                Label lblHind = new Label { TextColor = Colors.DarkBlue, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center }; // Xamarin: Color.DarkBlue
                lblHind.SetBinding(Label.TextProperty, new Binding("Hind", stringFormat: "{0} €")); // Lisame € märgi

                // Teksti paigutus (vertikaalne virn)
                var textLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.Center,
                    Children = { lblNimetus, lblTootja, lblHind }
                };

                // -- 3. Loome REA PEAVIRNA (horisontaalne StackLayout) --
                var rowLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(10),
                    VerticalOptions = LayoutOptions.Center,

                    // !!! SIIN MÄÄRATAKSE PILDI ASUKOHT !!!

                    // PILT VASAKUL:
                    Children = { imgPilt, textLayout }

                    // PILT PAREMAL (kommenteeri eelmine rida välja ja kasuta seda):
                    // Children = { textLayout, imgPilt } 
                };

                // Kui pilt on paremal, muuda ka pildi veerist:
                // imgPilt.Margin = new Thickness(10, 0, 0, 0); // Vasakult veeris

                return new ViewCell
                {
                    View = rowLayout
                };
            });

            // Paneme kõik elemendid lehele kokku
            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    entryNimetus,
                    entryTootja,
                    entryHind,
                    entryPilt,
                    btnLisa,
                    btnKustuta,
                    list // Nimekiri on kõige all
                }
            };
        }
        // --- SÜNDMUSTE TÖÖTLEJAD ---

        // 3. Elemendile vajutamine
        private async void List_ItemTapped(object? sender, ItemTappedEventArgs e)
        {
            Telefon valitudTelefon = e.Item as Telefon;

            if (valitudTelefon != null)
            {
                await DisplayAlertAsync("Telefoni info", $"Tootja: {valitudTelefon.Tootja}\nMudel: {valitudTelefon.Nimetus}\nHind: {valitudTelefon.Hind} €", "Sulge");
            }
        }
        // 2. Telefoni kustutamine (koos kinnitusega)
        private async void BtnKustuta_Clicked(object? sender, EventArgs e)
        {
            Telefon valitudTelefon = list.SelectedItem as Telefon;

            if (valitudTelefon != null)
            {
                bool vastus = await DisplayAlertAsync("Kinnitus", $"Kas oled kindel, et soovid mudeli {valitudTelefon.Nimetus} kustutada?", "Jah", "Ei");

                if (vastus == true)
                {
                    telefons.Remove(valitudTelefon);
                    list.SelectedItem = null; // Tühistame valiku
                }
            }
            else
            {
                await DisplayAlertAsync("Viga", "Palun vali nimekirjast telefon, mida soovid kustutada.", "OK");
            }
        }
        // 1. Uue telefoni lisamine
        private void BtnLisa_Clicked(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryNimetus.Text) && !string.IsNullOrWhiteSpace(entryTootja.Text))
            {
                int hind = 0;
                int.TryParse(entryHind.Text, out hind);

                // Kui pildi failinime ei sisestata, kasuta vaikimisi pilti
                string pildiNimi = string.IsNullOrWhiteSpace(entryPilt.Text) ? "default_phone.png" : entryPilt.Text;

                telefons.Add(new Telefon
                {
                    Nimetus = entryNimetus.Text,
                    Tootja = entryTootja.Text,
                    Hind = hind,
                    Pilt = pildiNimi
                });

                // Tühjendame väljad pärast lisamist
                entryNimetus.Text = "";
                entryTootja.Text = "";
                entryHind.Text = "";
                entryPilt.Text = "";
            }
            else
            {
                DisplayAlertAsync("Viga", "Palun täida vähemalt mudeli ja tootja väljad!", "OK");
            }
        }



    }
}


