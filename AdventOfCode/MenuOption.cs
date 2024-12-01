using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
internal class MenuOption
{
    public string Description { get; }
    private readonly Action _action;
    private readonly Menu _submenu;
    public Menu Submenu => _submenu;

    public MenuOption(string description, Action action = null, Menu submenu = null)
    {
        Description = description;
        _action = action;
        _submenu = submenu;
    }

    public void Execute()
    {
        if (_submenu != null)
        {
            _submenu.Display();
        }
        else
        {
            _action?.Invoke();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}