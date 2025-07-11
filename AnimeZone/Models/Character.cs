namespace AnimeZone.Models;

public class Character : ContentPage
{
    public string Name { get; set; }
    public string Anime { get; set; }
    public string Birth { get; set; }
    public string Gender { get; set; }
    public string H { get; set; }
    public string W { get; set; }
    public string Affiliations { get; set; }
    public string Biography { get; set; }
    public string Img { get; set; }
    public string Img1 { get; set; }
    public string Img2 { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
