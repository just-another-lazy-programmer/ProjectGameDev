using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    public class SoundManager
    {
        private readonly ContentManager contentManager;
        private readonly Dictionary<MusicType, List<Song>> music = new();
        private readonly Dictionary<MusicType, int> currentPlaybackIndexes = new();

        private bool isPlaying = false;
        private MusicType currentPlayType = MusicType.None;
        private Song currentSong;

        public SoundManager(DependencyManager dependencyManager)
        {
            dependencyManager.InjectChecked(ref contentManager);

            Initialize();
        }

        private void Initialize()
        {
            music.Add(MusicType.Menu, new());
            music.Add(MusicType.Game, new());

            currentPlaybackIndexes.Add(MusicType.Menu, -1);
            currentPlaybackIndexes.Add(MusicType.Game, -1);

            AddSong(MusicType.Menu, "Music/Game/The Builder");
            AddSong(MusicType.Game, "Music/Game/Ether Vox");
            AddSong(MusicType.Game, "Music/Game/Night Vigil");
        }

        public void AddSong(MusicType type, string song)
        {
            music[type].Add(contentManager.Load<Song>(song));
        }

        public void NextSong()
        {
            var index = currentPlaybackIndexes[currentPlayType] + 1;
            var list = music[currentPlayType];

            MediaPlayer.Stop();

            if (index >= list.Count)
                index = 0;

            currentPlaybackIndexes[currentPlayType] = index;

            if (list.Count == 0)
            {
                if (currentSong is not null)
                {
                    MediaPlayer.Stop(); // maybe fade out
                }

                return;
            }

            var song = list[index];
            MediaPlayer.Play(song);
            currentSong = song;
        }

        public void StartMusic(MusicType type)
        {
            //var index = currentPlaybackIndex[type];
            //var list = music[type];
            // if we're already playing another type, switch
            if (isPlaying && currentPlayType != type)
            {
                MediaPlayer.Stop();
            }
            else if (isPlaying)
            {
                return;
            }

            currentPlayType = type;
            isPlaying = true;

            NextSong();
        }

        public void StopMusic()
        {
            isPlaying = false;
            currentSong = null;
            MediaPlayer.Stop();
        }

        public void Update()
        {
            if (isPlaying && MediaPlayer.State == MediaState.Stopped)
            {
                NextSong();
            }
        }
    }

    public enum MusicType
    {
        None,
        Menu,
        Game
    }
}
