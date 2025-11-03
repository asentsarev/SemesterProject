namespace ProjectAurora
{
    public class Game
    {
        private static Room? currentRoom;
        private static Room? previousRoom;
        private static List<Room>? rooms;

        public Game()
        {
            CreateRooms();
            CreateNPCs(rooms);
        }

        public void CreateRooms()
        {

            Room? start = new Room("Aurora Control Hub", "You stand inside the Aurora Control Hub, the heart of the last renewable energy initiative." +
                "\r\nThe air hums with faint backup power. Screens flicker, showing maps of four darkened regions." +
                "\r\nA workbench lies in the corner with scattered tools.\r\n");
            rooms.Add(start);
            Room? solarDesert = new Room("Solar Desert", "The desert stretches before you. Towers of sand cover the solar field. Heat shimmers across the horizon." +
                "\r\nThere you meet Dr. Liora Sunvale");
            rooms.Add(solarDesert);
            currentRoom = start;

        }

        private void CreateNPCs(List<Room> rooms)
        {
            NPC Liora = new NPC("Dr. Liora Sunvale", "", rooms.Find(x => x.ShortDescription == "Solar Desert"));
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

                switch(command.Name)
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

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "talk":

                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing Project Aurora!");
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


        private static void PrintWelcome()
        {
            Console.WriteLine("The year is 2075. The world has fallen into darkness. Cities flicker, villages wait in silence. " +
                "\r\nYou are part of Project Aurora — humanity’s last hope to restore clean power. " +
                "\r\nArmed with your tools, knowledge, and determination, you set out to bring light back.\r\n");

            Console.WriteLine("Your mission: travel to four regions and repair their energy plants. " +
                "\r\nYour choices will shape the future of humanity.\r\n");

            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Navigate by typing the name of the room you would like to go to.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}
