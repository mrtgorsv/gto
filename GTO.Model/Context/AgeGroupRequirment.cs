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
    
    public partial class AgeGroupRequirment
    {
        public int Id { get; set; }
        public short GoldCount { get; set; }
        public short SerebroCount { get; set; }
        public short BronzeCount { get; set; }
        public int AgeGroupId { get; set; }
        public short Sex { get; set; }
    
        public virtual AgeGroup AgeGroup { get; set; }
    }
}
