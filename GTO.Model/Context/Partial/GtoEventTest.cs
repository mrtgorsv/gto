namespace GTO.Model.Context
{
    public partial class GtoEventTest
    {
        public string TestName
        {
            get { return Test != null ? Test.Name : string.Empty; }
        }
    }
}
