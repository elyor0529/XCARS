//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCars.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AutoModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutoModel()
        {
            this.Autoes = new HashSet<Auto>();
        }
    
        public int ID { get; set; }
        public int MakeID { get; set; }
        public string Name { get; set; }
        public string Name_ru { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auto> Autoes { get; set; }
        public virtual AutoMake AutoMake { get; set; }
    }
}