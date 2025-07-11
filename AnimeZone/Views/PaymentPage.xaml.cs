using System.Diagnostics;
using System.Windows.Input;
using static AnimeZone.Views.MangaDetailPage;
namespace AnimeZone.Views;

public partial class PaymentPage : ContentPage
{
    public ICommand LineCommand { get; set; }
    public decimal ptotalitem { get; set; }
    public static decimal totalpriceshiping { get; set; }
    public static decimal shipping { get; set; }

    public PaymentPage()
    {
        InitializeComponent();
        LineCommand = new Command<string>(ActionCommand);
        this.BindingContext = this;
        CartItemsumm();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CartItemsumm();
    }

    public async void ActionCommand(string pUrl)
    {
        await Launcher.Default.OpenAsync(pUrl);
    }

    public void CartItemsumm()
    {
        decimal ptotalItem = 0;
        foreach (var item in GlobalVariables.LoggedOnUser.CartItems)
        {
            Debug.WriteLine($" Item Name : {item.Name}, Price: {item.Price}");
            Debug.WriteLine($" Item Name : {item.Name}");

            ptotalItem += (decimal)item.TotalPrice;


            Debug.WriteLine($"Total Price : {ptotalItem}");



        }

        decimal shipping = 40;
        decimal totalpriceshiping = ptotalItem + shipping;

        amountPaidLabel.Text =    $"Amount Paid          THB {totalpriceshiping}";
        totalLabel.Text =         $"Total                        THB {ptotalItem}";
        shippingLabel.Text =      $"Shipping cost                     THB {shipping}";
        priceshippingLabel.Text = $"Amount paid          THB {totalpriceshiping}";
    }
}
