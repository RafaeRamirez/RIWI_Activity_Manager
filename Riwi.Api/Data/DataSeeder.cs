using Riwi.Api.Models;
using Riwi.Api.Enums;

namespace Riwi.Api.Data;

public static class DataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        // Ensure database is created
        // context.Database.EnsureCreated(); // Usually handled by migrations or external setup, but good to have if we want auto-create. 
        // However, if migrations are used, EnsureCreated might cause issues. 
        // Given the context, I'll assume the DB exists or is managed elsewhere, but I'll leave it commented out or rely on the caller to ensure DB exists.
        // Actually, for a simple seed, checking Any() is enough.

        // Seed Persons
        if (!context.Persons.Any())
        {
            var persons = new List<Person>
            {
                new Person
                {
                    Email = "admin@riwi.io",
                    FullName = "Admin User",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Role = UserRole.Admin,
                    Phone = "1234567890",
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Email = "user1@riwi.io",
                    FullName = "Regular User 1",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Role = UserRole.Coder,
                    Phone = "0987654321",
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Email = "user2@riwi.io",
                    FullName = "Regular User 2",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Role = UserRole.Coder,
                    Phone = "1122334455",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Persons.AddRange(persons);
            context.SaveChanges();
        }
        else
        {
            // Backfill passwords for existing users if needed
            var usersWithoutPassword = context.Persons.Where(p => p.Password == "" || p.Password == null).ToList();
            if (usersWithoutPassword.Any())
            {
                foreach (var user in usersWithoutPassword)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword("123456");
                }
                context.SaveChanges();
            }
        }

        // Seed Events
        if (!context.Events.Any())
        {
            var adminUser = context.Persons.FirstOrDefault(p => p.Email == "admin@riwi.io");
            if (adminUser != null)
            {
                var events = new List<Event>
                {
                    new Event
                    {
                        Title = "Intro to C# Workshop",
                        Description = "A beginner friendly workshop on C#.",
                        EventType = "Workshop",
                        Capacity = 30,
                        StartAt = DateTimeOffset.UtcNow.AddDays(7),
                        EndAt = DateTimeOffset.UtcNow.AddDays(7).AddHours(2),
                        CreatedBy = adminUser.PersonId,
                        IsPublished = true,
                        PublishedAt = DateTimeOffset.UtcNow
                    },
                    new Event
                    {
                        Title = "Tech Meetup",
                        Description = "Networking event for tech enthusiasts.",
                        EventType = "Meetup",
                        Capacity = 50,
                        StartAt = DateTimeOffset.UtcNow.AddDays(14),
                        EndAt = DateTimeOffset.UtcNow.AddDays(14).AddHours(3),
                        CreatedBy = adminUser.PersonId,
                        IsPublished = true,
                        PublishedAt = DateTimeOffset.UtcNow
                    }
                };

                context.Events.AddRange(events);
                context.SaveChanges();
            }
        }
    }
}
