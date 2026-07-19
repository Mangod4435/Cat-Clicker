namespace API
{
    public static class NumberFormatter
    {
        static SettingManager setting = SettingManager.Instance;
        static readonly string[] Names =
        {
            "",
            " thousand",
            " million",
            " billion",
            " trillion",
            " quadrillion",
            " quintillion",
            " sextillion",
            " septillion",
            " octillion",
            " nonillion",
            " decillion",
            " undecillion",
            " duodecillion",
            " tredecillion",
            " quattuordecillion",
            " quindecillion",
            " sexdecillion",
            " septendecillion",
            " octodecillion",
            " novemdecillion",
            " vigintillion",
            " unvigintillion",
            " duovigintillion",
            " tresvigintillion",
            " quattuorvigintillion",
            " quinvigintillion",
            " sesvigintillion",
            " septemvigintillion",
            " octovigintillion",
            " novemvigintillion",
            " trigintillion",
            " untrigintillion",
            " duotrigintillion",
            " trestrigintillion",
            " quattuortrigintillion",
            " quintrigintillion",
            " sestrigintillion",
            " septemtrigintillion",
            " octotrigintillion",
            " novemtrigintillion",
            " quadragintillion",
            " unquadragintillion",
            " duoquadragintillion",
            " tresquadragintillion",
            " quattuorquadragintillion",
            " quinquadragintillion",
            " sesquadragintillion",
            " septenquadragintillion",
            " octoquadragintillion",
            " novemquadragintillion",
            " quinquagintillion",
            " unquinquagintillion",
            " duoquinquagintillion",
            " tresquinquagintillion",
            " quattuorquinquagintillion",
            " quinquinquagintillion",
            " sesquinquagintillion",
            " septenquinquagintillion",
            " octoquinquagintillion",
            " novemquinquagintillion",
            " sexagintillion",
            " unsexagintillion",
            " duosexagintillion",
            " tresexagintillion",
            " quattuorsexagintillion",
            " quinsexagintillion",
            " sessexagintillion",
            " septensexagintillion",
            " octosexagintillion",
            " novemsexagintillion",
            " septuagintillion",
            " unseptuagintillion",
            " duoseptuagintillion",
            " treseptuagintillion",
            " quattuorseptuagintillion",
            " quinseptuagintillion",
            " sesseptuagintillion",
            " septenseptuagintillion",
            " octoseptuagintillion",
            " novemseptuagintillion",
            " octogintillion",
            " unoctogintillion",
            " duooctogintillion",
            " tresoctogintillion",
            " quattuoroctogintillion",
            " quinoctogintillion",
            " sexoctogintillion",
            " septemoctogintillion",
            " octooctogintillion",
            " novemoctogintillion",
            " nonagintillion",
            " unnonagintillion",
            " duononagintillion",
            " tresnonagintillion",
            " quattuornonagintillion",
            " quinnonagintillion",
            " sesnonagintillion",
            " septennonagintillion",
            " octononagintillion",
            " novemnonagintillion",
        };

        public static string FormatDouble(double n)
        {
            if (n > 9.99e+302)
                return "Infinity";

            if (n < 1000)
                return n.ToString("0");

            if (setting.Notation)
                return n.ToString("0.00e0");

            int tier = 0;

            while (n >= 1000 && tier < Names.Length - 1)
            {
                n /= 1000;
                tier++;
            }

            return n.ToString("0.##") + Names[tier];
        }
    }
}
