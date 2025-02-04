using System.ComponentModel.DataAnnotations;
using comp2139.Models;

namespace COMP2139_Labs.Models;

public class ProjectTask
{
    
    [Key]
    public int ProjectTaskId { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    [Required]
    public required string Description { get; set; }
    
    //Foreign key from Project
    public int ProjectId { get; set; }
    
    //Navigation property
    //This property allows for easy access to the related Project entity from a ProjectTask entity
    public Project? Project { get; set; }
    
}