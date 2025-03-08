using ConsoleWordProcessorDBEFCore.Controller;
using System;

Console.Write("Введите путь к файлу: ");
string filePath = Console.ReadLine();
WordProcessorController _wordProceccor = new WordProcessorController();

try
{
	_wordProceccor.LoadWordsFromFile(filePath);
}
catch (Exception ex)
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}

Console.ReadLine();