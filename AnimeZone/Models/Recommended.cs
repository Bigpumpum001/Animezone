namespace AnimeZone.Models;

public class Recommended
{
    public string Title { get; set; }
    public string Storyline { get; set; }
    public string Tag { get; set; }
    public string Image { get; set; }

    public override string ToString()
    {
        return Title;
    }
}
