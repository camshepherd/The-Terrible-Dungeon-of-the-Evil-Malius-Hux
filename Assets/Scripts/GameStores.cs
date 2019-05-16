using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStores
{
    public static float playThroughNumber;

    public static Color[] gameColours = { Color.red, Color.blue, Color.green, new Color(255, 255, 0), new Color(255, 0, 255), new Color(0, 255, 255) };
    public static List<Color> currentColours = new List<Color> { Color.red };

    private const int DEFAULT_LENGTH = 4;

    private static int length = DEFAULT_LENGTH;

    private static int depth = 1;

    private static float health = 99;

    private static float defModifier = 2;

    private static float atkMod = 0.4f;

    private static int gameComplete = 0;

    static GameStores()
    {
        playThroughNumber = 0;
    }

    public static List<Color> AvailableColours()
    {
        List<Color> output = new List<Color>();

        foreach (Color c in gameColours)
        {
            if (!currentColours.Contains(c))
            {
                output.Add(c);
            }
            //if(!current.Contains(c))
            //{
            //    output.Add(c);
            //}
        }
        return output;
    }

    public static List<Vector3> GetPlayerColours()
    {
        List<Vector3> output = new List<Vector3>();

        foreach (Color c in currentColours)
        {
            output.Add(new Vector3(c.r, c.g, c.b));
        }
        return output;
    }

    public static void AddColour(Vector3 c)
    {
        currentColours.Add(new Color(c.x, c.y, c.z));
    }

    public static void ResetColours()
    {
        currentColours = new List<Color> { Color.red };
    }

    public static int GetLength() { return length; }

    public static void ResetLength() { length = DEFAULT_LENGTH; }

    public static void IncrementLength() { length++; }

    public static void ResetState()
    {
        ResetLength();
        ResetColours();
        ResetHealth();
        ResetDefMod();
        ResetAtkMod();
        ResetDepth();
    }

    public static int GetDepth()
    {
        return depth;
    }

    public static void ResetDepth()
    {
        depth = 1;
    }

    public static void IncrementDepth()
    {
        depth++;
    }

    public static void ResetHealth()
    {
        health = 99;
    }

    public static float GetHealth()
    {
        return health;
    }

    public static void SetHealth(float value)
    {
        health = value;
    }

    public static float GetDefMod()
    {
        return defModifier;
    }

    public static void SetDefMod(float value) { defModifier = value; }
    public static void ResetDefMod() { defModifier = 2; }

    public static float GetAtkMod() { return atkMod; }
    public static void SetAtkMod(float value) { atkMod = value; }
    public static void ResetAtkMod() { atkMod = 0.4f; }

    public static void addGameComplete() { gameComplete += 1; }
    public static int getGameComplete() { return gameComplete; }
}
