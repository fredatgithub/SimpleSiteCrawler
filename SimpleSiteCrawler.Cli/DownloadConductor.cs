﻿using System;
using System.IO;
using SimpleSiteCrawler.Lib;

namespace SimpleSiteCrawler.Cli
{
    internal static class DownloadConductor
    {
        private static readonly Lazy<ILogger> LoggerLazy = new Lazy<ILogger>(LoggerFactory.CreateLogger);

        private static ILogger Logger => LoggerLazy.Value;

        public static void Start(Options options)
        {
            var downloadToFolder = GetDownloadFolderPath(options);

            var crawler = new Crawler(new SitePage
            {
                Uri = new Uri(options.Site)
            });

            crawler.OnDownloadCompleated += (s, e) => Logger.Info("Download compleated!");
            crawler.OnPageDownloadBegin += (s, p) => Logger.Info($"{p.Uri} download start");
            crawler.OnPageDownloadCompleate += (s, p) => Logger.Info($"[OK] {p.Uri.AbsolutePath}");
            crawler.OnPageDownloadCompleate += (s, p) => SaveHelper.SaveResult(downloadToFolder, p);

            crawler.Execute();
        }

        private static string GetCurrentFolder() => Path.GetDirectoryName(typeof(Program).Assembly.Location);

        private static string GetDownloadFolderPath(Options options) =>
            Path.Combine(GetCurrentFolder(), options.DownloadsFolderName, SaveHelper.MakePath(options.Site));
    }
}