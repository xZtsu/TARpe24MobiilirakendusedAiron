using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TARpe24MobiilirakendusedAiron
{
    public class Riik
    {
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public int Rahvaarv { get; set; }
        public string Lipp { get; set; }
    }
    public class EuroopaRiigid : ContentPage
    {
        // Globaalsed muutujad, et neile pääseks ligi ka sündmuste töötlejates
        ObservableCollection<Riik> riiks;
        ListView list;
        Entry entryNimi, entryPealinn, entryRahvaarv, entryLipp;

        public EuroopaRiigid()
        {
            Title = "Riikide haldus";

            // Andmebaasi algväärtustamine
            riiks = new ObservableCollection<Riik>
            {
                new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv=14000000, Lipp="eesti.jng" },
                new Riik { Nimi="Soome", Pealinn="Helsingi", Rahvaarv=1234, Lipp="Soome.png" },
                new Riik { Nimi="Läti", Pealinn="Idk", Rahvaarv=0, Lipp="Lati.png" }
            };

            // Tekstilahtrite loomine
            entryNimi = new Entry { Placeholder = "Riigi nimi (nt Eesti)" };
            entryPealinn = new Entry { Placeholder = "Pealinn (nt Tallinn)" };
            entryRahvaarv = new Entry { Placeholder = "Rahvaarv (täisarv)", Keyboard = Keyboard.Numeric };
            entryLipp = new Entry { Placeholder = "Pildi failinimi (valikuline)" };

            ////
            // Nuppude loomine
            Button btnLisa = new Button
            {
                Text = "Lisa Riik",
                BackgroundColor = Colors.LightGreen // Xamarinis kasuta: Color.LightGreen
            };
            btnLisa.Clicked += BtnLisa_Clicked;

            Button btnKustuta = new Button
            {
                Text = "Kustuta valitud Riik",
                BackgroundColor = Colors.LightPink // Xamarinis kasuta: Color.LightPink
            };
            btnKustuta.Clicked += BtnKustuta_Clicked;

            // ListView loomine ja kujundamine
            list = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = riiks,
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
                imgPilt.SetBinding(Image.SourceProperty, "Lipp"); // Seome pildi failinimega

                // -- 2. Loome tekstid --
                Label lblNimetus = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center };
                lblNimetus.SetBinding(Label.TextProperty, "Nimi");

                Label lblTootja = new Label { TextColor = Colors.Gray, VerticalOptions = LayoutOptions.Center }; // Xamarin: Color.Gray
                lblTootja.SetBinding(Label.TextProperty, "Pealinn");

                Label lblHind = new Label { TextColor = Colors.DarkBlue, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center }; // Xamarin: Color.DarkBlue
                lblHind.SetBinding(Label.TextProperty, new Binding("Rahvaarv", stringFormat: "{0} inimest")); 

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
                    entryNimi,
                    entryPealinn,
                    entryRahvaarv,
                    entryLipp,
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
            Riik valitudRiik = e.Item as Riik;

            if (valitudRiik != null)
            {
                await DisplayAlertAsync("Riigi info", $"Riik: {valitudRiik.Nimi}\nPealinn: {valitudRiik.Pealinn}\nRahvaarv: {valitudRiik.Rahvaarv} inimest", "Sulge");
            }
        }
        // 2. Riigi kustutamine (koos kinnitusega)
        private async void BtnKustuta_Clicked(object? sender, EventArgs e)
        {
            Riik valitudRiik = list.SelectedItem as Riik;

            if (valitudRiik != null)
            {
                bool vastus = await DisplayAlertAsync("Kinnitus", $"Kas oled kindel, et soovid Riigi {valitudRiik.Nimi} kustutada?", "Jah", "Ei");

                if (vastus == true)
                {
                    riiks.Remove(valitudRiik);
                    list.SelectedItem = null; // Tühistame valiku
                }
            }
            else
            {
                await DisplayAlertAsync("Viga", "Palun vali nimekirjast Riik, mida soovid kustutada.", "OK");
            }
        }
        // 1. Uue telefoni lisamine
        private void BtnLisa_Clicked(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryNimi.Text) && !string.IsNullOrWhiteSpace(entryPealinn.Text))
            {
                int rahvaarv = 0;
                int.TryParse(entryRahvaarv.Text, out rahvaarv);

                // Kui pildi failinime ei sisestata, kasuta vaikimisi pilti
                string pildiNimi = string.IsNullOrWhiteSpace(entryLipp.Text) ? "default_phone.png" : entryLipp.Text;

                riiks.Add(new Riik
                {
                    Nimi = entryNimi.Text,
                    Pealinn = entryPealinn.Text,
                    Rahvaarv = rahvaarv,
                    Lipp = pildiNimi
                });

                // Tühjendame väljad pärast lisamist
                entryNimi.Text = "";
                entryPealinn.Text = "";
                entryRahvaarv.Text = "";
                entryLipp.Text = "";
            }
            else
            {
                DisplayAlertAsync("Viga", "Palun täida vähemalt mudeli ja tootja väljad!", "OK");
            }
        }
    }
}
