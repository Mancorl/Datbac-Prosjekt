using System;
using Microsoft.EntityFrameworkCore;
using Unhosted_Api.Models;

namespace Unhosted_Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Borrowing> Borrow { get; set; }
    public DbSet<RegisteredUser> RegisteredUsers { get; set; }
    public DbSet<BoardGame> BoardGames { get; set; }
}