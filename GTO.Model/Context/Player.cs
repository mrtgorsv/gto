//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GTO.Model.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            this.GtoEventPlayers = new HashSet<GtoEventPlayer>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public short Sex { get; set; }
        public Nullable<short> PassSerial { get; set; }
        public Nullable<int> PassNumber { get; set; }
        public string PassCode { get; set; }
        public string PassLocation { get; set; }
        public Nullable<System.DateTime> PassDate { get; set; }
        public string HomeAddress { get; set; }
        public string RegistrationAddress { get; set; }
        public string SportCategory { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GtoEventPlayer> GtoEventPlayers { get; set; }
    }
}
