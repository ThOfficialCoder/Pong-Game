using System;
using System.Numerics;
using Raylib_cs;

public class Program {

    static int selectedItem = 0;
    static string[] menuItems = { "Start Game", "Options", "Quit" };
    static bool inGame = false;

    static double recPosY = 12;
    static double rec2PosY = 12;
    static Vector2 ballPos = new Vector2(400, 300);
    static Vector2 ballVel = new Vector2(4f, 4f); // Increased speed for better gameplay
    static float ballRadius = 10;

    static Rectangle rec = new Rectangle(20, (float)recPosY, 20, 60);
    static Rectangle rec2 = new Rectangle(760, (float)rec2PosY, 20, 60);

    public static void Main() {
        Raylib.InitWindow(800, 400, "Hello World");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose()) {
            
            HandleInput();

            if (inGame) {
                UpdateGame();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);

            if (inGame) {
                DrawGame();
            } else {
                DrawMenu();
            }

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    static void UpdateGame() {

        ballPos += ballVel;;

        if (Raylib.IsKeyDown(KeyboardKey.S)) {
            recPosY += 5;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.W)) {
            recPosY -= 5;
        }

        if (Raylib.IsKeyDown(KeyboardKey.Down)) {
            rec2PosY += 5;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Up)) {
            rec2PosY -= 5;
        }

        recPosY = Math.Clamp(recPosY, 0, Raylib.GetScreenHeight() - 60);
        rec2PosY = Math.Clamp(rec2PosY, 0, Raylib.GetScreenHeight() - 60);

        rec2.Y = (float)rec2PosY;
        rec.Y = (float)recPosY;

        if (ballPos.Y - ballRadius < 0 || ballPos.Y + ballRadius > Raylib.GetScreenHeight()) {
            ballVel.Y *= -1;
        }

        if (Raylib.CheckCollisionCircleRec(ballPos, ballRadius, rec)) {
            ballVel.X *= -1; // Reverse horizontal velocity
        }

        if (Raylib.CheckCollisionCircleRec(ballPos, ballRadius, rec2)) {
            ballVel.X *= -1; // Reverse horizontal velocity
        }

        // Ball out of bounds (left or right)
        if (ballPos.X - ballRadius < 0 || ballPos.X + ballRadius > Raylib.GetScreenWidth()) {
            // Reset ball position
            ballPos = new Vector2(400, 300);
            ballVel = new Vector2(4f, 4f);
        }
    }

    static void DrawGame() {
        Raylib.DrawRectangleRec(rec, Color.Black);
        Raylib.DrawRectangleRec(rec2, Color.Black);

        // Draw ball

        Raylib.DrawCircleV(ballPos, ballRadius, Color.Black);
    }

    static void HandleInput() {

        if (inGame) {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape)) {
                inGame = false;
            }
        }
        else {
            if (Raylib.IsKeyPressed(KeyboardKey.Down)) {
                selectedItem = (selectedItem + 1) % menuItems.Length;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.Up)) {
                selectedItem = (selectedItem - 1 + menuItems.Length) % menuItems.Length;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Enter)) {
                SelectMenuItem(selectedItem);
            }
        }  
    }

    static void DrawMenu() {
        int startY = 150;
        int itemHeight = 50;

        for (int i = 0; i < menuItems.Length; i++) {
            Color textColor = (i == selectedItem) ? Color.Yellow : Color.White;

            Raylib.DrawText(
                menuItems[i],
                300,
                startY + i * itemHeight,
                30,
                textColor
                );
        }
    }

    static void SelectMenuItem(int itemIndex) {
        switch(itemIndex) {
            case 0:
                inGame = true;
                break;
            case 1:
                break;
            case 2:
                Raylib.CloseWindow();
                Environment.Exit(0);
                break;
        }
    }
}
