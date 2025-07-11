using System.Collections.ObjectModel;
using System.Windows.Input;
using AnimeZone.Models;

namespace AnimeZone.Views;

public partial class HomePage : ContentPage
{
    public Services.AnimeService animeService { get; set; } = new Services.AnimeService();
    public ObservableCollection<Anime> Recommendeds { get; set; } = new ObservableCollection<Anime>();
    public ObservableCollection<Anime> Populars { get; set; } = new ObservableCollection<Anime>();
    public ICommand ItemTappedCommand { get; }
    public ICommand PopularTappedCommand { get; }

    public HomePage()
    {
        InitializeComponent();
        InitializeRecommended();
        InitializePopular();
        ItemTappedCommand = new Command<Anime>(OnItemTapped);
        PopularTappedCommand = new Command<Anime>(OnPopularTapped);
        BindingContext = this;
    }

    private async void OnItemTapped(Anime selectedAnime)
    {
        // เปิดหน้าใหม่ AnimeDetailPage โดยส่งข้อมูลที่ต้องการไปด้วย
        await Navigation.PushAsync(new AnimeDetailPage(selectedAnime));
    }


    private async void OnPopularTapped(Anime selectedAnime)
    {
        await Navigation.PushAsync(new AnimeDetailPage(selectedAnime));
    }

    private void InitializeRecommended()
    {
        string type = "Recommended"; // ประเภทของ Anime ที่ต้องการแสดง (Recommended)
        Recommendeds.Clear(); // ล้างข้อมูลเก่าทั้งหมดก่อน
        foreach (var anime in animeService.GetAnimeFromType(type))
        {
            Recommendeds.Add(anime); // เพิ่มข้อมูล Recommended จากฐานข้อมูลเข้า ObservableCollection
        }
    }

    private void InitializePopular()
    {
        string type = "Popular";
        Populars.Clear();
        foreach (var anime in animeService.GetAnimeFromType(type))
        {
            Populars.Add(anime);
        }
    }
}
