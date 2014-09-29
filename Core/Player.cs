namespace Core
{
    public class Player
    {
        private int _points;

        // Ut i enum?
        private const int PointsForVictory = 3;
        private const int PointsForDraw = 1;
        private const int PointsForLoss = 0;

        public Player(string name, string team)
        {
            Name = name;
            Team = team;
            Games = 0;
            Won = 0;
            Draw = 0;
            Loss = 0;
            GoalsForwarded = 0;
            GoalsAllowed = 0;
            Points = 0;
        }

        public string Name { get; set; }
        public string Team { get; set; }
        public int Games { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Loss { get; set; }
        public int GoalsForwarded { get; set; }
        public int GoalsAllowed { get; set; }
        public int GoalDifference
        {
            get { return GoalsForwarded - GoalsAllowed; }
        }
        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                _points = Won * PointsForVictory + Draw * PointsForDraw + Loss * PointsForLoss;
            }
        }
    }
}
