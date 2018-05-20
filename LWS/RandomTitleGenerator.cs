using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;



namespace LWS
{
    internal class WordTableRecord
    {
        static readonly int WordsPerRecord = 14;


        // Always has "WordsPerRecord" words;
        string[] words;


        public string Category { get; }


        public WordTableRecord(string csvLine)
        {
            string[] fields;
            if (csvLine == "")
            {
                fields = Enumerable.Repeat("", WordsPerRecord + 1).ToArray();
            }
            else
            {
                fields = csvLine.Split(',');
            }
            Debug.Assert(fields.Length == WordsPerRecord + 1);

            words = fields
                .Take(WordsPerRecord)
                .Select(w => Capitalize(w))
                .ToArray();

            Category = fields[WordsPerRecord];
        }


        // Returns (word, column).
        public string GetRandomWord(out int column)
        {
            column = HSPRNG.Rnd(WordsPerRecord);
            return words[column];
        }


        public string Get(int column)
        {
            return words[column];
        }


        // For debugging
        public void Dump()
        {
            foreach (var word in words)
            {
                Console.Write(word, ',');
            }
            Console.WriteLine(Category);
        }


        static string Capitalize(string s)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s);
        }
    }


    internal class WordTable
    {
        List<WordTableRecord> table;


        public WordTable(string[] lines)
        {
            table = lines
                .Select(line => new WordTableRecord(line))
                .ToList();
        }


        // Returns (word, category, row, column).
        public string GetRandomWord(out string category, out int row, out int column)
        {
            string word;

            do
            {
                row = GetRandomRow();
                var record = table[row];
                category = record.Category;
                word = record.GetRandomWord(out column);
            } while (word == "");

            return word;
        }


        public int GetRandomRow()
        {
            return HSPRNG.Rnd(table.Count);
        }


        public WordTableRecord GetRecord(int row)
        {
            return table[row];
        }


        // For debugging
        public void Dump()
        {
            table.ForEach(r => r.Dump());
        }
    }


    internal class WordInfo
    {
        public string Word { get; }
        public string Category { get; }
        public int Row { get; }
        public int Column { get; }
        public bool Immediately { get; }


        public WordInfo(string word, string category, int row, int column, bool immediately = false)
        {
            Word = word;
            Category = category;
            Row = row;
            Column = column;
            Immediately = immediately;
        }
    }


    public static class RandomTitleGenerator
    {
        static readonly int MaxTitleLengthInShiftJis = 28;


        static WordTable wordTable;


        public static void Initialize(string filepath)
        {
            wordTable = new WordTable(
                    File.ReadAllLines(
                        filepath,
                        Encoding.GetEncoding("shift_jis")));
        }


        public static string Generate(bool jp)
        {
            while (true)
            {
                var head = GenerateHead(jp);
                if (head is null)
                    continue;
                var body = GenerateBody(jp, head);
                if (body is null)
                    continue;
                if (body.Immediately)
                    return body.Word + head.Word;
                var tail = GenerateTail(jp, body);
                if (tail is null)
                    continue;
                return head.Word + body.Word + tail.Word;
            }
        }


        private static WordInfo GenerateHead(bool jp)
        {
            var word = wordTable.GetRandomWord(out var category, out var row, out var column);

            if (category == "具")
            {
                return null;
            }
            else
            {
                return new WordInfo(word, category, row, column);
            }
        }


        private static WordInfo GenerateBody(bool jp, WordInfo head)
        {
            int column = head.Column;
            string word = "";

            if (jp)
            {
                switch (column)
                {
                    case 10:
                    case 11:
                        if (HSPRNG.Rnd(5) == 0)
                        {
                            column = 0;
                            if (HSPRNG.Rnd(2) == 0)
                            {
                                word = "の";
                            }
                        }
                        else
                        {
                            switch (HSPRNG.Rnd(5))
                            {
                                case 0:
                                    word = "・オブ・";
                                    break;
                                case 1:
                                    return new WordInfo("ザ・", null, 0, 0, true);
                                case 2:
                                    word = "・";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 0:
                    case 1:
                        word = "の";
                        if (HSPRNG.Rnd(10) == 0)
                        {
                            column = 10;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (column)
                {
                    case 0:
                    case 1:
                        if (HSPRNG.Rnd(6) == 0)
                        {
                            word = " of";
                        }
                        else
                        {
                            if (HSPRNG.Rnd(6) == 0)
                            {
                                return new WordInfo("The ", null, 0, 0, true);
                            }
                        }
                        break;
                    default:
                        word = " ";
                        break;
                }
            }

            return new WordInfo(word, head.Category, head.Row, column);
        }


        private static WordInfo GenerateTail(bool jp, WordInfo body)
        {
            bool failed = true;
            int row = body.Row;
            int column = body.Column;

            for (int i = 0; i < 100; ++i)
            {
                row = wordTable.GetRandomRow();
                if (row == body.Row)
                {
                    continue;
                }
                var record = wordTable.GetRecord(row);
                if (record.Category == body.Category
                        && record.Category != "万能" && body.Category != "万能")
                {
                    continue;
                }
                column = (column < 10 ? 0 : 10) + HSPRNG.Rnd(2);
                if (record.Get(column) != "")
                {
                    failed = false;
                    break;
                }
            }

            if (failed)
                return null;

            var word = wordTable.GetRecord(row).Get(column);

            if (!ValidateLength(word))
                return null;

            return new WordInfo(word, null, 0, 0);
        }


        static private bool ValidateLength(string s)
        {
            return Encoding.GetEncoding("shift_jis").GetBytes(s).Length
                < MaxTitleLengthInShiftJis;
        }
    }
}
