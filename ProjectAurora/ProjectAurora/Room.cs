namespace ProjectAurora
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();

        private List<Item> Items { get; } = new List<Item>();

        private NPC Npc { get; set; }

        public Room(string shortDesc, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
        }

        // Method to put an item into the room
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public Item? GetItem(string itemName)
        {
            foreach (Item item in Items)
            {
                if (item.Name.ToLower() == itemName.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        public bool RemoveItem(string itemName)
        {
            Item? itemToRemove = GetItem(itemName);
            if (itemToRemove != null)
            {
                return Items.Remove(itemToRemove);
            }
            return false;
        }


        // Method to get the exits as a formatted string
        public string GetExitString()
        {
            string exitString = "Exits:";
            foreach (string direction in Exits.Keys)
            {
                exitString += " " + direction;
            }
            return exitString;
        }

        // Method to get the items as a formatted string
        public string GetItemString()
        {
            if (Items.Count == 0)
            {
                return "";
            }

            string itemString = "\nItems in the room:";
            foreach (Item item in Items)
            {
                itemString += "\n  - " + item.GetLongDescription();
            }
            return itemString;
        }

        // Method to combine description, exits, and items
        public string GetLongDescription()
        {
            return ShortDescription + "\n" + GetExitString() + GetItemString();
        }

        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
    }
}
