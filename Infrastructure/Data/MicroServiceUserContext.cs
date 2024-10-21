using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MicroServiceUserContext : DbContext
    {
        public MicroServiceUserContext(DbContextOptions<MicroServiceUserContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=localhost;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionBuilder);
        }
        //entities
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.UserID);
                entity.Property(i => i.UserID).ValueGeneratedOnAdd().IsRequired();
                entity.Property(n => n.Name).HasColumnType("nvarchar(150)").IsRequired();
                entity.Property(e => e.Email).HasColumnType("nvarchar(100)").IsRequired();
                entity.Property(p => p.Password).HasColumnType("varbinary(512)").IsRequired(); // Cambiado a varbinary
                entity.Property(p => p.PasswordSalt).HasColumnType("varbinary(512)").IsRequired(); // Añadido para el salt
                entity.Property(t => t.Type).IsRequired();
                entity.Property(p => p.Photo);
                entity.Property(d => d.DateBirth).IsRequired();
                entity.Property(d => d.CreateDate).IsRequired();

                // Generar hashes y salts para las contraseñas precargadas
                var passwordSalt = GenerateSalt();
                var passwordHash = HashPassword("password123", passwordSalt);

                entity.HasData(
                    // Usuarios con Type = 2 (compradores)
                    new User { UserID = 1, Name = "John Doe", Email = "johndoe@example.com", Password = passwordHash, PasswordSalt = passwordSalt, Type = 2, Photo = null, DateBirth = new DateTime(1990, 5, 20), CreateDate = DateTime.Now },
                    new User { UserID = 2, Name = "Jane Smith", Email = "janesmith@example.com", Password = passwordHash, PasswordSalt = passwordSalt, Type = 2, Photo = null, DateBirth = new DateTime(1985, 8, 12), CreateDate = DateTime.Now },
                    new User { UserID = 3, Name = "Carlos Lopez", Email = "carloslopez@example.com", Password = passwordHash, PasswordSalt = passwordSalt, Type = 2, Photo = null, DateBirth = new DateTime(1992, 11, 3), CreateDate = DateTime.Now },
                    new User { UserID = 4, Name = "Maria Garcia", Email = "mariagarcia@example.com", Password = passwordHash, PasswordSalt = passwordSalt, Type = 2, Photo = null, DateBirth = new DateTime(1995, 1, 10), CreateDate = DateTime.Now },
                    new User { UserID = 5, Name = "Pedro Fernandez", Email = "pedrofernandez@example.com", Password = passwordHash, PasswordSalt = passwordSalt, Type = 2, Photo = null, DateBirth = new DateTime(1988, 9, 25), CreateDate = DateTime.Now },

                    // Usuarios con Type = 1 (admins)
                    new User { UserID = 6, Name = "Laura Martinez", Email = "lauramartinez@example.com", Password = passwordHash, PasswordSalt = passwordSalt, Type = 1, Photo = null, DateBirth = new DateTime(1993, 7, 15), CreateDate = DateTime.Now },
                    new User { UserID = 7, Name = "David Rodriguez", Email = "davidrodriguez@example.com", Password = passwordHash, PasswordSalt = passwordSalt, Type = 1, Photo = null, DateBirth = new DateTime(1987, 3, 5), CreateDate = DateTime.Now }
                );
            });

        }
        public byte[] GenerateSalt()
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return hmac.Key; // Genera el salt
            }
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
