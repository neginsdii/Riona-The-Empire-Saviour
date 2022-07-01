//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   Data.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/29
///   Description       : Data manager 
///   Revision History  : 4th ed. Joystick sensitivity defaults to 1 not 0
///----------------------------------------------------------------------------------

public static class Data
{
    public static bool isWon = false;

    public static int enemyCount = 22;
    public static int totalEnemies = 22;        // Change later

    
    public static int spellBooksCount = 2;
    public static int totalSpellBook = 2;

    public static int score = 0;

    public static float LevelTimer = 0f;

    public static float joystickSensitivity = 1f;
    
    public static void ResetGame()
    {
        // Tutorial
        spellBooksCount = 2;
        totalSpellBook = 2;


        isWon = false;
        enemyCount = 22;
        totalEnemies = 22;
        score = 0;
        LevelTimer = 0f;
    }
}