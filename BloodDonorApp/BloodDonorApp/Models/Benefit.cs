//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BloodDonorApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Benefit
    {
        public Benefit()
        {
            this.Donares = new HashSet<Donare>();
        }
    
        public int id_beneficiu { get; set; }
        public string denumire { get; set; }
        public int nr_total { get; set; }
        public int nr_ramase { get; set; }
        public Nullable<int> cost_per_buc { get; set; }
    
        public virtual ICollection<Donare> Donares { get; set; }
    }
}