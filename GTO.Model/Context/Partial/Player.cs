using System;

namespace GTO.Model.Context
{
    public partial class Player
    {
        public string FullName
        {
            get { return string.Format("{0} {1} {2}({3:D4}-{4:D6})", LastName, MiddleName, Name, PassSerial, PassNumber); }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;
                if (BirthDate > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}