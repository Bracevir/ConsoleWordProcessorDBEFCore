using ConsoleWordProcessorDBEFCore.Controller;

Console.Write("Введите путь к файлу: ");
string filePath = Console.ReadLine();
WordProcessorController _wordProceccor = new WordProcessorController();

try
{
	_wordProceccor.LoadWordsFromFile(filePath);
}
catch (Exception)
{

	throw;
}