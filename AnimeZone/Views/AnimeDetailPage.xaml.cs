using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using AnimeZone.Models;
using static AnimeZone.Views.MangaDetailPage;

namespace AnimeZone.Views;

public partial class AnimeDetailPage : ContentPage
{
    private object CountCart;

    public bool IsFavorite { get; set; }
    public bool isFavorite { get; set; }

    public ObservableCollection<Anime> FavoriteItems { get; }

    public AnimeDetailPage(Models.Anime anime)
    {
        InitializeComponent();
        FavoriteItems = new ObservableCollection<Anime>(GlobalVariables.LoggedOnUser.FavoriteItems);
        BindingContext = new AnimeDetailViewModel(anime);
        SetFavoriteIcon();

        MessagingCenter.Subscribe<MangaDetailPage, int>(this, "UpdateCartCount", (sender, count) =>
        {
            CountCart = (count + 1).ToString(); // เพิ่ม 1 เข้าไปในค่า count ที่ได้รับ
        });
    }

    public class AnimeDetailViewModel : INotifyPropertyChanged
    {
        private Models.Anime selectedAnime;

        public Models.Anime SelectedAnime
        {
            get { return selectedAnime; }
            set
            {
                if (selectedAnime != value)
                {
                    selectedAnime = value;
                    OnPropertyChanged(nameof(SelectedAnime));
                }
            }
        }

        public AnimeDetailViewModel(Models.Anime anime)
        {
            SelectedAnime = anime;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Models.Anime GetSelectedAnime()
        {
            return SelectedAnime;
        }

        // เพิ่ม Property IsClicked
        private bool isClicked;

        public bool IsClicked
        {
            get { return isClicked; }
            set
            {
                if (isClicked != value)
                {
                    isClicked = value;
                    OnPropertyChanged(nameof(IsClicked));
                }
            }
        }

    }

    // เมื่อปุ่มถูกคลิก
    void SetFavoriteIcon()
    {
        var selectedAnime = (BindingContext as AnimeDetailViewModel)?.SelectedAnime ?? new Anime();
        if (BindingContext is AnimeDetailViewModel viewModel && viewModel.SelectedAnime != null)
        {

        }

        if (selectedAnime.IsFavorite == true)
        {
            imgx.Source = "blackheart.png";
            Debug.WriteLine("selectedAnime is a favorite");
        }
        else
        {
            // selectedAnime is found in FavoriteItems
            imgx.Source = "whiteheart.png";
            Debug.WriteLine("selectedAnime is not in a favorite");

            // เพิ่มโค้ดเพื่อลบอนิเมะออกจากรายการชื่นชอบ เมื่อภาพถูกเปลี่ยนเป็น blackheart.png
            var existingAnime = GlobalVariables.LoggedOnUser.FavoriteItems.FirstOrDefault(m => m.Name == selectedAnime.Name);
            if (imgx.Source.ToString() == "blackheart.png" && existingAnime != null)
            {
                GlobalVariables.LoggedOnUser.FavoriteItems.Remove(existingAnime);
                FavoriteItems.Remove(existingAnime);
            }
        }
    }


    public void OnFavoriteClicked(object sender, EventArgs e)
    {
        var selectedAnime = (BindingContext as AnimeDetailViewModel)?.SelectedAnime ?? new Anime(); // กำหนดค่า default เป็น new Anime() ถ้า SelectedAnime เป็น null

        if (selectedAnime != null)
        {
            var existingAnime = GlobalVariables.LoggedOnUser.FavoriteItems.FirstOrDefault(m => m.Name == selectedAnime.Name);
            Debug.WriteLine(existingAnime);
            Debug.WriteLine(selectedAnime);
            foreach (var anime in FavoriteItems)
            {
                Debug.WriteLine($"Anime Name: {anime.Name}, IsFavorite: {anime.IsFavorite}");
            }

            if (selectedAnime.IsFavorite)
            {
                Debug.WriteLine("Remove");
                if (existingAnime != null && FavoriteItems.Contains(existingAnime))
                {
                    FavoriteItems.Remove(existingAnime);
                    GlobalVariables.LoggedOnUser.FavoriteItems.Remove(existingAnime);
                }

                selectedAnime.Item = 0; // ตั้งค่า Item เป็น 0 สำหรับลบรายการ
                selectedAnime.IsFavorite = false; // อัปเดตสถานะเป็นไม่ใช่ Favorite
                imgx.Source = "whiteheart.png"; // อัปเดตรูปภาพ
                isFavorite = false;
            }

            else
            {
                Debug.WriteLine("Add");
                selectedAnime.Item = 1; // ตั้งค่า Item เป็น 1 สำหรับเพิ่มรายการ
                selectedAnime.IsFavorite = true; // อัปเดตสถานะเป็น Favorite

                if (!FavoriteItems.Contains(existingAnime))
                {
                    FavoriteItems.Add(selectedAnime);
                    GlobalVariables.LoggedOnUser.FavoriteItems.Add(selectedAnime); // เพิ่มอนิเมะใหม่เข้าไปในรายการชื่นชอบ

                }

                imgx.Source = "blackheart.png"; // อัปเดตรูปภาพ
                isFavorite = true;
            }
        }
        MessagingCenter.Send(this, "AddToFavorite", selectedAnime);

        var favoriteItemsToSend = new ObservableCollection<Anime>(FavoriteItems.Where(item => item.IsFavorite));

        // ส่งข้อมูลไปยัง FavoritePage
        MessagingCenter.Send(this, "UpdateFavoriteItems", favoriteItemsToSend);

        foreach (var anime in FavoriteItems)
        {
            Debug.WriteLine($"Anime Name: {anime.Name}, IsFavorite: {anime.IsFavorite}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}

