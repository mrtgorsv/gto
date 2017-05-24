namespace GTO.Model.Context
{
    public partial class GtoEventPlayer
    {
        public string PlayerName
        {
            get { return Player != null ?Player.FullName : string.Empty; }
        }
    }
}
