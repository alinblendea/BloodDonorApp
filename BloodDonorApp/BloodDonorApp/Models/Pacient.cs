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
    
    public partial class Pacient
    {
        public string cnp_pacient { get; set; }
        public string nume { get; set; }
        public string grupa_sanguina { get; set; }
        public int id_spital { get; set; }
        public string nume_spital { get; set; }
    
        public virtual Spital Spital { get; set; }
    }
}
