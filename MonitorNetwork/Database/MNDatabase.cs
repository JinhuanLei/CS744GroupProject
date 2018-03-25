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
		public virtual DbSet<colors> colors { get; set; }
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

			modelBuilder.Entity<colors>()
				.Property(e => e.colorName)
				.IsUnicode(false);

			modelBuilder.Entity<colors>()
				.HasMany(e => e.region)
				.WithRequired(e => e.colors)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<creditcard>()
				.Property(e => e.expirationDate)
				.HasPrecision(0);

			modelBuilder.Entity<region>()
				.HasOptional(e => e.region1)
				.WithRequired(e => e.region2);

			modelBuilder.Entity<region>()
				.HasMany(e => e.relay)
				.WithRequired(e => e.region)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<region>()
				.HasMany(e => e.store)
				.WithRequired(e => e.region)
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
