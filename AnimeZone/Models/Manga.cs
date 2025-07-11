using CommunityToolkit.Mvvm.ComponentModel;
using static Android.Content.ClipData;

namespace AnimeZone.Models
{
    public partial class Manga : ObservableObject
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        private int _item;

        public int Item
        {
            get => _item;
            set
            {
                SetProperty(ref _item, value);
                OnPropertyChanged(nameof(TotalPrice)); 
            }
        }

        [ObservableProperty
        ,NotifyPropertyChangedFor(nameof(TotalPrice))]
       
        private int _cartQuantity;

        // ราคารวมแต่ละรายการ
        public double TotalPrice => Item * Price;

        public Manga Clone() => MemberwiseClone() as Manga;


    }
}