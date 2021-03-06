﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    public static class StringExtension
    {
        private static readonly string[] Lower = new string[] { "of", "the", "a", "an", "in" };
        public static string ToProper(this string p)
        {
            string[] words = p.ToLower().Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (i > 0 && Lower.Contains(words[i]))
                {
                    continue;
                }
                if (words[i].Length > 1)
                {
                    words[i] = words[i].Remove(1).ToUpper() + words[i].Substring(1);
                }
                else if (words[i].Length > 0)
                {
                    words[i] = words[i].ToUpper();
                }
            }
            return string.Join(" ", words);
        }
    }
}