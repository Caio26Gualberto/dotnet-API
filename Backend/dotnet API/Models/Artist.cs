﻿using dotnet_API.Enumerables;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_API.Models
{
    [Table("Artistas")]
    public class Artist
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        [NotMapped]
        public ICollection<EStyle> Styles { get; set; }
        public virtual User? User { get; set; } 
    }
}
