using System.Collections.ObjectModel;
using System.Diagnostics;
using AnimeZone.Models;
using static AnimeZone.Views.LogInPage;

namespace AnimeZone.Views;


public partial class ProfilePage : ContentPage
{
    public ObservableCollection<Customer2> Customers2 { get; set; }
    public static int lastId { get; set; }
    int nlastId = AppData.lastId;

    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = this;
        InitializeProfile();
    }

    async void OnEditClicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new EditProfilePage());
    }
   
    private async void LoadItem(int nlastId)
    {
        var itemeiei = await App.Mydatabase.ReadLastLoggedInCustomer(nlastId);

        if (itemeiei != null)
        {
            Debug.WriteLine($"Item ID: {itemeiei.Id}, Item Name: {itemeiei.Name}");
            itemNameLabel.Text = itemeiei.Name; // 'itemNameLabel' คือ Name ของ Label ใน XAML
            itemEmailLabel.Text = itemeiei.Email;
            //itemPasswordLabel.Text = itemeiei.Password;
            Debug.WriteLine("Update" + itemeiei.Name);
        }

        else
        {
            Debug.WriteLine("Item not found");
            Debug.WriteLine("itemeiei" + nlastId);
            Debug.WriteLine(itemeiei);
        }

    }

    public async Task InitializeProfile()
    {
        Debug.WriteLine("An error occurred Profile : $" + lastId); //เอาไว้เช็คค่า Name ตอนล้อคอินที่output
        Debug.WriteLine("An error occurred READDB : $" + App.Mydatabase.ReadCustomers());
        Debug.WriteLine("kuyukyukyuk");
        Debug.WriteLine("kuyukyukyuk");

        try
        {
            base.OnAppearing();
            LoadItem(nlastId);  
        }

        catch (Exception ex)
        {
            Debug.WriteLine("An error occurred: " + ex.Message);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadItem(nlastId);
    }
}


