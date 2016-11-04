using System;

namespace KittenMafiaBlackJack
{
    public class KittenTools
    {
        public static string GetInputString()               //static as its not going to do anything else and dont want to init Kittentools at anytime...
        {
            var choice = Console.ReadKey();

            return choice != null
                ? choice.Key.ToString().ToLower()
                : null;
        }
    }
}
