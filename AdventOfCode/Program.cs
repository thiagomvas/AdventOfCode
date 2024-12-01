using AdventOfCode;
using System.Reflection;

var mainMenu = new Menu("Advent Of Code", [
    new MenuOption("Inputs", submenu: Menus.GetInputYearsMenu()),
    new MenuOption("Problems", submenu: Menus.GetProblemYearsMenu()),
    new MenuOption("Solutions", submenu: Menus.GetSolutionYearsMenu())
    ]);
mainMenu.Display();