namespace NeonPremierLeague
{
    /// <summary>
    /// Represents a Fantasy league team.
    /// </summary>
    internal class Team
    {
        public const int MaxPlayers = 15;
        public const int MaxValue = 100000000; // 100 million

        public int PlayerCount { get; set; }
        public int TotalValue { get; set; }

        public IEnumerable<Player> Players { get; set; } = new List<Player>();

        /// <summary>
        /// The name of the Fantasy league team.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Calculates the total performance rating of the team.
        /// </summary>
        public double TeamPerformanceTotalRating
        {
            get
            {
                if (PlayerCount == 0)
                {
                    return 0;
                }

                // The error message could be a constant, so that the app logic could be unit-tested against it
                if (PlayerCount > MaxPlayers)
                {
                    throw new InvalidOperationException("Cannot calculate the total team performance rating - Player count exceeds maximum allowed players.");
                }

                return Players.Sum(p => p.PerformanceRating);
            }
        }

        /// <summary>
        /// Calculates the mean performance rating of the team.
        /// </summary>
        public double TeamPerformanceAveragelRating
        {
            get
            {
                if (PlayerCount == 0)
                {
                    return 0;
                }
                return Players.Average(p => p.PerformanceRating);
            }
        }

        public Team(string teamName)
        {
            TeamName = teamName;
            PlayerCount = 0;
            TotalValue = 0;
        }


        public bool AddPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player), "Player cannot be null.");
            }    
            
            if (player.Club == null)
            {
                throw new InvalidOperationException("Player's club cannot be null.");
            }

            if (PlayerCount >= MaxPlayers)
            {
                throw new InvalidOperationException("Cannot add more players to the team.");
            }

            if (TotalValue + player.TransferValue > MaxValue)
            {
                throw new InvalidOperationException($"Adding this player would exceed the maximum team value of {MaxValue}.");
            }

            if (Players.Contains(player))
            {
                throw new InvalidOperationException("Player already exists in the team.");
            }

            var playerPerClubCount = Players.Count(x => x.Club == player.Club);

            if (playerPerClubCount >= NeonHelper.MaxNumberOfPlayersFromSameClub)
            {
                throw new InvalidOperationException($"Cannot add more than {NeonHelper.MaxNumberOfPlayersFromSameClub} players from the same club.");
            }

            ((List<Player>)Players).Add(player);
            player.AssignedTeam = TeamName; // Assign the team to the player

            PlayerCount++;
            TotalValue += player.TransferValue;
            return true;
        }

        public bool RemovePlayer(Player player)
        {
            if (PlayerCount == 0)
            {
                throw new InvalidOperationException("Cannot remove players from an empty team.");
            }

            if (Players.Contains(player))
            {
                ((List<Player>)Players).Remove(player);
                PlayerCount--;
                TotalValue -= player.TransferValue;

                return true;
            }
            else
            {
                throw new InvalidOperationException("Player not found in the team.");
            }
        }

        public bool TransferPlayer(Player outgoingPlayer, Team targetTeam)
        {
            if (targetTeam == null)
            {
                throw new ArgumentNullException(nameof(targetTeam), "Target team cannot be null.");
            }
            if (outgoingPlayer == null)
            {
                throw new ArgumentNullException(nameof(outgoingPlayer), "outgoingPlayer cannot be null.");
            }

            if (!Players.Contains(outgoingPlayer))
            {
                throw new InvalidOperationException($"outgoingPlayer not found in the team {TeamName}.");
            }

            if (targetTeam.Equals(this))
            {
                throw new InvalidOperationException("Cannot transfer player to the same team.");
            }

            if (targetTeam.TotalValue + outgoingPlayer.TransferValue > MaxValue)
            {
                throw new InvalidOperationException("Transferring this player would exceed the maximum team value.");
            }

            if (targetTeam.PlayerCount >= MaxPlayers)
            {
                throw new InvalidOperationException("Target team has reached maximum player count.");
            }

            try
            {
                var success = RemovePlayer(outgoingPlayer);
                if (!success)
                {
                    throw new InvalidOperationException($"Failed to remove player from the current team {TeamName}.");
                }

                // Add the player to the target team
                success = targetTeam.AddPlayer(outgoingPlayer);

                if (success)
                {
                    outgoingPlayer.Club = targetTeam.TeamName; // Update the player's club

                    return success;
                }

                throw new InvalidOperationException($"Failed to add player to the target team {targetTeam.TeamName}.");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
