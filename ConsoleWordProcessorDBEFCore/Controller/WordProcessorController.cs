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
            try
            {
                // Проверяем, существует ли файл
                if (File.Exists(filePath))
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        string line;
                        int count = 0;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Разбиваем строку на слова
                            var words = line.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries); // разбиваем файл на слова
                            
                            foreach (var word in words)
                            {
                                if (word.Length >= 3 && word.Length <= 20) // проверка длины слова
                                {
                                    if (wordCounts.ContainsKey(word))
                                    {
                                        wordCounts[word]++;
                                    }
                                    else
                                    {
                                        wordCounts[word] = 1;
                                    }
                                    count++;
                                }
                            }
                            
                        }
                        Console.WriteLine($"Извлечено {count} новых слов");
                    }
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            using (var context = new AppDbContext())
            {
                context.EnsureDatabaseCreated();
                Console.WriteLine("База данных проверена/создана успешно");
                foreach (var pair in wordCounts.Where(w => w.Value >= 4)) // число вхождений и запись в базу
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
                Console.WriteLine("Записаны извлеченные слова");
            }
        }
    }
}
