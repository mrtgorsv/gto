//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
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
        public short PassSerial { get; set; }
        public int PassNumber { get; set; }
        public string PassCode { get; set; }
        public string PassLocation { get; set; }
        public System.DateTime PassDate { get; set; }
        public string HomeAddress { get; set; }
        public string RegistrationAddress { get; set; }
        public string SportCategory { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GtoEventPlayer> GtoEventPlayers { get; set; }
    }
}