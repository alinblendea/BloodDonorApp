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
    
    public partial class Donator
    {
        public Donator()
        {
            this.Punga_Sange = new HashSet<Punga_Sange>();
        }
    
        public string cnp_donator { get; set; }
        public string nume { get; set; }
        public string domiciliu { get; set; }
        public string resedinta { get; set; }
        public string email { get; set; }
        public string telefon { get; set; }
        public int id_chestionar { get; set; }
    
        public virtual Chestionar_Medical Chestionar_Medical { get; set; }
        public virtual ICollection<Punga_Sange> Punga_Sange { get; set; }
    }
}
