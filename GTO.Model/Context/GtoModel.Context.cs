﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GTO.Model.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GtoDatabaseContext : DbContext
    {
        public GtoDatabaseContext()
            : base("name=GtoDatabaseContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GtoEvent> GtoEvents { get; set; }
        public virtual DbSet<GtoEventPlayer> GtoEventPlayers { get; set; }
        public virtual DbSet<GtoEventPlayerRecord> GtoEventPlayerRecords { get; set; }
        public virtual DbSet<GtoEventTest> GtoEventTests { get; set; }
        public virtual DbSet<Judge> Judges { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestGroup> TestGroups { get; set; }
        public virtual DbSet<AgeGroup> AgeGroups { get; set; }
        public virtual DbSet<AgeGroupRequirment> AgeGroupRequirments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}
