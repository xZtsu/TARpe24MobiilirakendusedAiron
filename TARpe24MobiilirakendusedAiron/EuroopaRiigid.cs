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
        ObservableCollection<Riik> riiks;
        ListView list;
        Entry entryNimi, entryPealinn, entryRahvaarv;
        Label lblValitudPilt;           
        string valitudPildiTee = "";    

        public EuroopaRiigid()
        {
            Title = "Riikide haldus";

            riiks = new ObservableCollection<Riik>
            {
                new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv=14000000, Lipp="eesti.jng" },
                new Riik { Nimi="Soome", Pealinn="Helsingi", Rahvaarv=1234, Lipp="" },
                new Riik { Nimi="Läti", Pealinn="Idk", Rahvaarv=0, Lipp="" }
            };

            entryNimi = new Entry { Placeholder = "Riigi nimi (nt Eesti)" };
            entryPealinn = new Entry { Placeholder = "Pealinn (nt Tallinn)" };
            entryRahvaarv = new Entry { Placeholder = "Rahvaarv (täisarv)", Keyboard = Keyboard.Numeric };

            // gallery
            Button btnValiPilt = new Button
            {
                Text = "Vali lipp galeriist",
                BackgroundColor = Colors.LightSkyBlue
            };
            btnValiPilt.Clicked += BtnValiPilt_Clicked;

            lblValitudPilt = new Label
            {
                Text = "Pilti ei ole valitud",
                TextColor = Colors.Gray
            };
            // ───────────────────────────────────────────────────────────

            Button btnLisa = new Button
            {
                Text = "Lisa Riik",
                BackgroundColor = Colors.LightGreen
            };
            btnLisa.Clicked += BtnLisa_Clicked;

            Button btnKustuta = new Button
            {
                Text = "Kustuta valitud Riik",
                BackgroundColor = Colors.LightPink
            };
            btnKustuta.Clicked += BtnKustuta_Clicked;

            list = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = riiks,
                SelectionMode = ListViewSelectionMode.Single
            };

            list.ItemTapped += List_ItemTapped;

            list.ItemTemplate = new DataTemplate(() =>
            {
                Image imgPilt = new Image
                {
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Aspect = Aspect.AspectFit,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 10, 0)
                };
                imgPilt.SetBinding(Image.SourceProperty, "Lipp");

                Label lblNimetus = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center };
                lblNimetus.SetBinding(Label.TextProperty, "Nimi");

                Label lblTootja = new Label { TextColor = Colors.Gray, VerticalOptions = LayoutOptions.Center };
                lblTootja.SetBinding(Label.TextProperty, "Pealinn");

                Label lblHind = new Label { TextColor = Colors.DarkBlue, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center };
                lblHind.SetBinding(Label.TextProperty, new Binding("Rahvaarv", stringFormat: "{0} inimest"));

                var textLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.Center,
                    Children = { lblNimetus, lblTootja, lblHind }
                };

                var rowLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(10),
                    VerticalOptions = LayoutOptions.Center,
                    Children = { imgPilt, textLayout }
                };

                return new ViewCell { View = rowLayout };
            });

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    entryNimi,
                    entryPealinn,
                    entryRahvaarv,
                    btnValiPilt,      
                    lblValitudPilt,   
                    btnLisa,
                    btnKustuta,
                    list
                }
            };
        }

        //gallery
        private async void BtnValiPilt_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    valitudPildiTee = photo.FullPath;
                    lblValitudPilt.Text = $"Valitud: {photo.FileName}";
                    lblValitudPilt.TextColor = Colors.Green;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", "Pildi valimine ebaõnnestus: " + ex.Message, "OK");
            }
        }
        

        private async void List_ItemTapped(object? sender, ItemTappedEventArgs e)
        {
            Riik valitudRiik = e.Item as Riik;

            if (valitudRiik != null)
            {
                await DisplayAlert("Riigi info", $"Riik: {valitudRiik.Nimi}\nPealinn: {valitudRiik.Pealinn}\nRahvaarv: {valitudRiik.Rahvaarv} inimest", "Sulge");
            }
        }

        private async void BtnKustuta_Clicked(object? sender, EventArgs e)
        {
            Riik valitudRiik = list.SelectedItem as Riik;

            if (valitudRiik != null)
            {
                bool vastus = await DisplayAlert("Kinnitus", $"Kas oled kindel, et soovid Riigi {valitudRiik.Nimi} kustutada?", "Jah", "Ei");

                if (vastus == true)
                {
                    riiks.Remove(valitudRiik);
                    list.SelectedItem = null;
                }
            }
            else
            {
                await DisplayAlert("Viga", "Palun vali nimekirjast Riik, mida soovid kustutada.", "OK");
            }
        }

        private async void BtnLisa_Clicked(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryNimi.Text) && !string.IsNullOrWhiteSpace(entryPealinn.Text))
            {
                //  dupe kontroll
                bool riikOnOlemas = riiks.Any(r => r.Nimi.Equals(entryNimi.Text, StringComparison.OrdinalIgnoreCase));

                if (riikOnOlemas)
                {
                    await DisplayAlert("Viga", $"Riik '{entryNimi.Text}' on juba nimekirjas!", "OK");
                    return; 
                }

                int rahvaarv = 0;
                int.TryParse(entryRahvaarv.Text, out rahvaarv);

                string pildiNimi = string.IsNullOrWhiteSpace(valitudPildiTee) ? "default_lipp.png" : valitudPildiTee;

                riiks.Add(new Riik
                {
                    Nimi = entryNimi.Text,
                    Pealinn = entryPealinn.Text,
                    Rahvaarv = rahvaarv,
                    Lipp = pildiNimi
                });

                entryNimi.Text = "";
                entryPealinn.Text = "";
                entryRahvaarv.Text = "";
                valitudPildiTee = "";
                lblValitudPilt.Text = "Pilti ei ole valitud";
                lblValitudPilt.TextColor = Colors.Gray;
            }
            else
            {
                await DisplayAlert("Viga", "Palun täida vähemalt nimi ja pealinn väljad!", "OK");
            }
        }
    }
}