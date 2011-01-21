using System;

namespace CobaltAlchemy
{
    static class FMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            FVector2D vector = new FVector2D(0, 1);
            using (FManager game = new FManager())
            {
                game.Run();
            }
        }
    }
}

