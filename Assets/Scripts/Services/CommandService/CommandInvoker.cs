using System.Collections.Generic;

namespace Services.CommandService
{
public class CommandInvoker
{
    public Stack<ICommand> Commands { get; }
    
    public readonly Stack<ICommand> undoStack = new();

    public readonly Stack<ICommand> redoStack = new();

    public CommandInvoker()
    {
        Commands = new Stack<ICommand>();
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);

        redoStack.Clear();
    }

    public void UndoCommand()
    {
        if (undoStack.Count > 0)
        {
            var activeCommand = undoStack.Pop();
            redoStack.Push(activeCommand);
            Commands.Push(activeCommand);
            activeCommand.Undo();
        }
    }
    
    public void UndoCommandAll()
    {
        while (undoStack.Count > 0)
            UndoCommand();
    }

    public void RedoCommand()
    {
        if (redoStack.Count > 0)
        {
            var activeCommand = redoStack.Pop();
            undoStack.Push(activeCommand);
            Commands.Push(activeCommand);
            activeCommand.Execute();
        }
    }

    public void Reset()
    {
        Commands.Clear();
        undoStack.Clear();
        redoStack.Clear();
    }
}
}