//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sitedb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Details_has_Materials
    {
        public int Details_ID { get; set; }
        public int Materials_ID { get; set; }
        [Required]
        [Range(1, 999)]
        public int Quantity { get; set; }
    
        public virtual Details Details { get; set; }
        public virtual Materials Materials { get; set; }
    }
}