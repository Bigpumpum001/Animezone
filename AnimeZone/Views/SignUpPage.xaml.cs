using AnimeZone.Models;
using SQLite;
using System.Diagnostics;

namespace AnimeZone.Views;

public partial class SignUpPage : ContentPage

{
    private static SQLiteHelper db;

    public SignUpPage()
    {
        InitializeComponent();
    }

    public static class AppDataSign
    {

        public static string namecheck { get; private set; }

        public static void SetLastRegis(string regname)
        {
            namecheck = regname;
        }
    }
 
    async void OnButtonChange(System.Object sender, System.EventArgs e)
    {
        AppDataSign.SetLastRegis(YourName.Text);
        Debug.WriteLine("name" + YourName.Text);
        Debug.WriteLine("namecheck" + AppDataSign.namecheck);

        var regeiei = await App.Mydatabase.RegisterChecked(AppDataSign.namecheck);

        if (regeiei != null)
        {
            Debug.WriteLine("nameref" + regeiei.Name);
        }
        Debug.WriteLine("name" + YourName.Text);

        if (YourName.Text == "" | YourEmail.Text == "" | Password.Text == "" | ConfirmPassword.Text == "")
        {
            PasswordErrorLabel.Text = "Please fill in complete information";
            PasswordSuccessLabel.Text = string.Empty;
        }


        // check existingUser
        else if (regeiei != null)
        {
            PasswordErrorLabel.Text = "Already have this Name!!";
        }

        else if (Password.Text == ConfirmPassword.Text && YourEmail.Text != "" && YourName.Text != "")
        {
            AddNewCustomer();
            PasswordSuccessLabel.Text = "Welcome " + YourName.Text + "!";
            PasswordErrorLabel.Text = string.Empty;
            await DisplayAlert("Successfully", "Welcome!", "Ok");
            Application.Current.MainPage = new NavigationPage(new LogInPage());
        }

        else
        {
            PasswordErrorLabel.Text = "Password and confirm password not matched!!";
            PasswordSuccessLabel.Text = string.Empty;
        }

    }
 
    async void OnLogInClicked(Object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LogInPage());
    }

    async void AddNewCustomer()
    {
        await App.Mydatabase.CreateCustomer2(new Models.Customer2
        {
            Name = YourName.Text,
            Email = YourEmail.Text,
            Password = Password.Text,
            ConfirmPassword = ConfirmPassword.Text,
            CartItems = new List<Manga>(),
            FavoriteItems = new List<Anime>(),
        });
        await Navigation.PopAsync();
    }
}



