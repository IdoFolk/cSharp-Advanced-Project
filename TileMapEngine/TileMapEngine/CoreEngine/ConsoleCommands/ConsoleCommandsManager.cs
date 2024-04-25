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
            args => Console.WriteLine($"Selecting tile object at {args}"));
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
    }
    
    public void AddCommand(ConsoleCommand command) => _availableCommands.Add(command);

    public void PrintAllCommands()
    {
        foreach (var command in _availableCommands)
        {
            Console.WriteLine(command.ToString());
        }
    }

    public void WaitForInput()
    {
        var input = Console.ReadLine();
        var firstWordInInput = input?.Split(' ')[0];

        Console.WriteLine(firstWordInInput);
        foreach (var command in _availableCommands.Where(command => $"/{command.Command}" == $"{firstWordInInput}"))
        {
            HandleCommandInput(command, input);
        }
    }

    private void HandleCommandInput(ConsoleCommand command, string? input)
    {
        var args = input.Split(' ')[1];
        
        if (command.HasArgument)
        {
            command.Execute(args);
            return;
        }
        
        command.Execute();
    }
}