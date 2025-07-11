using System.ComponentModel;
using AnimeZone.Models;

namespace AnimeZone.Views;

public partial class MangaDetailPage : ContentPage
{
    public MangaDetailPage(Models.Manga manga)
    {
        InitializeComponent();
        BindingContext = new MangaDetailViewModel(manga);

        MessagingCenter.Subscribe<MangaDetailPage, int>(this, "UpdateCartCount", (sender, count) =>
        {
            CountCart.Text = (count + 1).ToString(); // เพิ่ม 1 เข้าไปในค่า count ที่ได้รับ
        });
    }

    public static class GlobalVariables
    {
        public static int CountCart { get; set; }
        public static Customer2 LoggedOnUser { get; set; }
        public static double Amount { get; set; }
        public static int CountItem { get; internal set; }
    }


    public class MangaDetailViewModel : INotifyPropertyChanged
    {
        public Models.Manga selectedManga;

        public Models.Manga SelectedManga
        {
            get { return selectedManga; }
            set
            {
                Console.Write(value);
              
                selectedManga = value;
                OnPropertyChanged(nameof(SelectedManga));  
            }
        }

        public MangaDetailViewModel(Models.Manga manga)
        {
            SelectedManga = manga;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Models.Manga GetSelectedManga()
        {
            return SelectedManga;
        }

        internal object GetCountCartManga()
        {
            throw new NotImplementedException();
        }
    }

    //Add manga to Cart
    void OnAddToCart(System.Object sender, System.EventArgs e)
    {
        MessagingCenter.Send(this, "UpdateCartCount", GlobalVariables.CountCart);

        var selectedManga = (BindingContext as MangaDetailViewModel)?.GetSelectedManga(); // Get SelectedManga from ViewModel

        if (selectedManga != null)
        {
            // Check if SelectedManga is already in the Cart
            var existingManga = GlobalVariables.LoggedOnUser.CartItems.FirstOrDefault(m => m.Name == selectedManga.Name);
            if (existingManga != null)
            {
                // Increase the quantity if the item already exists in the Cart
                existingManga.Item++; ;
            }

            else
            {
                selectedManga.Item = 1; // Set the initial quantity to 1
                                        // Add a new item to the Cart if it's not already in the Cart
                GlobalVariables.LoggedOnUser.CartItems.Add(selectedManga.Clone());
            }

            // Update the total number of items and total price in the Cart
            GlobalVariables.CountCart = GlobalVariables.LoggedOnUser.CartItems.Sum(m => m.Item);
            // Convert double to int

            MessagingCenter.Send(this, "AddToCart", selectedManga); // Send SelectedManga data to CartPage
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CountCart.Text = GlobalVariables.CountCart.ToString();
    }

    async void OnCart(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CartPage());
    }

}