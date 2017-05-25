namespace GTO.Model.Context
{
    public partial class Judge
    {
        public string FullName => $"{LastName} {MiddleName} {Name}";
    }
}
