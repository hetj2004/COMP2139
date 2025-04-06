using System.ComponentModel.DataAnnotations;

namespace COMP2139_ICE.Areas.ProjectManagement.Models;

public class Project
{
    /// <summary>
    /// The unique identifier for the project entity
    /// </summary>
    public int ProjectId { get; set; }
    
    /// <summary>
    /// Required field describing a project name
    /// </summary>
    [Display(Name = "Project Name")]
    [Required]
    [StringLength(100, ErrorMessage = "Project Name cannot be longer than 100 characters.")]
    public required string Name { get; set; }
    
    [Display(Name = "Project Description")]
    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = "Project Description cannot be longer than 500 characters.")]
    public string? Description { get; set; }
    
    [Display(Name = "Project Start Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime StartDate 
    { 
        get => _startDate; 
        set => _startDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
    }
    private DateTime _startDate = DateTime.UtcNow;
    
    [Display(Name = "Project End Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime EndDate 
    { 
        get => _endDate; 
        set => _endDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
    }
    private DateTime _endDate = DateTime.UtcNow;
    
    [Display(Name = "Project Status")]
    public string? Status { get; set; }
    
    // One-to-Many: A Project can have many tasks
    public List<ProjectTask>? Tasks { get; set; } = new();
}