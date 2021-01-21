using System.Collections.Generic;

namespace App.Web.Core.Models
{
    public class ConfirmationModalModel
    {
        public ConfirmationModalModel()
        {
            PositiveText = "Yes";
            NegativeText = "No";
            Title = "Are you sure";
            RouteData = new Dictionary<string, string>();
            ModalId = "confirmationModal";
        }
        public string Controller { get; set; }

        public string Action { get; set; }

        public IDictionary<string, string> RouteData { get; set; }
        public string ModalId { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public string PositiveText { get; set; }

        public string NegativeText { get; set; }
    }
}
