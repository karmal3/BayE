using System;
using BayE.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace BayE.Entities
{
    public partial class BayEContext : DbContext
    {
        private readonly AppSettings _appSettings;

        public BayEContext()
        {
        }

        public BayEContext(DbContextOptions<BayEContext> options, IOptions<AppSettings> appSettings)
            : base(options)
        {
            _appSettings = appSettings.Value;
        }

        public virtual DbSet<Ad> Ad { get; set; }
        public virtual DbSet<Adcategory> Adcategory { get; set; }
        public virtual DbSet<Adcomments> Adcomments { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Adsubcategory> Adsubcategory { get; set; }
        public virtual DbSet<Auctionad> Auctionad { get; set; }
        public virtual DbSet<Autobid> Autobid { get; set; }
        public virtual DbSet<Bid> Bid { get; set; }
        public virtual DbSet<Blockeduser> Blockeduser { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Competition> Competition { get; set; }
        public virtual DbSet<Forum> Forum { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Privatemessage> Privatemessage { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<Topiccomments> Topiccomments { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Viewedad> Viewedad { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_appSettings.DefaultConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Ad>(entity =>
            {
                entity.ToTable("ad", "baye");

                entity.HasIndex(e => e.FkCategoryId)
                    .HasName("fk_category_id");

                entity.HasIndex(e => e.FkSubcategoryId)
                    .HasName("fk_subcategory_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.HasIndex(e => e.Status)
                    .HasName("status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FkCategoryId)
                    .HasColumnName("fk_category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkSubcategoryId)
                    .HasColumnName("fk_subcategory_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCategory)
                    .WithMany(p => p.Ad)
                    .HasForeignKey(d => d.FkCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ad_ibfk_2");

                entity.HasOne(d => d.FkSubcategory)
                    .WithMany(p => p.Ad)
                    .HasForeignKey(d => d.FkSubcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ad_ibfk_3");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Ad)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("ad_ibfk_1");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Ad)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ad_ibfk_4");
            });

            modelBuilder.Entity<Adcategory>(entity =>
            {
                entity.ToTable("adcategory", "baye");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Adcomments>(entity =>
            {
                entity.ToTable("adcomments", "baye");

                entity.HasIndex(e => e.FkAdId)
                    .HasName("fk_ad_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkAdId)
                    .HasColumnName("fk_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkAd)
                    .WithMany(p => p.Adcomments)
                    .HasForeignKey(d => d.FkAdId)
                    .HasConstraintName("adcomments_ibfk_2");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Adcomments)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("adcomments_ibfk_1");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin", "baye");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Admin)
                    .HasForeignKey(d => d.FkUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("admin_ibfk_1");
            });

            modelBuilder.Entity<Adsubcategory>(entity =>
            {
                entity.ToTable("adsubcategory", "baye");

                entity.HasIndex(e => e.FkCategoryId)
                    .HasName("fk_category_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkCategoryId)
                    .HasColumnName("fk_category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCategory)
                    .WithMany(p => p.Adsubcategory)
                    .HasForeignKey(d => d.FkCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adsubcategory_ibfk_1");
            });

            modelBuilder.Entity<Auctionad>(entity =>
            {
                entity.ToTable("auctionad", "baye");

                entity.HasIndex(e => e.FkCategoryId)
                    .HasName("fk_category_id");

                entity.HasIndex(e => e.FkSubcategoryId)
                    .HasName("fk_subcategory_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.HasIndex(e => e.Status)
                    .HasName("status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FkCategoryId)
                    .HasColumnName("fk_category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkSubcategoryId)
                    .HasColumnName("fk_subcategory_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkCategory)
                    .WithMany(p => p.Auctionad)
                    .HasForeignKey(d => d.FkCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auctionad_ibfk_2");

                entity.HasOne(d => d.FkSubcategory)
                    .WithMany(p => p.Auctionad)
                    .HasForeignKey(d => d.FkSubcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auctionad_ibfk_3");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Auctionad)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("auctionad_ibfk_1");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Auctionad)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auctionad_ibfk_4");
            });

            modelBuilder.Entity<Autobid>(entity =>
            {
                entity.ToTable("autobid", "baye");

                entity.HasIndex(e => e.FkAuctionAdId)
                    .HasName("fk_auction_ad_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkAuctionAdId)
                    .HasColumnName("fk_auction_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MaxPrice)
                    .HasColumnName("max_price")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.FkAuctionAd)
                    .WithMany(p => p.Autobid)
                    .HasForeignKey(d => d.FkAuctionAdId)
                    .HasConstraintName("autobid_ibfk_2");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Autobid)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("autobid_ibfk_1");
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("bid", "baye");

                entity.HasIndex(e => e.FkAuctionAdId)
                    .HasName("fk_auction_ad_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkAuctionAdId)
                    .HasColumnName("fk_auction_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.FkAuctionAd)
                    .WithMany(p => p.Bid)
                    .HasForeignKey(d => d.FkAuctionAdId)
                    .HasConstraintName("bid_ibfk_2");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Bid)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("bid_ibfk_1");
            });

            modelBuilder.Entity<Blockeduser>(entity =>
            {
                entity.ToTable("blockeduser", "baye");

                entity.HasIndex(e => e.FkAdminId)
                    .HasName("fk_admin_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkAdminId)
                    .HasColumnName("fk_admin_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkAdmin)
                    .WithMany(p => p.Blockeduser)
                    .HasForeignKey(d => d.FkAdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("blockeduser_ibfk_1");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Blockeduser)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("blockeduser_ibfk_2");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city", "baye");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("competition", "baye");

                entity.HasIndex(e => e.FkAdminId)
                    .HasName("fk_admin_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Deadline).HasColumnName("deadline");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FkAdminId)
                    .HasColumnName("fk_admin_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkAdmin)
                    .WithMany(p => p.Competition)
                    .HasForeignKey(d => d.FkAdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("competition_ibfk_2");
            });

            modelBuilder.Entity<Forum>(entity =>
            {
                entity.ToTable("forum", "baye");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ViewCount)
                    .HasColumnName("view_count")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Forum)
                    .HasForeignKey(d => d.FkUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("forum_ibfk_1");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("participant", "baye");

                entity.HasIndex(e => e.FkCompetitionId)
                    .HasName("fk_competition_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkCompetitionId)
                    .HasColumnName("fk_competition_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkCompetition)
                    .WithMany(p => p.Participant)
                    .HasForeignKey(d => d.FkCompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("participant_ibfk_2");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Participant)
                    .HasForeignKey(d => d.FkUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("participant_ibfk_1");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment", "baye");

                entity.HasIndex(e => e.FkAdId)
                    .HasName("fk_ad_id");

                entity.HasIndex(e => e.FkAuctionAdId)
                    .HasName("fk_auction_ad_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CardHolder)
                    .HasColumnName("card_holder")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasColumnName("card_number")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkAdId)
                    .HasColumnName("fk_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkAuctionAdId)
                    .HasColumnName("fk_auction_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.FkAd)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.FkAdId)
                    .HasConstraintName("payment_ibfk_2");

                entity.HasOne(d => d.FkAuctionAd)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.FkAuctionAdId)
                    .HasConstraintName("payment_ibfk_3");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.FkUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("payment_ibfk_1");
            });

            modelBuilder.Entity<Privatemessage>(entity =>
            {
                entity.ToTable("privatemessage", "baye");

                entity.HasIndex(e => e.FkReceiverId)
                    .HasName("fk_receiver_id");

                entity.HasIndex(e => e.FkSenderId)
                    .HasName("fk_sender_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkReceiverId)
                    .HasColumnName("fk_receiver_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkSenderId)
                    .HasColumnName("fk_sender_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiverDeleted)
                    .HasColumnName("receiver_deleted")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ReceiverRead)
                    .HasColumnName("receiver_read")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SenderDeleted)
                    .HasColumnName("sender_deleted")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkReceiver)
                    .WithMany(p => p.PrivatemessageFkReceiver)
                    .HasForeignKey(d => d.FkReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("privatemessage_ibfk_2");

                entity.HasOne(d => d.FkSender)
                    .WithMany(p => p.PrivatemessageFkSender)
                    .HasForeignKey(d => d.FkSenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("privatemessage_ibfk_1");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status", "baye");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("topic", "baye");

                entity.HasIndex(e => e.FkForumId)
                    .HasName("fk_forum_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FkForumId)
                    .HasColumnName("fk_forum_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ViewCount)
                    .HasColumnName("view_count")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkForum)
                    .WithMany(p => p.Topic)
                    .HasForeignKey(d => d.FkForumId)
                    .HasConstraintName("topic_ibfk_2");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Topic)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("topic_ibfk_1");
            });

            modelBuilder.Entity<Topiccomments>(entity =>
            {
                entity.ToTable("topiccomments", "baye");

                entity.HasIndex(e => e.FkTopicId)
                    .HasName("fk_topic_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FkTopicId)
                    .HasColumnName("fk_topic_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkTopic)
                    .WithMany(p => p.Topiccomments)
                    .HasForeignKey(d => d.FkTopicId)
                    .HasConstraintName("topiccomments_ibfk_1");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Topiccomments)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("topiccomments_ibfk_2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "baye");

                entity.HasIndex(e => e.Email)
                    .HasName("email")
                    .IsUnique();

                entity.HasIndex(e => e.FkCityId)
                    .HasName("fk_city_id");

                entity.HasIndex(e => e.Username)
                    .HasName("username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FkCityId)
                    .HasColumnName("fk_city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasColumnType("binary(64)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasColumnType("binary(128)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCity)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.FkCityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ibfk_1");
            });

            modelBuilder.Entity<Viewedad>(entity =>
            {
                entity.ToTable("viewedad", "baye");

                entity.HasIndex(e => e.FkAdId)
                    .HasName("fk_ad_id");

                entity.HasIndex(e => e.FkAuctionAdId)
                    .HasName("fk_auction_ad_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.FkAdId)
                    .HasColumnName("fk_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkAuctionAdId)
                    .HasColumnName("fk_auction_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkAd)
                    .WithMany(p => p.Viewedad)
                    .HasForeignKey(d => d.FkAdId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("viewedad_ibfk_2");

                entity.HasOne(d => d.FkAuctionAd)
                    .WithMany(p => p.Viewedad)
                    .HasForeignKey(d => d.FkAuctionAdId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("viewedad_ibfk_3");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Viewedad)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("viewedad_ibfk_1");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("wishlist", "baye");

                entity.HasIndex(e => e.FkAdId)
                    .HasName("fk_ad_id");

                entity.HasIndex(e => e.FkUserId)
                    .HasName("fk_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkAdId)
                    .HasColumnName("fk_ad_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUserId)
                    .HasColumnName("fk_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkAd)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.FkAdId)
                    .HasConstraintName("wishlist_ibfk_2");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("wishlist_ibfk_1");
            });
        }
    }
}
