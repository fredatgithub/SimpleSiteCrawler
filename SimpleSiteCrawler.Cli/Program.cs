using System;

namespace SimpleSiteCrawler.Cli
{
  // ReSharper disable once ArrangeTypeModifiers
  class Program
  {
    private static readonly Lazy<ILogger> LoggerLazy = new Lazy<ILogger>(LoggerFactory.CreateLogger());

    private static ILogger Logger => LoggerLazy.Value;

    // ReSharper disable once ArrangeTypeMemberModifiers
    static void Main(string[] arguments)
    {
      if (arguments.Length == 0)
      {
        arguments = new string[]
        {
          "https://www.dictionnaire-academie.fr/"
        };
      }

      OptionsProvider.Parse(arguments,
          DownloadConductor.Start,
          Logger.Error,
          Logger.Error);

      Console.WriteLine("Press any key to exit:");
      Console.ReadLine();
    }
  }
}