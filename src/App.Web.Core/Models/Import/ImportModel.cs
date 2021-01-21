using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Import
{
    public class ImportModel
    {
        public Guid EnvironmentId { get; set; }
        public Guid ApplicationId { get; set; }
        public string Format { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
