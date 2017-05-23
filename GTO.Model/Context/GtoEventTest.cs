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
    
    public partial class GtoEventTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GtoEventTest()
        {
            this.GtoEventPlayerRecords = new HashSet<GtoEventPlayerRecord>();
        }
    
        public int Id { get; set; }
        public int GtoEventId { get; set; }
        public int TestId { get; set; }
        public Nullable<int> JudgeId { get; set; }
    
        public virtual GtoEvent GtoEvent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GtoEventPlayerRecord> GtoEventPlayerRecords { get; set; }
        public virtual Judge Judge { get; set; }
        public virtual Test Test { get; set; }
    }
}
