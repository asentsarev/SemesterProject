namespace ProjectAurora
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private bool hasDamKey = false;

        private Room? damplant;
        private Room? controlroom;
        public Game()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {

            //main hydro rooms, sub hydro rooms and their items
            Room? hub = new("Hydro Hub", "You started walking towards the river. But you reach an area where the road separates into 4. Luckily there is a sign that reads =Welcome, you are at the Hydro Hub= /n = Outside(south), Research Center(north), Hydroelectric dam(east), Tundra forrest(west)=");
            damplant = new("The Dam Plant", "After a short stroll you arrive at the riverside with no bridge leading acoross. However there is a an obstruction that connects the two sides: A huge Hydroelectric Plant. And you see an enterance, that you can enter(inside) however there is a lock on the door");
            Room? researchcenter = new("Research Center", "You enter inside the lobby of an Aurora outpost, this building gives help and guidiance to engineers on their missions, it has 4 sections: a Library(up), the Cafeteria(right), the Lab(left) and an ominous Basement(down). The way back to the Hydro Hub is South");
            Room? resourcearea = new("Tundra Forrest", "You have entered the Tundra forrest.");
            Item berries = new("berries", "A cluster of edible-looking berries. Maybe they could be used as bait or food.");
            resourcearea.AddItem(berries);
            Item pinecone = new("pinecone", "A large, sticky pinecone. It feels heavy, perhaps useful for starting a fire.");
            resourcearea.AddItem(pinecone);
            Room? library = new("Library", "Loads of heavy shelves hold thousands of technical theory, documents and old logbooks. A single chair is occupied by a person reading one of the books. The lobby is downstrairs(down)");
            Room? cafeteria = new("Caferetia", "A regular cafeteria. Near the serving station, you see a small, misplaced item lying among the cutlery. It looks like a key, dou you take it?(take key) It might come in handy later... The lobby is to the Left(left).");            Item damKey = new("key", "A small metal tool with the Aurora symbol on it. It doesn't quite fit the shape of the rest of the spoons and forks, perhaps you should investigate? (take) ");
            cafeteria.AddItem(damKey);             
            Room? hydrolab = new("Hydro Lab", "A workspace cluttered with beakers and other equipment. A researcher in a clean white coat is hard at work. The lobby is to the Right(right).");
            Room? basement = new("Basement", "A dark, seemingly abandoned utility area. The air is cold and heavy. You see a series of locked storage crates. The lobby is Up(up).");
            Room? bonus = new("Top of the Hill", "After climbing up the hill you find a forgotten toolbox. You see a box labeled levers. The only way down is back to the Tundra (south).");
            Item lever = new("lever", "A heavy, stainless steel lever. It looks like it could replace a rusted, jammed control.");
            bonus.AddItem(lever);
            controlroom = new("Control room", "You walk deep inside the dam to the Control room. Directly ahead is the emergency restart control panel with the restart lever marked, however the levers are completely rusted and jammed shut. The only way to go is leaving and going back out to the DampPlant(outside).");


            //area directions from the hydro hub
            hub.SetExit("south", outside); // [Added the reverse way back from the hub to the outside South]
            hub.SetExit("east", damplant);
            hub.SetExit("north", researchcenter);
            hub.SetExit("west", resourcearea);            
            damplant.SetExit("west", hub);
            damplant.SetExit("inside", controlroom);
            controlroom.SetExit("outside", damplant);
            researchcenter.SetExit("south", hub);
            researchcenter.SetExit("up", library);
            researchcenter.SetExit("right", cafeteria);
            researchcenter.SetExit("left", hydrolab);
            researchcenter.SetExit("down", basement);
            library.SetExit("down", researchcenter);
            cafeteria.SetExit("left", researchcenter);
            hydrolab.SetExit("right", researchcenter);
            basement.SetExit("up", researchcenter);
            resourcearea.SetExit("east", hub);
            resourcearea.SetExit("north", bonus);
            bonus.SetExit("south", resourcearea);
            theatre.SetExit("west", outside);
            pub.SetExit("east", outside);
            lab.SetExits(outside, office, null, null);
            office.SetExit("west", lab);
          


        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch (command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.LongDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;

                    case "take":
                        TakeItem(command);
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                    case "right":
                    case "up":
                    case "down":
                    case "left":
                    case "outside":

                        Move(command.Name);
                        break;

                    case "inside":
                        TryMoveInside();
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;
                    


                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        private void TakeItem(Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine("Take what?");
                return;
            }

            string itemName = command.SecondWord;
            Item? itemToTake = currentRoom?.GetItem(itemName);

            if (itemToTake == null)
            {
                Console.WriteLine($"There is no {itemName} here to take.");
                return;
            }

            string itemNameLower = itemToTake.Name.ToLower();

            if (itemNameLower == "key")
            {
            hasDamKey = true; // Still need this flag for the door
            currentRoom?.RemoveItem(itemToTake.Name);
            Console.WriteLine($"You take the {itemToTake.Name}. Its weight is noticeable.");
            }
            else if (itemNameLower == "lever" || itemNameLower == "berries" || itemNameLower == "pinecone")
            {
    
    
    currentRoom?.RemoveItem(itemToTake.Name);
    Console.WriteLine($"You take the {itemToTake.Name}.");
    }
    else
    {

    Console.WriteLine($"You can't take the {itemNameLower}.");
    }


        }

       private void TryMoveInside()
        {
            if (currentRoom == damplant) //Check if player is at the right location (ie Dam Plant)
            {
                if (hasDamKey) //check if player has the key
                {
                    previousRoom = currentRoom;
                    currentRoom = controlroom; // Move to the Control Room
                    Console.WriteLine("The key clicks perfectly into a hidden lock next to the entrance.");
                    Console.WriteLine("You turn the key and the heavy door slides open. You step inside.");
                }
                else
                {
                    //if the player doesnt have the hydro key
                    Console.WriteLine("The entrance to the control room is sealed shut. It looks like it requires a specialized key.");
                }
            }
            else //if the player is not at the Dam Plant
            {
                Console.WriteLine("You can only go 'inside' the control room from the Dam Plant.");
            }
        }


       
    }

public class Item
{
    private string name;
    private string description;

    public string Name { get { return name; } }
    public string Description { get { return description; } }

    //items 
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
