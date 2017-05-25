using System;

namespace GTO.Model.Context
{
    public partial class Player
    {
        public string FullName => $"{LastName} {MiddleName} {Name}({PassSerial:D4}-{PassNumber:D6})";

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