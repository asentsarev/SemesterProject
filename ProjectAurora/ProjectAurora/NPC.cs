using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAurora
{
    public class NPC
    {
        public string Name { get; private set; }
        public string Dialog { get; private set; }
        
        public Room Room { get; private set; }

        public NPC(string name, string dialog, Room room)
        {
            Name = name;
            Dialog = dialog;
            Room = room;
        }

        public void PrintDialog(string Name)
        {
            Console.WriteLine(Dialog);
        }
    }
}
