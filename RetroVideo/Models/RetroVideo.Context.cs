﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetroVideo.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RetroVideoEntities : DbContext
    {
        public RetroVideoEntities()
            : base("name=RetroVideoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Klant> Klanten { get; set; }
        public virtual DbSet<Reservatie> Reservaties { get; set; }
    }
}
