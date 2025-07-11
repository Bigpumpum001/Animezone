using System.Windows.Input;
using AnimeZone.Models;

namespace AnimeZone.Views;

public partial class AnimePage : ContentPage
{
    public Services.AnimeService animeService { get; set; } = new Services.AnimeService();
    public ICommand ItemTappedCommand { get; }

    public AnimePage()
    {
        InitializeComponent();

        // สร้าง Command และกำหนดการทำงานเมื่อถูกเรียกใช้
        ItemTappedCommand = new Command<Anime>(async (item) =>
        {
            // ตัวอย่างการเปิดหน้าใหม่เพื่อแสดงข้อมูลของ Anime
            await Navigation.PushAsync(new AnimeDetailPage(item));
        });

        // กำหนด BindingContext ของหน้า XAML เพื่อใช้ Command ใน XAML
        BindingContext = this;
    }

    void OnPickerIndexChanged(Object sender, EventArgs e)
    {
        int x = pickerGenre.SelectedIndex;

        Picker picker = sender as Picker;
        int y = picker.SelectedIndex;

        if (picker.SelectedIndex >= 0)
        {
            string animeType = picker.ItemsSource[picker.SelectedIndex].ToString();
            List<Anime> animes = animeService.GetAnimeFromType(animeType);

            myCollection.ItemsSource = animes;
        }
    }
}
