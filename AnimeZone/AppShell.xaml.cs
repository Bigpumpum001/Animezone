using System.Windows.Input;
using AnimeZone.Views;

namespace AnimeZone;

public partial class AppShell : Shell
{
    public ICommand CommunityCommand { get; set; }
    public ICommand HelpCommand { get; set; }

    public AppShell()
    {
        InitializeComponent();
        CommunityCommand = new Command<string>(ActionCommamd);
        HelpCommand = new Command<string>(ActionCommamd);
        this.BindingContext = this;

    }

    public async void ActionCommamd(string pUrl)
    {
        await Launcher.Default.OpenAsync(pUrl);
    }

 
    async void OnLogout(System.Object sender, System.EventArgs e)
    {
        bool logoutConfirmed = await DisplayAlert("Log out", "Do you want to log out?", "Ok", "Cancel");
        if (logoutConfirmed)
        {
            Application.Current.MainPage = new NavigationPage(new LogInPage());
        }
    }


}

