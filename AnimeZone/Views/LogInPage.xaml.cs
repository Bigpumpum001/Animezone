using AnimeZone.Table;
using SQLite;
using AnimeZone.Models;
using System.Diagnostics;
using static AnimeZone.Views.MangaDetailPage;
namespace AnimeZone.Views;

public partial class LogInPage : ContentPage

{
    private static SQLiteHelper db;
  
    public LogInPage()
    {
        InitializeComponent();
        //  _dbService = dbService;
    }
    Models.Customer2 _customer2;
  
    public LogInPage(Models.Customer2 customer2)
    {
        InitializeComponent();
        _customer2 = customer2;

        YourName.Text = customer2.Name;
        YourPassword.Text = customer2.Password;
    }

    public static class AppData
    {
        public static int lastId { get; private set; }
        public static string lastName { get; private set; }

        public static void SetLastLogin(int userid)
        {
            lastId = userid;
        }
        public static void SetLastLoginName(string userid)
        {
            lastName = userid;
        }

    }

    async void OnButtonLogin(System.Object sender, System.EventArgs e)
    {
        var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mycustomer.db3");
        var db = new SQLiteConnection(dbpath);
        var myquery = db.Table<Customer2>().Where(u => u.Name.Equals(YourName.Text) && u.Password.Equals(YourPassword.Text));

        if (myquery != null)
        {
         
            var user = await App.Mydatabase.LoginAsync(YourName.Text, YourPassword.Text);
            if (user != null)
            {
                GlobalVariables.LoggedOnUser = user;
                AppData.SetLastLoginName(user.Name);
                AppData.SetLastLogin(user.Id);
                PasswordErrorLabel.Text = "Success";
                PasswordSuccessLabel.Text = string.Empty;
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                PasswordErrorLabel.Text = string.Empty;
                PasswordSuccessLabel.Text = "Error";
                DisplayAlert("Login Failed", "Invalid username or password.", "OK");
            }
         
            Debug.WriteLine("An error occurred Login : เลข" + AppData.lastId);  
        }

        else
        {
            PasswordErrorLabel.Text = "Please fill in complete information";
            PasswordSuccessLabel.Text = string.Empty;
        }
    }

    void OnSignUpClicked(System.Object sender, System.EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new SignUpPage());
    }

}

