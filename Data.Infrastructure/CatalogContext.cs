using Data.Domain;
using Data.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

public class CatalogContext : DbContext
{
	public CatalogContext()
	{
	}

    public DbSet<Brand> Brand { get; set; }
    public DbSet<Model> Model { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CatalogContext)));
        base.OnModelCreating(modelBuilder); 
    }
}
