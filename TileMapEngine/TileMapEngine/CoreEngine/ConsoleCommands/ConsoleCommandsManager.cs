namespace TileMapEngine.CoreEngine.ConsoleCommands;

public class ConsoleCommandsManager
{
    private readonly List<ConsoleCommand> _availableCommands = new();

    public void Init()
    {
        var help = new ConsoleCommand("help",
            "Show all available commands. example: /help",
            false,
            _ => HandleHelpCommand());
        AddCommand(help);

        var select = new ConsoleCommand("select",
            "Select a tile object at position (x,y). example: /select 3,5",
            true,
            HandleSelectCommand);
        AddCommand(select);

        var deselect = new ConsoleCommand("deselect",
            "Deselect the current tile object. example: /deselect",
            false,
            _ => HandleDeselectCommand());
        AddCommand(deselect);

        var move = new ConsoleCommand("move",
            "Move the selected tile object (if possible) to position (x,y). example: /move 0,1",
            true,
            HandleMoveCommand);
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
            Console.WriteLine(command);
        }
    }

    public void HandleUserInput()
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
            return;
        }

        HandleHelpCommand();
    }

    private static void HandleCommandInput(ConsoleCommand command, string args)
    {
        if (command.HasArgument)
        {
            command.Execute(args);
            return;
        }

        command.Execute();
    }

    private void HandleHelpCommand()
    {
        Console.WriteLine($"\nBelow are all the possible commands.\n" +
                          $"Please refer to each command's example to learn how to use it:\n");
        PrintAllCommands();
    }

    private void HandleSelectCommand(string args)
    {
        if (!TryGetPositionFromArgumentAndHandleError(args, out var position2D)) { return; }

        Console.WriteLine($"Selecting tile object at {position2D.X},{position2D.Y}"); // TODO change
    }

    private void HandleDeselectCommand()
    {
        Console.WriteLine("Deselecting current tile object"); // TODO change
    }

    private void HandleMoveCommand(string args)
    {
        if (!TryGetPositionFromArgumentAndHandleError(args, out var position2D)) { return; }

        Console.WriteLine($"Moving the selected tile object to {position2D.X},{position2D.Y}"); // TODO change
    }

    private bool TryGetPositionFromArgumentAndHandleError(string? args, out Position2D position2D)
    {
        var splitArgs = args?.Split(',');

        if (splitArgs is { Length: 2 } &&
            int.TryParse(splitArgs[0], out var x) &&
            int.TryParse(splitArgs[1], out var y))
        {
            position2D = new Position2D(x, y);
            return true;
        }

        HandleHelpCommand();
        position2D = default;
        return false;
    }
}