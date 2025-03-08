using ConsoleWordProcessorDBEFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWordProcessorDBEFCore.Controller
{
    class WordProcessorController
    {
        public WordProcessorController()
        {
        }

        public void LoadWordsFromFile(string filePath)
        {
            var wordCounts = new Dictionary<string, int>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var words = line.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        if (word.Length >= 3 && word.Length <= 20)
                        {
                            if (wordCounts.ContainsKey(word))
                            {
                                wordCounts[word]++;
                            }
                            else
                            {
                                wordCounts[word] = 1;
                            }
                        }
                    }
                }
            }

            using (var context = new AppDbContext())
            {
                context.EnsureDatabaseCreated();
                Console.WriteLine("База данных проверена/создана успешно");
                foreach (var pair in wordCounts.Where(w => w.Value >= 4))
                {
                    var existingWord = context.Words.SingleOrDefault(w => w.Text == pair.Key);
                    if (existingWord != null)
                    {
                        existingWord.Count += pair.Value;
                    }
                    else
                    {
                        context.Words.Add(new Word { Text = pair.Key, Count = pair.Value });
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
