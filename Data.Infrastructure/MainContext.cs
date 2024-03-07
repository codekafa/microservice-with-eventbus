using Data.Domain;
using Data.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

public class MainContext : DbContext
{
	public MainContext()
	{
	}

    public DbSet<User> Users { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(MainContext)));
        base.OnModelCreating(modelBuilder); 
    }
}

// tayfun geliştirme

//tayfun 2
