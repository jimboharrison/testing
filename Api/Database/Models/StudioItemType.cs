﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PeoplesPartnership.ApiRefactor.Database.Models;

public class StudioItemType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudioItemTypeId { get; set; }
    
    [Required]
    public string Value { get; set; }
    
    [JsonIgnore]
    public ICollection<StudioItem> StudioItem { get; set; }
}