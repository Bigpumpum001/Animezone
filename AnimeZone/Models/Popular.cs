namespace AnimeZone.Models;

public class Popular
{
    public string Name { get; set; }
    public string Synopsis { get; set; }
    public string Tags { get; set; }
    public string ImgUrl { get; set; }

    public override string ToString()
    {
        return Name;
    }
}