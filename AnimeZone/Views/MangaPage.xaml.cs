using System.Collections.ObjectModel;
using System.Windows.Input;
using AnimeZone.Models;
using static AnimeZone.Views.MangaDetailPage;

namespace AnimeZone.Views;

public partial class MangaPage : ContentPage
{
    public ObservableCollection<Manga> Mangas { get; set; }
    public ICommand ItemTappedCommand { get; }

    public MangaPage()
    {
        InitializeComponent();
        InitializeManga();
        BindingContext = this ;

        MessagingCenter.Subscribe<MangaDetailPage, int>(this, "UpdateCartCount", (sender, count) =>
        {
            CountCart.Text = (count + 1).ToString(); // เพิ่ม 1 เข้าไปในค่า count ที่ได้รับ
        });

        ItemTappedCommand = new Command<Manga>(async (item) =>
        {
            await Navigation.PushAsync(new MangaDetailPage(item));
        });

        BindingContext = this;
    }

    public void InitializeManga()
    {
        Mangas = new ObservableCollection<Manga>
        {
            new Manga() { Name = "Demon Slayer Complete Box Set", Price = 6910.54,Description = "   In Taisho-era Japan, kindhearted Tanjiro Kamado makes a living selling charcoal. But his peaceful life is shattered when a demon slaughters his entire family. His little sister Nezuko is the only survivor, but she has been transformed into a demon herself! Tanjiro sets out on a dangerous journey to find a way to return his sister to normal and destroy the demon who ruined his life." , Img = "https://mangamate.shop/cdn/shop/products/demon-slayer-complete-box-set-9781974725953_xlg.jpg?v=1629430617&width=1800" },
            new Manga() { Name = "One Piece Box Set", Price = 6013.35,Description="   As a child, Monkey D. Luffy dreamed of becoming King of the Pirates. But his life changed when he accidentally gained the power to stretch like rubber…at the cost of never being able to swim again! Years, later, Luffy sets off in search of the \"One Piece,\" said to be the greatest treasure in the world.", Img = "https://mangamate.shop/cdn/shop/products/one-piece-box-set-4-dressrosa-to-reverie-9781974725960_xlg.jpg?v=1643226943&width=1800" },
            new Manga() { Name = "Bleach Box Set 01", Price = 6377.08,Description="   Ichigo Kurosaki never asked for the ability to see ghosts—he was born with the gift. When his family is attacked by a Hollow—a malevolent lost soul—Ichigo becomes a Soul Reaper, dedicating his life to protecting the innocent and helping the tortured spirits themselves find peace. Find out why Tite Kubo’s Bleach has become an international manga smash-hit!",  Img = "https://mangamate.shop/cdn/shop/products/bleach-box-set-_vol-s-1-21_-9781421526102_hr.jpg?v=1626991997&width=1800" },
            new Manga() { Name = "Naruto Box Set 02", Price = 6546.81 ,Description="   Naruto is a young shinobi with an incorrigible knack for mischief. He’s got a wild sense of humor, but Naruto is completely serious about his mission to be the world’s greatest ninja!",  Img = "https://mangamate.shop/cdn/shop/products/naruto-box-set-2-9781421580807_hr.jpg?v=1626991802&width=1800" },
            new Manga() { Name = "Fairy Tail Box Set 03", Price = 3928.23 ,Description="   Lucy is a young, rebellious celestial wizard with a dream: to join Fairy Tail, the world's most rambunctious and powerful magical guild! When she happens to meet one of Fairy Tail's top wizards, he turns out to be not quite what she expected: a slob traveling with a flying cat. But the promise of adventure is real, and together they escape from pirates and a devious magician! Their next task: to steal a book from the evil wizard-killing Duke Everlue, and outsmart his death trap. Eccentric new friends join along the way in this lushly-drawn modern classic!",  Img = "https://mangamate.shop/cdn/shop/products/9781646510290.jpg?v=1629439874&width=1800" },
            new Manga() { Name = "Tokyo Ghoul : Re Complete Box Set", Price = 6013.35 ,Description="   Ken Kaneki is an ordinary college student until a violent encounter turns him into the first half-human, half-Ghoul hybrid. Trapped between two worlds, he must survive Ghoul turf wars, learn more about Ghoul society and master his new powers.", Img = "https://mangamate.shop/cdn/shop/products/tokyo-ghoul-re-complete-box-set-9781974718474_xlg.jpg?v=1626991665&width=1800" },
            new Manga() { Name = "My Hero Academia Box Set 01", Price = 6013.35,Description="   Collect the first 20 volumes of the bestselling My Hero Academia manga in this heroic box set! Also includes a full-color double-sided poster and an exclusive 48-page booklet featuring the never-before-seen bonus illustrations and author commentaty printed on volume 1–20’s book covers in Japan!",  Img = "https://mangamate.shop/cdn/shop/products/my-hero-academia-box-set-1-9781974735990_xlg.jpg?v=1664258786&width=1800" },
            new Manga() { Name = "Assassination Classroom Complete Box Set", Price = 6377 ,Description="   The complete bestselling Assassination Classroom series is now available in a boldly designed, value-priced box set! Includes 21 volumes of this unique tale of a mysterious, smiley-faced, tentacled, superpowered teacher who guides a group of misfit students to find themselves—while doing their best to assassinate him.",  Img = "https://mangamate.shop/cdn/shop/products/assassination-classroom-complete-box-set-9781974710140_xlg.jpg?v=1626991629&width=1800" },

        };
    }

    async void OnCart(System.Object sender, System.EventArgs e)
    {
       await Navigation.PushAsync(new CartPage());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CountCart.Text = GlobalVariables.CountCart.ToString();
    }
}