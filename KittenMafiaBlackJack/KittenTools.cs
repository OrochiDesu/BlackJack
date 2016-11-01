using System;

namespace KittenMafiaBlackJack
{
    public class KittenTools
    {
        public static string GetInputString()
        {
            var choice = Console.ReadKey();

            return choice != null
                ? choice.Key.ToString().ToUpper()
                : null;
        }
    }
}
