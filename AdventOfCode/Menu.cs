using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
internal class Menu
{
    private readonly string _title;
    private readonly List<MenuOption> _options;
    private Menu? parent;
    private int _selectedOption;

    public Menu(string title, List<MenuOption> options)
    {
        _title = title;
        _options = options;
        if(_options != null)
        {
            foreach (var o in _options)
            {
                if(o.Submenu is not null)
                {
                    o.Submenu.parent = this;
                }
            }
        }
    }

    public void Display()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine(_title);
            Console.WriteLine();

            for (int i = 0; i < _options.Count; i++)
            {
                if (_selectedOption == i)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"> {_options[i].Description}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {_options[i].Description}");
                }
            }

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _selectedOption = Math.Max(0, _selectedOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    _selectedOption = Math.Min(_options.Count - 1, _selectedOption + 1);
                    break;
                case ConsoleKey.Enter:
                    _options[_selectedOption].Execute();
                    break;
                case ConsoleKey.Escape:
                    parent?.Display();
                    break;
            }
        }
    }
}
