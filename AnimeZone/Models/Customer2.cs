using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeZone.Models
{
    public class Customer2
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        [Ignore]
        public List<Manga> CartItems { get; set; }

        [Ignore]
        public List<Anime> FavoriteItems { get; set; }


        [NotMapped]
        public string ConfirmPassword { get; set; }

        public Customer2()
        {
            CartItems = new List<Manga>();
            FavoriteItems = new List<Anime>();
        }


    }
}

