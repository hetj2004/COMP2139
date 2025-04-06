using COMP2139_ICE.Models;
using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    
    public DbSet<ProjectComment> ProjectComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure Identity Configurations and Table are created
        base.OnModelCreating(modelBuilder);
        
        // Define one-to-many relationship
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)                 // One project has (potentially) many tasks
            .WithOne(t => t.Project)            // Each projectTask belong to a project
            .HasForeignKey(t => t.ProjectId)    // Foreign Key in ProjectTask table
            .OnDelete(DeleteBehavior.Cascade);  
        
        // Seeding the database
        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                ProjectId = 1, 
                Name = "Assignment 1", 
                Description = "COMP2139 - Assignment 1",
                StartDate = DateTime.SpecifyKind(new DateTime(2025, 4, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2025, 6, 21), DateTimeKind.Utc),
                Status = "Not Started"
            },
            new Project
            {
                ProjectId = 2, 
                Name = "Assignment 2", 
                Description = "COMP2139 - Assignment 2",
                StartDate = DateTime.SpecifyKind(new DateTime(2025, 4, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2025, 6, 21), DateTimeKind.Utc),
                Status = "Not Started"
            }
            );
    }
    
}