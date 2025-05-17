using static NeonPremierLeague.NeonHelper;

namespace NeonPremierLeague
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numberOfTeams = NumberOfClubs;   // get the value once, use it many times in the loop
            var teams = GetAllFantasyTeams().ToArray();

            var players = GetAllPlayers().ToList();

            // Create an array of teams
            for (int i = 0; i < numberOfTeams; i++)
            {
                Team team = new Team(teams[i]);

                while (team.PlayerCount < Team.MaxPlayers)
                {

                    foreach (var player in players)
                    {
                        var availablePlayers = new List<Player>(players.Where(x => string.IsNullOrWhiteSpace(x.AssignedTeam )));
                        
                        // Add players to the team
                        Console.WriteLine($"Enter the Player to add to {team.TeamName}" + Environment.NewLine);

                        var playerName = Console.ReadLine();

                        try
                        {
                            var newPlayer = players.First(p => p.Name == playerName);

                            // this will check rules for adding players to the team
                            team.AddPlayer(newPlayer);

                            availablePlayers.Remove(newPlayer); // remove the player from the list of available players
                            Console.WriteLine($"Player Name: {newPlayer.Name} added to {team.TeamName}" + Environment.NewLine);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }

                // Display the team details
                Console.WriteLine($"Team Name: {team.TeamName}");
                Console.WriteLine($"Total Value: {team.TotalValue}");
                Console.WriteLine($"Player Count: {team.PlayerCount}");
                Console.WriteLine($"Average Performance Rating: {team.TeamPerformanceAveragelRating}");
                Console.WriteLine("Players in the team:");
                foreach (var player in team.Players)
                {
                    Console.WriteLine($"Player Mame - {player.Name} (Club: {player.Club}, Value: {player.TransferValue}, Performance Rating: {player.PerformanceRating})");
                }

            }
        }
    }
}
