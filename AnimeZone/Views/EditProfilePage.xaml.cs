using System.Collections.ObjectModel;
using System.Diagnostics;
using AnimeZone.Models;
using Microsoft.Maui;
using static System.Net.Mime.MediaTypeNames;
using static AnimeZone.Views.LogInPage;
using Application = Microsoft.Maui.Controls.Application;
namespace AnimeZone.Views;

public partial class EditProfilePage : ContentPage
{
    public ObservableCollection<Customer2> Customers2 { get; set; }
    public static int lastId { get; set; }
    int nlastId = AppData.lastId;

    public EditProfilePage()
	{
        InitializeComponent();
        BindingContext = this;
        InitializeEditProfile();
        LoadItem(nlastId);
    }


    async void ChangeProfile_Clicked(object sender, EventArgs e)
    {
        // Check if the required fields are not blank or whitespace
        if (string.IsNullOrWhiteSpace(itemNameLabel.Text) || string.IsNullOrWhiteSpace(itemEmailLabel.Text) || string.IsNullOrWhiteSpace(itemPasswordLabel.Text) || string.IsNullOrWhiteSpace(itemConfirmPasswordLabel.Text))
        {
            await DisplayAlert("Invalid", "Blank or WhiteSpace value is Invalid!", "Ok");
        }

        else
        {
            var itemeiei = await App.Mydatabase.ReadLastLoggedInCustomer(nlastId);
            if (itemeiei != null && itemeiei.Name == itemNameLabel.Text && itemeiei.Email == itemEmailLabel.Text && itemeiei.Password == itemPasswordLabel.Text)
            {
                // If the data is the same as the loaded data, don't update
                await DisplayAlert("Info", "No changes detected", "Ok");
            }

            else if (itemPasswordLabel.Text == itemConfirmPasswordLabel.Text)
            {
                // Update the item
                EditItem(nlastId);

                // Display success message
                await DisplayAlert("Success", "Profile has been updated", "Ok");

                // Load updated item data
                LoadItem(nlastId);

                // Navigate back to ProfilePage
                await Navigation.PushAsync(new ProfilePage());
            }

            else
            {
                await DisplayAlert("Invalid", "Passwords do not match", "Ok");
            }
        }
    }





    private async void EditItem(int nlastId)
    {
        var itemeiei = await App.Mydatabase.ReadLastLoggedInCustomer(nlastId);
        itemeiei.Name = itemNameLabel.Text;
        itemeiei.Email = itemEmailLabel.Text;
        itemeiei.Password = itemPasswordLabel.Text;
        if (itemeiei.Id != 0)
        {
            App.Mydatabase.UpdateCustomer2(itemeiei);
        }
        else
        {
            App.Mydatabase.CreateCustomer2(itemeiei);
        }
    }
    private async void LoadItem(int nlastId)
    {
        var itemeiei = await App.Mydatabase.ReadLastLoggedInCustomer(nlastId);
        if (itemeiei != null)
        {
            itemNameLabel.Text = itemeiei.Name; // 'itemNameLabel' ??? Name ??? Label ?? XAML
            itemEmailLabel.Text = itemeiei.Email;
            itemPasswordLabel.Text = itemeiei.Password;
        }
        else
        {

        }
    }

    private void Button_Back_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new ProfilePage());

    }
    public async Task InitializeEditProfile()
    {
       
        try
        {
            //--var customers = new ObservableCollection<Customer2>();
            base.OnAppearing();

        }
        catch (Exception ex)
        {
            Debug.WriteLine("An error occurred: " + ex.Message);

        }
    }
}