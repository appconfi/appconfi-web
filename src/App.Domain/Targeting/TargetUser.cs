using System.Collections.Generic;

namespace App.Domain
{
    public class TargetUser : Dictionary<string, string>
    {
        public TargetUser(string id)
        {
            Add("identifier", id);
            Add("id", id);
        }
        public TargetUser(IDictionary<string, string> properties)
        {
            foreach (var p in properties)
            {
                Add(p.Key, p.Value);
            }
        }
    }
}
