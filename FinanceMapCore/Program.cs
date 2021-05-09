using System;

namespace FinanceMap
{
    public static class Program
    {
        public static void Main()
        {
            //TemplateService.CreateProjectionJsonTemplate();
            Console.WriteLine("Hello, welcome to Finance Map!");
            PromptService.ProjectFromFile();
        }
    }
}
