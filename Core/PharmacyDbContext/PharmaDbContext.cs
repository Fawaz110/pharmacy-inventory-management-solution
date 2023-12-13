﻿using Core.PharmacyEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyDbContext
{
    public class PharmaDbContext : IdentityDbContext<ApplicationUser>
    {
        public PharmaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicineInventory>().HasKey(x => new { x.MedicineId, x.inventoryId });

            modelBuilder.Entity<ReceiptSender>().HasKey(x => new {x.SenderId,x.ReceiptId});
            modelBuilder.Entity<ReceiptReceiver>().HasKey(x => new {x.ReceiverId,x.ReceiptId});

            //modelBuilder.Entity<ReceiptInventory>().HasKey(x => new { x.Sender, x.Receiver, x.Receipt });

            //modelBuilder.Entity<ReceiptInventory>()
            //                .HasOne(ri => ri.Sender)
            //                .WithMany(i => i.ReceiptInventories)
            //                .HasForeignKey(ri => ri.SenderId);

            //modelBuilder.Entity<ReceiptInventory>()
            //                            .HasOne(ri => ri.Receiver)
            //                            .WithMany(i => i.ReceiptInventories)
            //                            .HasForeignKey(ri => ri.ReceiverId);

            //modelBuilder.Entity<ReceiptInventory>()
            //        .HasOne(ri => ri.Receipt)
            //        .WithMany(i => i.ReceiptInventories).HasForeignKey(ri => ri.ReceiptId);




            //modelBuilder.Entity<Receipt>()
            //                .HasMany(i => i.SenderInventory)
            //                .WithMany(inv => inv.SentInvoices)
            //                .UsingEntity(j => j.ToTable("InvoiceSenderInventory"));

            //modelBuilder.Entity<Receipt>()
            //                .HasMany(i => i.ReceiverInventory)
            //                .WithMany(inv => inv.ReceivedInvoices)
            //                .UsingEntity(j => j.ToTable("InvoiceReceiverInventory"));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<MedicineInventory> MedicineInventories { get; set; }
        public DbSet<ReceiptSender> ReceiptSenders { get; set; }
        public DbSet<WorkflowTracking> WorkflowTrackingRecords { get; set; }
        public DbSet<Report> Reports { get; set; }
        //public DbSet<ReceiptInventory> ReceiptInventories { get; set; }
    }
}
