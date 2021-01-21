using System;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.v1
{

    public class RequestFeaturesModel
    {
        [Required(ErrorMessage = "Invalid key")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Invalid environment")]
        public string Env { get; set; } = "[default]";

        [Required(ErrorMessage = "Invalid application")]
        public Guid? App { get; set; }
        public bool Download { get; set; } = false;
    }
}
