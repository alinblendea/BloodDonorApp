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
    
    public partial class Cont
    {
        public Cont()
        {
            this.Donators = new HashSet<Donator>();
            this.Medics = new HashSet<Medic>();
            this.Personal_Recoltare = new HashSet<Personal_Recoltare>();
        }
    
        public int id_cont { get; set; }
        public string email { get; set; }
        public string parola { get; set; }
        public string type { get; set; }
    
        public virtual ICollection<Donator> Donators { get; set; }
        public virtual ICollection<Medic> Medics { get; set; }
        public virtual ICollection<Personal_Recoltare> Personal_Recoltare { get; set; }
    }
}