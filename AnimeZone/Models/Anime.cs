using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimeZone.Models;

public partial class Anime : ObservableObject
{
    public string Name { get; set; }
    public string Synopsis { get; set; }
    public string Tags { get; set; }
    public string ImgUrl { get; set; }
    public int Item { get; set; }
    public bool IsFavorite { get; internal set; }

    public override string ToString()
    {
        return Name;
    }

    public Anime Clone() => MemberwiseClone() as Anime;

}
