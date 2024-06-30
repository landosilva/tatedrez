using UnityEngine;

namespace Lando.Plugins.Sound
{
    public partial class SoundDatabase
    {
        public static class Music
        {
            public class BackgroundData
            {
                public static AudioClip mus_game_tatedrez => Instance._categories[0].Audios[0].Clips[0];

                public static implicit operator AudioData(BackgroundData _) =>
                    Instance._categories[0].Audios[0];
            }
            public static BackgroundData Background => new BackgroundData();
        }
        public static class Piece
        {
            public class HoldData
            {
                public static AudioClip sfx_game_piece_pickUp_01 => Instance._categories[1].Audios[0].Clips[0];

                public static implicit operator AudioData(HoldData _) =>
                    Instance._categories[1].Audios[0];
            }
            public static HoldData Hold => new HoldData();
            public class PlaceData
            {
                public static AudioClip sfx_game_piece_release_01 => Instance._categories[1].Audios[1].Clips[0];

                public static implicit operator AudioData(PlaceData _) =>
                    Instance._categories[1].Audios[1];
            }
            public static PlaceData Place => new PlaceData();
        }
        public static class Game
        {
            public class TurnStartData
            {
                public static AudioClip sfx_game_playerTurn_start_01 => Instance._categories[2].Audios[0].Clips[0];

                public static implicit operator AudioData(TurnStartData _) =>
                    Instance._categories[2].Audios[0];
            }
            public static TurnStartData TurnStart => new TurnStartData();
            public class GameOverData
            {
                public static AudioClip sfx_game_end_01 => Instance._categories[2].Audios[1].Clips[0];

                public static implicit operator AudioData(GameOverData _) =>
                    Instance._categories[2].Audios[1];
            }
            public static GameOverData GameOver => new GameOverData();
        }
    }
}
