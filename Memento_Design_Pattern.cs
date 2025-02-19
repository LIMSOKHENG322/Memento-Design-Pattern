using System;
using System.Collections.Generic;

// Memento Class
class Memento
{
    private string savedContent;
    
    public Memento(string content)
    {
        savedContent = content;
    }
    
    public string GetSavedContent()
    {
        return savedContent;
    }
}

// Originator Class
class Originator
{
    private string content;
    
    public void Write(string text)
    {
        content = text;
    }
    
    public Memento Save()
    {
        return new Memento(content);
    }
    
    public void Restore(Memento memento)
    {
        content = memento.GetSavedContent();
    }
    
    public void ShowContent()
    {
        Console.WriteLine("Current Content: " + content);
    }
}

// Caretaker Class
class Caretaker
{
    private List<Memento> mementos = new List<Memento>();
    
    public void SaveState(Memento mem)
    {
        mementos.Add(mem);
    }
    
    public void Undo(Originator originator)
    {
        if (mementos.Count > 0)
        {
            Memento lastState = mementos[mementos.Count - 1];
            mementos.RemoveAt(mementos.Count - 1);
            originator.Restore(lastState);
        }
        else
        {
            Console.WriteLine("No saved states to restore.");
        }
    }
}

// Client Code
class Program
{
    static void Main()
    {
        Originator originator = new Originator();
        Caretaker caretaker = new Caretaker();
        
        originator.Write("State 1");
        originator.ShowContent();
        caretaker.SaveState(originator.Save());
        
        originator.Write("State 2");
        originator.ShowContent();
        caretaker.SaveState(originator.Save());
        
        originator.Write("State 3");
        originator.ShowContent();
        
        Console.WriteLine("Undoing...");
        caretaker.Undo(originator);
        originator.ShowContent();
        
        caretaker.Undo(originator);
        originator.ShowContent();
    }
}
