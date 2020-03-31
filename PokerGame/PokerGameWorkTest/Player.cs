using System.Text;

namespace PokerGameWorkTest
{
    public class Player
    {
        public string Name { get; set; }
        public string Hand { get; set; }
        public Hand HandStrength { get; set; }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendLine(Name + ": ");
            s.Append("Cards: ");
            foreach (var card in Hand)
            {
                s.Append(card + " ");
            }
            s.AppendLine("\nResult: " + HandStrength.Description);
            s.AppendLine();

            return s.ToString();
        }
    }
}
