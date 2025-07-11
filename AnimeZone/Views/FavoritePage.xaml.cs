using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using AnimeZone.Models;
using static AnimeZone.Views.MangaDetailPage;

namespace AnimeZone.Views
{
    public partial class FavoritePage : ContentPage
    {
        public ObservableCollection<Anime> FavoriteItems { get; }
        public ICommand ItemTappedCommand { get; }
        public ICommand DeleteCommand { get; }


        public FavoritePage()
        {
            InitializeComponent();

            FavoriteItems = new ObservableCollection<Anime>(GlobalVariables.LoggedOnUser.FavoriteItems);
            BindingContext = this;

            //MessagingCenter.Subscribe<AnimeDetailPage, Anime>(this, "AddToFavorite", OnAddToFavorite);

            MessagingCenter.Subscribe<AnimeDetailPage, ObservableCollection<Anime>>(this, "UpdateFavoriteItems", (sender, updatedItems) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    FavoriteItems.Clear(); // ล้างข้อมูลเดิม
                    foreach (var item in updatedItems)
                    {
                        FavoriteItems.Add(item); // เพิ่มข้อมูลใหม่ล่าสุด
                    }
                });
            });

            ItemTappedCommand = new Command<Anime>(async (item) =>
            {
                await Navigation.PushAsync(new AnimeDetailPage(item));
            });

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnDeleteFavorite(System.Object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var anime = button.BindingContext as Anime; // Get the Manga object associated with the button

            foreach (var anime2 in FavoriteItems)
            {
                Debug.WriteLine($"Anime Name: {anime2.Name}, IsFavorite: {anime2.IsFavorite}");
            }

            // Check if manga is not null and exists in CartItems
            if (anime != null && FavoriteItems.Contains(anime))
            {
                Debug.WriteLine(anime);
                FavoriteItems.Remove(anime); // Remove manga from CartItems

                // Optionally, if you want to remove manga from GlobalVariables.LoggedOnUser.CartItems
                if (GlobalVariables.LoggedOnUser.FavoriteItems.Contains(anime))
                {
                    GlobalVariables.LoggedOnUser.FavoriteItems.Remove(anime);
                }

                // Update the count in the cart globally
                GlobalVariables.CountCart -= anime.Item;
            }
        }

            void OnDeleteAll(System.Object sender, System.EventArgs e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (FavoriteItems.Count == 0)
                    {
                        await DisplayAlert("No anime", "There are no anime to delete.", "Ok");
                        return; // Exit the method if CartItems is empty
                    }

                    bool answer = await DisplayAlert("Delete all animes.", "Do you want to delete all anime ?", "Ok", "Cancel");
                    if (answer)
                    {
                        FavoriteItems.Clear();
                        GlobalVariables.LoggedOnUser.FavoriteItems.Clear();
                        GlobalVariables.CountCart = 0;
                    }
                });
            }
        }
}

