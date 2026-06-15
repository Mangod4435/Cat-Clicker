using System.Collections.Generic;

namespace UpgradePharsing
{
    class Tokenizer
    {
        public static string[] tokenize(string input)
        {
            var tokens = new List<string>();
            var currentToken = "";
            bool inQuotes = false;

            foreach (char c in input)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }
                if (c == ' ' && !inQuotes)
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                }
                else
                    currentToken += c;
            }
            if (currentToken.Length > 0)
                tokens.Add(currentToken);
            return tokens.ToArray();
        }
    }

    class UPDFunction
    {
        public string type;
    }
}
