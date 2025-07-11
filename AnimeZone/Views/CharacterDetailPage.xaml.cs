using System.ComponentModel;
namespace AnimeZone.Views;

public partial class CharacterDetailPage : ContentPage
{
    public CharacterDetailPage(Models.Character character)
    {
        InitializeComponent();
        BindingContext = new CharacterDetailViewModel(character);
    }

    public class CharacterDetailViewModel : INotifyPropertyChanged
    {
        private Models.Character selectedCharacter;

        public Models.Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (selectedCharacter != value)
                {
                    selectedCharacter = value;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public CharacterDetailViewModel(Models.Character character)
        {
            SelectedCharacter = character;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
