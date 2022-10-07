using System.ComponentModel;

namespace GamesApi.Models
{
    public enum GameGenre
    {
        [Description("Sandbox")]
        Sandbox =0,
        RTS=1,
        Shooters=2,
        MOBA=3,
        RolePplaying=4,
        Simulation=5,
        Puzzlers=6,
        ActionAdventure=7,
        Survival=8,
        Platformer=9
    }
}
