namespace TARpe24MobiilirakendusedAiron;

public partial class TripsTrapsTrull : ContentPage
{
    Grid grid;
    string currentPlayer = "X";
    Button[,] buttons;
    int boardSize = 3;
    Label titleLabel;
    Button startButton;
    Button whoFirstButton;
    Picker sizePicker;

    public TripsTrapsTrull()
    {
        BackgroundColor = Color.FromArgb("#FFFFFF");

        titleLabel = new Label
        {
            Text = "Trips Traps Trull",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 10)
        };

        sizePicker = new Picker
        {
            Title = "Laua suurus",
            ItemsSource = new List<int> { 3, 4, 5 },
            SelectedIndex = 0,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 160,
            Margin = new Thickness(0, 0, 0, 16)
        };
        sizePicker.SelectedIndexChanged += (s, e) =>
        {
            if (sizePicker.SelectedItem != null)
            {
                BuildGrid((int)sizePicker.SelectedItem);
                currentPlayer = "X";
            }
        };

        grid = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.White,
            Margin = new Thickness(0, 0, 0, 30)
        };

        BuildGrid(boardSize);

        startButton = new Button
        {
            Text = "Resetti mäng",
            FontSize = 16,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            BorderColor = Colors.Black,
            BorderWidth = 1,
            CornerRadius = 10,
            Padding = new Thickness(20, 10),
            WidthRequest = 140
        };
        startButton.Clicked += (s, e) => ResetGame();

        whoFirstButton = new Button
        {
            Text = "Kes essa?",
            FontSize = 16,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            BorderColor = Colors.Black,
            BorderWidth = 1,
            CornerRadius = 10,
            Padding = new Thickness(20, 10),
            WidthRequest = 140
        };
        whoFirstButton.Clicked += async (s, e) =>
        {
            string first = new Random().Next(2) == 0 ? "X" : "O";
            currentPlayer = first;
            await DisplayAlert("Esimene mängija", $"{first} alustab!", "OK");
        };

        var buttonRow = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 16,
            Children = { startButton, whoFirstButton }
        };

        Content = new VerticalStackLayout
        {
            Children = { titleLabel, sizePicker, grid, buttonRow }
        };
    }

    void BuildGrid(int size)
    {
        boardSize = size;
        buttons = new Button[size, size];

        int fontSize = size switch
        {
            3 => 36,
            4 => 26,
            5 => 20,
            _ => 16
        };

        int gridSize = size switch
        {
            3 => 220,
            4 => 280,
            5 => 320,
            _ => 320
        };

        grid.WidthRequest = gridSize;
        grid.HeightRequest = gridSize;
        grid.RowDefinitions.Clear();
        grid.ColumnDefinitions.Clear();
        grid.Clear();

        for (int i = 0; i < size; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                var btn = new Button
                {
                    Text = "",
                    FontSize = fontSize,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    BorderColor = Colors.Black,
                    BorderWidth = 1,
                    CornerRadius = 0
                };

                int row = r, col = c;
                btn.Clicked += (s, e) => OnCellClicked(row, col);

                buttons[r, c] = btn;
                grid.Add(btn, c, r);
            }
        }
    }

    void OnCellClicked(int row, int col)
    {
        if (buttons[row, col].Text != "") return;

        buttons[row, col].Text = currentPlayer;

        if (CheckWinner())
        {
            DisplayAlert("Mäng läbi!", $"{currentPlayer} võitis!", "Uus mäng");
            ResetGame();
            return;
        }

        if (IsBoardFull())
        {
            DisplayAlert("Mäng läbi!", "Viik!", "Uus mäng");
            ResetGame();
            return;
        }

        currentPlayer = currentPlayer == "X" ? "O" : "X";
    }

    bool CheckWinner()
    {
        for (int r = 0; r < boardSize; r++)
        {
            bool win = true;
            for (int c = 0; c < boardSize; c++)
                if (buttons[r, c].Text != currentPlayer) { win = false; break; }
            if (win) return true;
        }

        for (int c = 0; c < boardSize; c++)
        {
            bool win = true;
            for (int r = 0; r < boardSize; r++)
                if (buttons[r, c].Text != currentPlayer) { win = false; break; }
            if (win) return true;
        }

        bool diagWin = true;
        for (int i = 0; i < boardSize; i++)
            if (buttons[i, i].Text != currentPlayer) { diagWin = false; break; }
        if (diagWin) return true;

        bool antiDiagWin = true;
        for (int i = 0; i < boardSize; i++)
            if (buttons[i, boardSize - 1 - i].Text != currentPlayer) { antiDiagWin = false; break; }
        if (antiDiagWin) return true;

        return false;
    }

    bool IsBoardFull()
    {
        for (int r = 0; r < boardSize; r++)
            for (int c = 0; c < boardSize; c++)
                if (buttons[r, c].Text == "") return false;
        return true;
    }

    void ResetGame()
    {
        currentPlayer = "X";
        for (int r = 0; r < boardSize; r++)
            for (int c = 0; c < boardSize; c++)
                buttons[r, c].Text = "";
    }
}