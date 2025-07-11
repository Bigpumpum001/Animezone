//using CloudKit;
using AnimeZone.Views;

namespace AnimeZone;

public partial class App : Application
{
    private static SQLiteHelper db;

    public static SQLiteHelper Mydatabase
    {
        get
        {
            if (db == null)
            {
                db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mycustomer.db3"));
            }
            return db;
        }
    }
  
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new LogInPage());  
    }
}

