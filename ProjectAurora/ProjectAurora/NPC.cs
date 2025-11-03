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
        
        public string? LongDialog { get; private set; }

        public NPC(string name, string dialog)
        {
            Name = name;
            Dialog = dialog;
        }

        public NPC(string name, string dialog, string longDialog) 
        {
            Name = name;
            Dialog = dialog;
            LongDialog = longDialog;
        }

        public void PrintDialog(string Name)
        {
            Console.WriteLine(Dialog);
        }
    }
}
