using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleLibrary.Domain.Entities;
using SampleLibrary.Domain.Entities.ValueObjects;

namespace SampleLibrary.Infra.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
            
            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            builder.OwnsOne(b => b.Publication)
                .Property(p => p.Year)
                .IsRequired()
                .HasColumnName(nameof(Publication.Year));
            
            builder.OwnsOne(b => b.Publication)
                .Property(p => p.Edition)
                .IsRequired()
                .HasColumnName(nameof(Publication.Edition));
        }
    }
}