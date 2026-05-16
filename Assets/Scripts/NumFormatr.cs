using System;
using UnityEngine;

public class NumFormatr
{
    static readonly string[] names =
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

    public static string frmtDob(double n)
    {
        if (n > 9.99e+302)
            return "Infinity";

        if (n < 1000)
            return n.ToString("0");

        int tier = 0;

        while (n >= 1000 && tier < names.Length - 1)
        {
            n /= 1000;
            tier++;
        }

        return n.ToString("0.##") + names[tier];
    }
}
