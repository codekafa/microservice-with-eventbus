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

    public DbSet<User> User { get; set; }
    public DbSet<Brand> Brand { get; set; }
    public DbSet<Model> Model { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(MainContext)));
        base.OnModelCreating(modelBuilder); 
    }
}
