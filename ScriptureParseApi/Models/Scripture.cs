using System.Text;

namespace ScriptureParseApi.Models
{
    public class Scripture
    {
        public string Book { get; set; }
        public int StartChapter { get; set; }
        public int? StartVerse { get; set; }
        public int? EndChapter { get; set; }
        public int? EndVerse { get; set; }

        public Scripture()
        {
            Book = string.Empty;
            StartChapter = 0;
            StartVerse = null;
            EndChapter = null;
            EndVerse = null;
        }

        public Scripture(Scripture scripture)
        {
            Book = scripture.Book;
            StartChapter = scripture.StartChapter;
            StartVerse = scripture.StartVerse;
            EndChapter = scripture.EndChapter;
            EndVerse = scripture.EndVerse;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{Book} ");
            sb.Append($"{StartChapter}");

            if (StartVerse != null)
            {
                sb.Append($":{StartVerse}");
            }

            if (EndChapter != null)
            {
                sb.Append($" - {EndChapter}");
            }

            if (EndVerse != null)
            {
                if (EndChapter == null)
                {
                    sb.Append(" - ");
                }
                else
                {
                    sb.Append(":");
                }

                sb.Append($"{EndVerse}");
            }

            return sb.ToString();
        }
    }
}
