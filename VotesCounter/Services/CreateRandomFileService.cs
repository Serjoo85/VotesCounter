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
            List<string> list = new();
            list.Add(_items.Count.ToString());
            list.Add("");
            foreach (var item in _items)
            {
                list.Add(item.ConCount.ToString());
                foreach (var name in item.Names)
                {
                    list.Add(name);
                }

                string bull = "";
                for (int i = 0; i < item.Bulletins.GetLength(0); i++)
                {
                    for (int j = 0; j < item.Bulletins.GetLength(1); j++)
                    {
                        bull += item.Bulletins[i, j].ToString();
                        if (j != item.Bulletins.GetLength(1)) bull += " ";
                    }
                    list.Add(bull);
                    bull = "";
                }
                list.Add("");
            }

            try
            {
                System.IO.File.WriteAllLines("randomfile.txt", list);
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
