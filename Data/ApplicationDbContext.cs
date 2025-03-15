using COMP2139_Labs.Models;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }
    
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
           
        modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, Name = "Assignment 1", Description = "COMP2139 - Assignment 1" },
                new Project { ProjectId = 2, Name = "Assignment 2", Description = "COMP2139 - Assignment 2" }
            );

        
    }
    
    
    
}