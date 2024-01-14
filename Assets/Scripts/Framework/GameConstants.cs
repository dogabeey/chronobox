using UnityEngine;

public struct Const
{
    public struct GameEvents
    {
        public static string ENTITY_MOVED = "ENTITY_MOVED";
        public static string CREATURE_DEATH = "CREATURE_DEATH";
        public static string COLLECTIBLE_EARNED = "COLLECTIBLE_EARNED";
        public static string OBJECTIVE_COMPLETED = "OBJECTIVE_COMPLETED";
        public static string OBJECTIVE_FAILED = "OBJECTIVE_FAILED";
        public static string LEVEL_COMPLETED = "LEVEL_COMPLETED";
        public static string LEVEL_FAILED = "LEVEL_FAILED";
        public static string LEVEL_STARTED = "LEVEL_STARTED";

        public static string PLAYER_ENTERED_RANGE = "PLAYER_ENTERED_RANGE";
        public static string PLAYER_EXITED_RANGE = "PLAYER_EXITED_RANGE";
    }
}