namespace TileMapEngine.CoreEngine.ConsoleCommands;

public class ConsoleCommandsManager
{
    private readonly List<ConsoleCommand> _availableCommands = new();

    public void Init()
    {
        var help = new ConsoleCommand("help",
            "Show all available commands. example: /help",
            false,
            _ => PrintAllCommands());
        AddCommand(help);

        var select = new ConsoleCommand("select",
            "Select a tile object at position (x,y). example: /select 3,5",
            true,
            args => HandleSelectCommand(args));
        AddCommand(select);
        
        var deselect = new ConsoleCommand("deselect",
            "Deselect the current tile object. example: /deselect",
            false,
            _ => Console.WriteLine($"Deselecting current tile objet"));
        AddCommand(deselect);
        
        var move = new ConsoleCommand("move",
            "Move the selected tile object (if possible) to position (x,y). example: /move 0,1",
            true,
            args => Console.WriteLine($"Moving selected tile object to {args}"));
        AddCommand(move);
        
        var quit = new ConsoleCommand("quit",
            "Closes the game's application. example: /quit",
            true,
            _ => ConsoleGameLoopManager.StopGameLoop());
        AddCommand(quit);
    }

    public void AddCommand(ConsoleCommand command) => _availableCommands.Add(command);

    private void PrintAllCommands()
    {
        foreach (var command in _availableCommands)
        {
            Console.WriteLine(command.ToString());
        }
    }

    public void WaitForInput()
    {
        Console.WriteLine();
        Console.Write("Waiting for command: ");
        var input = Console.ReadLine();
        var splitInput = input?.Split(' ');
        
        var firstWordInInput = splitInput?[0];
        var args = "";
        
        if (splitInput?.Length > 1)
        {
            args = splitInput[1];
        }
        foreach (var command in _availableCommands.Where(command => $"/{command.Command}" == $"{firstWordInInput}"))
        {
            HandleCommandInput(command, args);
        }
    }

    private void HandleCommandInput(ConsoleCommand command, string args)
    {
        if (command.HasArgument)
        {
            command.Execute(args);
            return;
        }
        
        command.Execute();
    }
    
    private void HandleSelectCommand(string args)
    {
        
    }
}