namespace NeonPremierLeague
{
    internal class NeonHelper
    {
        /// <summary>
        /// The number of clubs in the league.
        /// </summary>
        public const int NumberOfClubs = 10;

        /// <summary>
        /// The maximum number of players that can be assigned to a single team from one club.
        /// </summary>
        public const int MaxNumberOfPlayersFromSameClub = 3;

        public static IEnumerable<string> GetAllClubs()
        {
            return new List<string>
            {
                "Arsenal",
                "AstonVilla",
                "BradfordCity", // wishful thinking!!!
                "Everton",
                "Liverpool",
                "ManchesterCity",
                "ManchesterUnited",
                "NewcastleUnited",
                "NottinghamForest",
                "TottenhamHotspur"
            };
        }

        public static IEnumerable<string> GetAllFantasyTeams()
        {
            var teamPrefixes = new List<string>();
            for (int i = 1; i <= 15; i++)
            {
                teamPrefixes.Add("Team" + i);
            }

            return teamPrefixes;
        }

        public static IEnumerable<Player> GetAllPlayers()
        {
            var clubs = GetAllClubs();
            var playerPrefixes = GetPlayerPrefixes();
            var players = new List<Player>();

            foreach (var club in clubs)
            {
                var playersForClub = playerPrefixes
                    .Select(prefix => new Player
                    {
                        Name = prefix + "_" + club,
                        TransferValue = new Random().Next(1000000, 10000000),// Random value between 1 million and 10 million
                        Club = club
                    })
                    .ToList();

                players.AddRange(playersForClub);
            }

            return players;
        }

        public static IEnumerable<string> GetPlayerPrefixes()
        {
            var playerPrefixes = new List<string>();
            for (int i = 1; i <= 15; i++)
            {
                playerPrefixes.Add("Player" + i);
            }

            return playerPrefixes;        
        }

    }
}
