﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureParseApi.Models
{
    public static class ScriptureOps
    {
        public static List<Scripture> ParseScriptureFromString(this string unparsedScriptures)
        {
            var scriptures = new List<Scripture>();

            var scriptureSplits = unparsedScriptures.Replace(", ", ";").Replace(",", ";").Split(';');

            foreach (var scripture in scriptureSplits)
            {
                var trimmedScripture = scripture.Trim();

                var cleanedScripture = trimmedScripture.Split(',')[0];

                var passage = new Scripture();

                if (cleanedScripture.Replace("-", string.Empty).Replace(":", string.Empty).IsNumeric()) //This is either a dangling verse, or another chapter
                {
                    if (!cleanedScripture.Contains(":"))//This is just a verse coupling
                    {
                        var chapter = (scriptures.Last().EndChapter != null) ? scriptures.Last().EndChapter : scriptures.Last().StartChapter;
                        if (cleanedScripture.Contains("-"))//This is a verse range
                        {                            
                            var verses = cleanedScripture.Split("-");
                            cleanedScripture = $"{scriptures.Last().Book} {chapter}:{verses[0]}-{chapter}:{verses[1]}";
                        }
                        else
                        {
                            if (scriptures.Last().StartVerse == null && scriptures.Last().EndVerse == null) //Previous entry was chapter sans verses
                            {
                                cleanedScripture = $"{scriptures.Last().Book} {cleanedScripture}";
                            }
                            else
                            {
                                cleanedScripture = $"{scriptures.Last().Book} {chapter}:{cleanedScripture}";
                            }
                        }
                    }
                }

                var bookName = GetBookName(cleanedScripture);
                passage.Book = (bookName.NullOrEmpty()) ? scriptures.Last().Book : bookName;
                cleanedScripture = cleanedScripture.Replace(passage.Book, "").Trim();
                passage.StartChapter = GetStartChapter(cleanedScripture);
                passage.StartVerse = GetStartVerse(cleanedScripture);
                passage.EndChapter = GetEndChapter(cleanedScripture);
                passage.EndVerse = GetEndVerse(cleanedScripture);

                passage.EndChapter = (passage.EndChapter == passage.StartChapter) ? null : passage.EndChapter;

                scriptures.Add(passage);
            }

            return scriptures;
        }

        private static string GetBookName(string scripture)
        {
            var spaces = scripture.Split(' ');

            if (spaces.IsNumeric())
                return null;

            if (int.TryParse(spaces[0], out _)) //1 Cor; 1 Pet etc
            {
                return $"{spaces[0]} {spaces[1]}";
            }

            if (int.TryParse(spaces[0].Replace(":", "").Replace("-", ""), out _)) //Is this simply a second section of another book?
            {
                return string.Empty;
            }

            return spaces[0];
        }

        private static int GetStartChapter(string scripture)
        {
            int val = 0;
            var scriptureRanges = scripture.Split('-'); //Break apart the range
            var chapter = scriptureRanges[0].Split(":")[0]; //Grab the first part of the range (there will always be at least 1, and snag the chapter, even if there's no verses there will be a chapter
            int.TryParse(chapter, out val);

            return val;
        }

        private static int? GetStartVerse(string scripture)
        {
            int val;
            var scriptureRanges = scripture.Split('-'); //Break apart the range
            var chapterVerse = scriptureRanges[0].Split(':');
            var verse = (chapterVerse.Length > 1) ? chapterVerse[1] : null;//If there is a verse, return it

            int.TryParse(verse, out val);

            return (verse == null || val == 0) ? null : (int?)val;
        }

        private static int? GetEndChapter(string scripture)
        {
            int val;
            //Romans 5:8-6:1
            var scriptureRanges = scripture.Split('-');

            if (scriptureRanges.Length == 1) //No range, no end chapter
                return null;

            var endChapter = (scriptureRanges[1].Contains(':') || !scriptureRanges[0].Contains(':')) ? scriptureRanges[1].Split(':')[0] : null; //Either return the chapter for the second range, or the range is a verse, therefore the chapter is null / same as start chapter

            int.TryParse(endChapter, out val);

            return (endChapter == null || val == 0) ? null : (int?)val;
        }

        private static int? GetEndVerse(string scripture)
        {
            int val;
            var scriptureRanges = scripture.Split('-');

            if (scriptureRanges.Length == 1) //No range, no end verse
                return null;

            string endVerse = string.Empty;

            if (scriptureRanges[1].Contains(':'))
            {
                endVerse = scriptureRanges[1].Split(':')[1];
            }
            else if (scriptureRanges[0].Contains(':'))
            {
                endVerse = scriptureRanges[1];
            }
            else
            {
                return null;
            }

            int.TryParse(endVerse, out val);

            return (endVerse == null || val == 0) ? null : (int?)val;
        }
    }
}
