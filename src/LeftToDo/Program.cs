using System;

namespace LeftToDo
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleMeny = new ConsoleMeny(); // Ny instans av ConsoleMeny.
            consoleMeny.UserInterface(); // Anropar metod som startar menyn.
        }
    }
}
