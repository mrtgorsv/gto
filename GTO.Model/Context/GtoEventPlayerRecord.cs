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
    
    public partial class GtoEventPlayerRecord
    {
        public int Id { get; set; }
        public int GtoEventPlayerId { get; set; }
        public int GtoEventTestId { get; set; }
        public string TestValue { get; set; }
    
        public virtual GtoEventPlayer GtoEventPlayer { get; set; }
        public virtual GtoEventTest GtoEventTest { get; set; }
    }
}
