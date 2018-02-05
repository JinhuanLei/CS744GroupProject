namespace MonitorNetwork.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MNDatabase : DbContext
    {
        public MNDatabase()
            : base("name=MNDatabase")
        {
        }

        public virtual DbSet<account> account { get; set; }
        public virtual DbSet<creditcard> creditcard { get; set; }
        public virtual DbSet<relay> relay { get; set; }
        public virtual DbSet<relayconnectionweight> relayconnectionweight { get; set; }
        public virtual DbSet<store> store { get; set; }
        public virtual DbSet<transaction> transaction { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.spendingLimit)
                .HasPrecision(10, 2);

            modelBuilder.Entity<account>()
                .Property(e => e.balance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<account>()
                .HasMany(e => e.creditcard)
                .WithOptional(e => e.account)
                .HasForeignKey(e => e.accID);

            modelBuilder.Entity<creditcard>()
                .Property(e => e.expirationDate)
                .HasPrecision(0);

            modelBuilder.Entity<relay>()
                .HasMany(e => e.relayconnectionweight)
                .WithOptional(e => e.relay)
                .HasForeignKey(e => e.relay1);

            modelBuilder.Entity<relay>()
                .HasMany(e => e.relayconnectionweight1)
                .WithOptional(e => e.relay3)
                .HasForeignKey(e => e.relay2);

            modelBuilder.Entity<relay>()
                .HasMany(e => e.store)
                .WithMany(e => e.relay)
                .Map(m => m.ToTable("storetorelay", "cs744").MapLeftKey("relayID").MapRightKey("storeID"));

            modelBuilder.Entity<transaction>()
                .Property(e => e.timeOfTransaction)
                .HasPrecision(0);

            modelBuilder.Entity<transaction>()
                .Property(e => e.timeOfResponse)
                .HasPrecision(0);

            modelBuilder.Entity<transaction>()
                .Property(e => e.amount)
                .HasPrecision(10, 2);
        }
    }
}
