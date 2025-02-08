using System;
using Raylib_cs;

public class Program {
    public static void Main() {
        Raylib.InitWindow(800, 400, "Hello World");

        int recPosx = 20;
        double recPosy = 12;

        double rec2Posy = 12;
        Rectangle rec = new Rectangle(recPosx, (float)recPosy, 20, 60);
        Rectangle rec2 = new Rectangle(760, (float)rec2Posy, 20, 60);

        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);

            Raylib.DrawRectangleRec(rec, Color.Black);
            Raylib.DrawRectangleRec(rec2, Color.Black);

            if (Raylib.IsKeyDown(KeyboardKey.S)) {
               recPosy += 0.1;
            } else if (Raylib.IsKeyDown(KeyboardKey.W)) {
                recPosy -= 0.1;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down)) {
                rec2Posy += 0.1;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Up)) {
                rec2Posy -= 0.1;
            }

            if (recPosy < 0) {
                recPosy = 0;
            } else if (recPosy + 60 > Raylib.GetScreenHeight()) {
                recPosy = Raylib.GetScreenHeight() - 60;
            }

            if (rec2Posy < 0) {
                rec2Posy = 0;
            }
            else if (rec2Posy + 60 > Raylib.GetScreenHeight()) {
                rec2Posy = Raylib.GetScreenHeight() - 60;
            }

            rec2.Y = (float)rec2Posy;
            rec.Y = (float)recPosy;

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
