namespace GTO.Model.Context
{
    public partial class GtoEventTest
    {
        public string TestName => Test != null ? Test.Name : string.Empty;

        public string JudgeName => Judge != null ? Judge.FullName : string.Empty;
    }
}
