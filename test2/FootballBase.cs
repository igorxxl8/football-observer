using System.Collections.Generic;
using System.Linq;

namespace FootballManager
{
    public static class Base
    {
        private static List<Club> clubs = new List<Club>();
        private static List<Player> players = new List<Player>();
        private static List<League> leagues = new List<League>();
        public static Club freeClub = new Club() { Name = "Свободный агент" };
        public static League freeLeague = new League() { Name = "Вне лиги" };

        public static List<Club> Clubs { get => clubs; set => clubs = value; }
        public static List<Player> Players { get => players; set => players = value; }
        public static List<League> Leagues { get => leagues; set => leagues = value; }

        public static List<Player> ReadAllPlayers => Players;
        public static List<Club> ReadAllClubs => Clubs;
        public static List<League> ReadAllLeagues => Leagues;

        public static List<Player> SelectionParallel(List<string> list)
        {
            IEnumerable<Player> temp = null;
            List<Player> evens = new List<Player>();
            bool isFirst = true;
            foreach (var filter in list)
            {
                if (isFirst)
                {
                    temp = players.AsParallel().Where(i => i.Club == freeClub);
                    isFirst = false;
                }
                foreach (var item in Clubs)
                {
                    if (item.Name == filter)
                    {
                        temp = players.AsParallel().Where(i => i.Club == item);
                    }
                }
                evens.AddRange(temp.ToList());
            }
            return evens;
        }

        public static IEnumerable<Player> OrderingParallel(bool isByPass, List<Player> pl)
        {
            IEnumerable<Player> sortedPlayer;
            if (pl.Count != 0)
            {
                if (isByPass) sortedPlayer = pl.AsParallel().OrderBy(i => i.Goals);
                else sortedPlayer = pl.AsParallel().OrderByDescending(i => i.Goals);
            }
            else
            {
                if (isByPass) sortedPlayer = Players.AsParallel().OrderBy(i => i.Goals);
                else sortedPlayer = Players.AsParallel().OrderByDescending(i => i.Goals);
            }
            return sortedPlayer;
        }

        public static List<Player> GroupingParallel(List<Player> list)
        {
            List<Player> temp = new List<Player>();
            if (list.Count != 0)
            {
                var playerGroup = list.AsParallel().GroupBy(i => i.Position)
                                           .Select(g => new
                                           {
                                               Name = g.Key,
                                               Count = g.Count(),
                                               Players = g.Select(i => i)
                                           });
                foreach (var group in playerGroup)
                {
                    foreach (Player player in group.Players) temp.Add(player);
                }
            }
            else
            {
                var playerGroup = Players.AsParallel().GroupBy(i => i.Position)
                                           .Select(g => new
                                           {
                                               Name = g.Key,
                                               Count = g.Count(),
                                               Players = g.Select(i => i)
                                           });
                foreach (var group in playerGroup)
                {
                    foreach (Player player in group.Players) temp.Add(player);
                }
            }
            return temp;
        }

        public static List<Player> AgregatingParallel(bool c, List<Player> pl)
        {
            int Max = 0, Min=0;
            List<Player> agregatePlayer = new List<Player>();
            if (pl.Count == 0)
            {
                try
                {
                    if (c) Max = Players.AsParallel().Max(i => i.Goals);
                    else Min = Players.AsParallel().Min(i => i.Goals);
                    foreach (var i in Players)
                    {
                        if (c && i.Goals == Max) agregatePlayer.Add(i);
                        if (!c && i.Goals == Min) agregatePlayer.Add(i);
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    if (c) Max = pl.AsParallel().Max(i => i.Goals);
                    else Min = pl.AsParallel().Min(i => i.Goals);

                    foreach (var i in pl)
                    {
                        if (c && i.Goals == Max) agregatePlayer.Add(i);
                        if (!c && i.Goals == Min) agregatePlayer.Add(i);
                    }
                }
                catch { }
            }
            return agregatePlayer;
        }
    }
}
