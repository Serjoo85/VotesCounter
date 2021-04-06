using System;
using System.Collections.Generic;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public class CreateRandomFileService
    {
        public static void CreateFile(List<VoteData> items)
        {
            var _items = items;
            List<string> s = new();
            s.Add(_items.Count.ToString());
            s.Add("");
            foreach (var item in _items)
            {
                s.Add(item.ConCount.ToString());
                foreach (var name in item.Names)
                {
                    s.Add(name);
                }

                string bull = "";
                for (int i = 0; i < item.Bulletins.GetLength(0); i++)
                {
                    for (int j = 0; j < item.Bulletins.GetLength(1); j++)
                    {
                        bull += item.Bulletins[i, j].ToString();
                        if (j != item.Bulletins.GetLength(1)) bull += " ";
                    }
                    s.Add(bull);
                    bull = "";
                }
                s.Add("");
            }

            try
            {
                System.IO.File.WriteAllLines("randomfile.txt", s);
                Console.WriteLine("Файл успешно создан!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Файл не создан, ошибка:");
                Console.WriteLine(e);
            }

        }
    }
}
