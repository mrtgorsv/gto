namespace GTO.Model.Context
{
    public partial class GtoEventPlayer
    {
        public string PlayerName => Player != null ?Player.FullName : string.Empty;
    }
}
