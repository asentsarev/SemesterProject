using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAurora
{
    public class Item
    {
        private string name;
        private string description;

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public Item(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string GetLongDescription()
        {
            return Name + ": " + Description;
        }
    }
}
