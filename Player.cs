namespace NeonPremierLeague
{
    internal class Player
    {
        /// <summary>
        /// The name of the player (e.g. Player1_Arsenal).
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public int TransferValue { get; set; }

        /// <summary>
        /// The name of the club that the player plays for (e.g. Bradford City).
        /// </summary>
        public string Club { get; set; } = string.Empty;

        /// <summary>
        /// The name of the Fantasy league team that the player is assigned to (e.g. The Mighty Bantams).
        /// </summary>
        public string AssignedTeam { get; set; } = string.Empty;

        public double PerformanceRating { get; set; } // 0 to 10 scale, half a point increments
    }
}
