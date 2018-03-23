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
		public virtual DbSet<connections> connections { get; set; }
		public virtual DbSet<creditcard> creditcard { get; set; }
		public virtual DbSet<region> region { get; set; }
		public virtual DbSet<relay> relay { get; set; }
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

			modelBuilder.Entity<creditcard>()
				.Property(e => e.expirationDate)
				.HasPrecision(0);

			modelBuilder.Entity<region>()
				.Property(e => e.regionColor)
				.IsUnicode(false);

			modelBuilder.Entity<relay>()
				.HasMany(e => e.connections)
				.WithOptional(e => e.relay)
				.HasForeignKey(e => e.relayID);

			modelBuilder.Entity<relay>()
				.HasMany(e => e.connections1)
				.WithRequired(e => e.relay1)
				.HasForeignKey(e => e.destRelayID)
				.WillCascadeOnDelete(false);

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
