using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using AnimeZone.Models;
using SQLite;

//using static Android.Content.ClipData;
using static AnimeZone.Views.MangaDetailPage;

namespace AnimeZone.Views
{
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<Manga> CartItems { get; set; }
        public MangaDetailViewModel ViewModel { get; set; }

        public decimal ptotalitem { get; set; }




        public CartPage()
        {
            InitializeComponent();
            CartItems = new ObservableCollection<Manga>(GlobalVariables.LoggedOnUser.CartItems);
            CartItemsumm();
          
            BindingContext = this;


        }

        public void CartItemsumm()
        {
            decimal ptotalItem = 0;
            foreach (var item in GlobalVariables.LoggedOnUser.CartItems)
            {
                Debug.WriteLine($" Item Name: {item.Name}, Price: {item.Price}");
                ptotalItem += (decimal)item.TotalPrice;
                Debug.WriteLine($"Total Price : {ptotalItem}");

            }
            
            totalLabel.Text = $"Total      THB {ptotalItem}";
          
        }

        void OnDeleteAll(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (CartItems.Count == 0)
                {
                    await DisplayAlert("No products", "There are no products to delete.", "Ok");
                    return; // Exit the method if CartItems is empty
                }

                bool answer = await DisplayAlert("Delete all products.", "Do you want to delete all products?", "Ok", "Cancel");
                if (answer)
                {
                    CartItems.Clear();
                    GlobalVariables.LoggedOnUser.CartItems.Clear();
                    GlobalVariables.CountCart = 0;
                    CartItemsumm();
                }
            });
           

        }


        void OnDelete(System.Object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var manga = button.BindingContext as Manga; // Get the Manga object associated with the button

            // Check if manga is not null and exists in CartItems
            if (manga != null && CartItems.Contains(manga))
            {
                CartItems.Remove(manga); // Remove manga from CartItems

                // Optionally, if you want to remove manga from GlobalVariables.LoggedOnUser.CartItems
                if (GlobalVariables.LoggedOnUser.CartItems.Contains(manga))
                {
                    GlobalVariables.LoggedOnUser.CartItems.Remove(manga);
                }

                // Update the count in the cart globally
                GlobalVariables.CountCart -= manga.Item;
            }
            CartItemsumm();
        }

        async void OnBuying(System.Object sender, System.EventArgs e)
        {
            if (CartItems.Count > 0)
            {
                await Navigation.PushAsync(new PaymentPage());
            }
            else
            {
                // Display alert if cart is empty
                await DisplayAlert("Error", "Cart is empty.", "OK");
            }
        }


        private void OnAddItem(object sender, EventArgs e)
        {
            var button = sender as Button;
            var manga = button.BindingContext as Manga; // Get the Manga object associated with the button

            // Check if manga is not null and exists in CartItems
            if (manga != null && CartItems.Contains(manga))
            {
                manga.Item++; // Increase the quantity of the manga

                // Optionally, update the count in the cart globally
                GlobalVariables.CountCart++;
                OnPropertyChanged(nameof(CartItems));

            }
            CartItemsumm();
        }



        private void OnDeleteItem(object sender, EventArgs e)
        {
            var button = sender as Button;
            var manga = button.BindingContext as Manga; // Get the Manga object associated with the button

            // Check if manga is not null and exists in CartItems
            if (manga != null && CartItems.Contains(manga))
            {
                if (manga.Item > 1) // Ensure the quantity is more than 1 before decrementing
                {
                    manga.Item--; // Decrease the quantity of the manga
                    GlobalVariables.CountCart--; // Decrease the count in the cart globally

                    OnPropertyChanged(nameof(CartItems));
                }
                else
                {
                    // Remove the manga from CartItems if quantity becomes 0
                    OnDelete(sender, e);
                    CartItems.Remove(manga);
                    GlobalVariables.CountCart--; // Decrease the count in the cart globally

                    if (CartItems.Count == 0) // Check if CartItems is empty after removing the manga
                    {
                        GlobalVariables.CountCart = 0; // Set count to 0 in GlobalVariables if CartItems is empty
                    }

                    OnPropertyChanged(nameof(CartItems));
                    
                }
            }
            CartItemsumm();
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            CartItemsumm();
        }
    }
}