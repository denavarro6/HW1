using System;

namespace HW1
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new HW1Game())
                game.Run();
        }
    }
}
