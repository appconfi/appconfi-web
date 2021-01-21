using System;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.v1
{
    public class RequestVersionModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Env { get; set; } = "[default]";

        [Required]
        public Guid App { get; set; }
    }
}
