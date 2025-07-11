namespace AnimeZone.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AnimeZone.Models;

public partial class CharacterPage : ContentPage
{
    public ObservableCollection<Character> Characters { get; set; }
    public ICommand ItemTappedCommand { get; }

    public CharacterPage()
    {
        InitializeComponent();
        InitializeCharacter();
        BindingContext = this;

        ItemTappedCommand = new Command<Character>(async (item) =>
        {
            // ตัวอย่างการเปิดหน้าใหม่เพื่อแสดงข้อมูลของ Anime
            await Navigation.PushAsync(new CharacterDetailPage(item));
        });

        BindingContext = this;
    }

    public void InitializeCharacter()
    {
        Characters = new ObservableCollection<Character>
        {
            new Character(){ Name = "Levi", Anime = "Attack on Titan", Birth = "December 25", Gender = "Male", H = "160 cm", W = "65 kg", Img = "https://www.take-up.co.jp/user_data/packages/default/client/lp/2023/shingeki/img/main.jpg", Img1 = "https://i.pinimg.com/564x/51/14/4f/51144f21016b84109f7d871f0653daea.jpg" ,Img2 = "https://i.pinimg.com/736x/d8/d6/44/d8d6440165db6706c10d8a71a0633800.jpg", Biography = "Levi is known as humanity's most powerful soldier. He's ranked as Captain of the Scouting Legion division. Levi is also the leader of the Special Operations Squad, an elite team that he hand-picked in order to protect Eren Yeager. While it is said that he is blunt and unapproachable, it is noted that he has a strong respect for structure and discipline. There are rumors that he was originally part of underground crime before he became a soldier. Although he often appears to be unfriendly, he cares deeply for his team and has never undervalued human life. He is also a notorious clean-freak."},
            new Character(){ Name = "Naruto Uzumaki", Anime = "Naruto", Birth = "October 10", Gender = "Male", H = "180 cm", W = "50.9 kg", Img = "https://i.redd.it/today-is-october-10-happy-birthday-naruto-uzumaki-btw-v0-7yhkko0e9btb1.jpg?width=735&format=pjpg&auto=webp&s=41fe09bf9e7631313b25d4b3d540b79da3d7b214", Img1 = "https://www.lifeisnerd.it/wp-content/uploads/2020/08/naruto-shippuden-e-boruto-1200x630-1-752x440.jpg" ,Img2 = "https://images.immediate.co.uk/production/volatile/sites/3/2023/04/naruto-762b09d.jpg?resize=768,574", Biography = "Naruto is a young ninja with a single dream: that he will one day become the greatest shinobi in the village. When he was young the Fourth Hokage sealed the nine-tailed demon fox within Naruto, and as a result he holds within him incredible power. Though he is not naturally talented or particularly intelligent, Naruto has the determination to succeed and works harder than anyone to achieve his goal. He has a particular like for eating ramen and utilising the Sexy technique jutsu on any unwilling male, but despite his laid-back and goofy attitude he cares deeply about his friends and would do anything to protect them." },
            new Character(){ Name = "Satoru Gojo", Anime = "Jujutsu Kaisen", Birth = "December 7", Gender = "Male", H = "190.5 cm", W = "84 kg", Img = "https://i.pinimg.com/564x/24/5b/ad/245badbfd9d4a0a5ae3052c1e0b10186.jpg", Img1 = "https://images4.alphacoders.com/133/1332281.jpeg" ,Img2 = "https://static.wikia.nocookie.net/jujutsu-kaisen/images/5/5a/Satoru_Gojo_arrives_on_the_battlefield_%28Anime%29.png/revision/latest?cb=20210226205256", Biography = "is one of the main protagonists of the Jujutsu Kaisen series. He is a special grade jujutsu sorcerer and widely recognized as the strongest in the world. Satoru is the pride of the Gojo Clan, the first person to inherit both the Limitless and the Six Eyes in four hundred years. He works as a teacher at the Tokyo Jujutsu High and uses his influence to protect and train strong young allies." },
            new Character(){ Name = "Nezuko Kamado", Anime = "Demon Slayer", Birth = "December 28", Gender = "Female", H = "153 cm", W = "45 kg", Img = "https://staticg.sportskeeda.com/editor/2022/02/40498-16437334600644-1920.jpg?w=840", Img1 = "https://i.pinimg.com/736x/72/b7/56/72b756ee900c26b3e616ba439e1f05c2.jpg" ,Img2 = "https://i.pinimg.com/564x/8d/43/a4/8d43a4b72703debf0d9e0c73194dfa30.jpg", Biography = "Nezuko Kamado is the younger sister of Tanjiro Kamado, supposedly turned into a demon by Muzan Kibutsuji. She debuts from the start of the Kimetsu no Yaiba (Demon Slayer) manga and anime series as one of the main protagonists. Growing up on the mountain alongside, she is one year younger than Tanjiro in age taking on the role as the eldest of the females in her family. These responsibilities include taking care of the family, as well as helping with chores around the home." },
            new Character(){ Name = "Killua Zoldyck", Anime = "Hunter x Hunter", Birth = "July 7", Gender = "Male", H = "171 cm", W = "55 kg", Img = "https://i.pinimg.com/564x/15/c6/5f/15c65f3a965dda6f2380cf610272a7b4.jpg", Img1 = "https://i.pinimg.com/564x/50/a7/3d/50a73dbe0adcffc6ef7320507e592537.jpg" ,Img2 = "https://i.pinimg.com/564x/bb/99/ba/bb99ba6317e5d10df9c77bc52afcee1c.jpg", Biography = "Killua was born as the middle child of a family of famous assassins, the Zoldycks. He was quickly recognized as a prodigy and managed to master many killing techniques at a young age, and then set to become one of the best assassins the family ever produced. Killua followed the traditional Zoldyck training since birth, being supervised by his father Silva and his older brother Illumi. At the tender age of three, he began a training that routinely put his life at risk." },
            new Character(){ Name = "Maomao", Anime = "Kusuriya no Hitorigoto", Birth = "March 14", Gender = "Female", H = "153 cm", W = "43 kg", Img = "https://i.pinimg.com/736x/50/c5/e8/50c5e88cf0a4e39110784bac0b3c3b19.jpg", Img1 = "https://i.pinimg.com/564x/60/53/a3/6053a3334481638a282a1fd96739e394.jpg" ,Img2 = "https://image.tensorartassets.com/cdn-cgi/image/w=500,q=85/model_showcase/662890501185306177/b8d5abc0-278b-8ff3-0d8d-2393949236d3.jpeg", Biography = "Maomao is the child of the high-ranking courtesan Fengxian and the military official Lakan; however, due to her parents' situation at the time, she was not raised by them. Instead, Maomao was born and raised in Verdigris House in the red-light district, and was nursed by Pairin, raised by the Three Princesses and the Madam, and adopted by her grand-uncle Luomen. From her adopted father, Maomao learned about herbs and how to handle illnesses, and she was a pharmacist for multiple brothels in the red-light district." },
            new Character(){ Name = "Manjiro Sano", Anime = "Tokyo Revengers", Birth = "August 20", Gender = "Male", H = "162 cm", W = "56 kg", Img = "https://i.pinimg.com/736x/ce/f5/69/cef5694b689ee35d6eca9986a075e1d6.jpg", Img1 = "https://i.pinimg.com/736x/ce/43/57/ce4357b3366491db76ceb34c914d5a4d.jpg" ,Img2 = "https://i.pinimg.com/564x/b2/21/70/b2217042b326c66d31c293c23de73de3.jpg", Biography = "Manjiro Mikey Sano is the deuteragonist-turned antagonist of the 2017 Japanese manga series Tokyo Revengers and its 2021 anime adaptation. He is a founding member and President of the Tokyo Manji Gang, the younger brother of Shinichiro Sano, the founder and the very first president of the Black Dragons, and the half-brother of Emma Sano." },
            new Character(){ Name = "Anya Forger", Anime = "Spy × Family", Birth = " - ", Gender = "Female", H = "99.5 cm", W = "35 kg", Img = "https://i.pinimg.com/564x/10/c9/c0/10c9c02224ae9c08ba781bae2a856675.jpg", Img1 = "https://i.pinimg.com/564x/ee/85/f2/ee85f2a8f3731b8f957a45d0d41dba18.jpg" ,Img2 = "https://i.pinimg.com/736x/54/89/1a/54891a6195c3c9314be38d4701cebbab.jpg", Biography = "She is the adoptive daughter of Loid Forger and Yor Forger, having been taken under Loid's care as part of his current mission. In her past life, she is an orphan known as Test Subject 007, where an accidental creation gave her telepathic abilities." }
        };

    }
}
