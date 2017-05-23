namespace GTO.Model.Context
{
    public partial class GtoEventTest
    {
        public string TestName
        {
            get { return Test != null ? Test.Name : string.Empty; }
        }
        public string JudgeName
        {
            get { return Judge != null ? Judge.FullName : string.Empty; }
        }
    }
}
