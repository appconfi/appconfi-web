﻿using System;

namespace App.Web.Core.Models.Application
{
    public class ApplicationFeatureToggleModel
    {
        public string Key { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        /// <summary>
        /// Setting id
        /// </summary>
        public Guid Id { get; set; }
    }
}
