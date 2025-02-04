using System.ComponentModel.DataAnnotations;
using COMP2139_Labs.Models;

namespace comp2139.Models;

public class Project
{
    /// <summary>
    /// The unique identifier dor the Project entity
    /// </summary>
    public int ProjectId { get; set; }
    /// <summary>
    /// Required field describing a projects name
    /// </summary>
    [Required]
    public required string Name { get; set; }
    
    public string? Description { get; set; }
    
    [DataType(DataType.Date)] 
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    public string? Status { get;set; }
    
    //One-to-Many
    //This will allow EF Core to understand that one Project has potentially many ProjectTasks
    public List<ProjectTask>? Tasks { get; set; }
}