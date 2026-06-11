namespace RSLib.GE
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using Godot;
    using CSharp.Framework;
    using Debug;

    public static class Localizer
    {
        private static readonly Regex REGEX_PLURAL = new(@"\(([^|()]*)\|([^()]*)\)", RegexOptions.Compiled);

        public class InitializationArgs
        {
            public LoadMode Mode;
            public string GoogleSheetUID;
            public string ResourceFilePath;
        }

#if TOOLS
        private class LocalizationDownloader : WebClient
        {
            public LocalizationDownloader(CookieContainer container)
            {
                _container = container;
            }

            private readonly CookieContainer _container;

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                if (request is HttpWebRequest httpRequest)
                    httpRequest.CookieContainer = _container;

                return request;
            }

            protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
            {
                WebResponse response = base.GetWebResponse(request, result);
                ReadCookies(response);
                return response;
            }

            protected override WebResponse GetWebResponse(WebRequest request)
            {
                WebResponse response = base.GetWebResponse(request);
                ReadCookies(response);
                return response;
            }

            private void ReadCookies(WebResponse r)
            {
                if (r is HttpWebResponse response)
                {
                    CookieCollection cookies = response.Cookies;
                    _container.Add(cookies);
                }
            }
        }
#endif

        public enum LoadMode
        {
            GOOGLE_SHEETS_DOWNLOAD,
            FILE_PATH,
        }

        private const string GOOGLE_SHEETS_EXPORT_FORMAT = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&amp;usp=sharing";
        private const char IGNORE_CHAR = '#';

        private static Dictionary<string, Dictionary<string, string>> _entries;

        public static event Action LanguageChanged;

        /// <summary>
        /// All languages handled in loaded CSV file.
        /// </summary>
        public static string[] Languages { get; private set; }

        /// <summary>
        /// Currently selected language.
        /// </summary>
        public static string Language { get; private set; }

        /// <summary>
        /// Initializes localization.
        /// </summary>
        /// <param name="args">Initialization arguments.</param>
        public static void Init(InitializationArgs args)
        {
            LoadMode loadMode = args.Mode;
            if (loadMode == LoadMode.GOOGLE_SHEETS_DOWNLOAD && !OS.HasFeature("editor"))
                loadMode = LoadMode.FILE_PATH;

            string downloadOutput = string.Empty;

            try
            {
                switch (loadMode)
                {
#if TOOLS
                    case LoadMode.GOOGLE_SHEETS_DOWNLOAD:
                        LocalizationDownloader wc = new(new CookieContainer());
                        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
                        wc.Headers.Add("DNT", "1");
                        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                        wc.Headers.Add("Accept-Encoding", "deflate");
                        wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");

                        byte[] downloadData = wc.DownloadData(string.Format(GOOGLE_SHEETS_EXPORT_FORMAT, args.GoogleSheetUID));
                        downloadOutput = System.Text.Encoding.UTF8.GetString(downloadData);
                        break;
#endif

                    case LoadMode.FILE_PATH:
                        downloadOutput = FileUtils.ReadText(args.ResourceFilePath);
                        break;
                    
                    default:
                        Debugger.Console.Warning($"Unhandled {nameof(LoadMode)} in {nameof(Localizer)}, downloading using {nameof(LoadMode.FILE_PATH)}.");
                        downloadOutput = FileUtils.ReadText(args.ResourceFilePath);
                        break;
                }
            }
            catch (Exception e1)
            {
                Debugger.Console.Error($"Exception caught during {nameof(Localizer)} initialization: {e1}");

                if (loadMode == LoadMode.GOOGLE_SHEETS_DOWNLOAD)
                {
                    Debugger.Console.Entry($"Localization initialization failed while downloading from Google Sheets, trying to initialize from {args.ResourceFilePath}");

                    try
                    {
                        downloadOutput = FileUtils.ReadText(args.ResourceFilePath);
                    }
                    catch (Exception e2)
                    {
                        Debugger.Console.Error($"Exception caught during {nameof(Localizer)} initialization from file path: {e2}");
                    }
                }
            }

            if (string.IsNullOrEmpty(downloadOutput))
            {
                Debugger.Console.Error($"Could not initialize {nameof(Localizer)}.");
                return;
            }

            Init(downloadOutput);
        }

        /// <summary>
        /// Initializes all entries based on a given CSV string.
        /// </summary>
        /// <param name="csvText">CSV as string.</param>
        private static void Init(string csvText)
        {
            string[,] grid = CSVReader.SplitCSVGrid(csvText);

            // Initialize languages.
            _entries = new Dictionary<string, Dictionary<string, string>>();
            List<string> languages = new();
            for (int x = 1; x < grid.GetLength(0); ++x) // Start at 1 to avoid keys column.
            {
                string language = grid[x, 0];
                if (string.IsNullOrEmpty(language) || language[0] == IGNORE_CHAR)
                    continue;

                if (_entries.ContainsKey(language))
                {
                    Debugger.Console.Error($"Cannot add already added language {language} in {nameof(Localizer)}.");
                    continue;
                }
                
                _entries.Add(language, new Dictionary<string, string>());
                languages.Add(language);
            }

            Languages = languages.ToArray();
            Language = Languages[0];

            // Initialize entries.
            for (int y = 1; y < grid.GetLength(1); ++y)
            {
                string key = grid[0, y];
                if (string.IsNullOrEmpty(key) || key[0] == IGNORE_CHAR)
                    continue;

                for (int x = 0; x < Languages.Length; ++x)
                {
                    string language = grid[x + 1, 0];
                    if (string.IsNullOrEmpty(language))
                        continue;

                    string entry = grid[x + 1, y];
                    _entries[language].Add(key, entry);
                }
            }
        }

        /// <summary>
        /// Sets the current language, based on its index in the handled languages list.
        /// </summary>
        /// <param name="languageIndex">Selected language index.</param>
        public static void SetCurrentLanguage(int languageIndex)
        {
            if (languageIndex > _entries.Count - 1)
            {
                Debugger.Console.Error($"Tried to set language index to {languageIndex} but only {_entries.Count} languages are known.");
                return;
            }

            SetCurrentLanguage(_entries.ElementAt(languageIndex).Key);
        }

        /// <summary>
        /// Sets the current language, based on its name.
        /// </summary>
        /// <param name="languageName">Selected language name.</param>
        public static void SetCurrentLanguage(string languageName)
        {
            if (!_entries.ContainsKey(languageName))
            {
                Debugger.Console.Error($"Tried to set language to {languageName} but it wasn't found.");
                return;
            }

            Debugger.Console.Entry($"Setting language to {languageName}.");

            Language = languageName;
            LanguageChanged?.Invoke();
        }

        /// <summary>
        /// Gets the localized key for current language, and returns the key itself if it has not been found.
        /// </summary>
        /// <param name="key">Key to localize.</param>
        /// <returns>Localized key if it exists, else the key itself.</returns>
        public static string Get(string key)
        {
            return Get(key, Language);
        }

        /// <summary>
        /// Gets the localized key for the specified language, and returns the key itself if it has not been found or if the language is not known.
        /// </summary>
        /// <param name="key">Key to localize.</param>
        /// <param name="language">Language to use for key localization.</param>
        /// <returns>Localized key if it and the language both exist, else the key itself.</returns>
        public static string Get(string key, string language)
        {
            bool languageKnown = false;
            for (int i = Languages.Length - 1; i >= 0; --i)
            {
                if (Languages[i] == language)
                {
                    languageKnown = true;
                    break;
                }
            }

            if (!languageKnown)
            {
                Debugger.Console.Warning($"Language {language} is not known in languages list. Known languages are: {string.Join(", ", Languages)}.");
                return key;
            }

            if (_entries[language].TryGetValue(key, out string entry))
                return entry;

            Debugger.Console.Warning($"Localization key {key} not found in language {language}.");
            return key;
        }

        /// <summary>
        /// Gets and pluralizes the localized key for current language, and returns the key itself if it has not been found.
        /// </summary>
        /// <param name="key">Key to localize.</param>
        /// <param name="plural">True for plural, false for singular.</param>
        /// <returns>Localized and pluralized key if it exists, else the key itself.</returns>
        public static string GetPluralized(string key, bool plural)
        {
            return Pluralize(Get(key, Language), plural);
        }
        
        /// <summary>
        /// Gets and pluralizes the localized key for current language, and returns the key itself if it has not been found.
        /// </summary>
        /// <param name="key">Key to localize.</param>
        /// <param name="pluralCount">Reference count for pluralization, a value higher than 1 will use plural form.</param>
        /// <returns>Localized and pluralized key if it exists, else the key itself.</returns>
        public static string GetPluralized(string key, int pluralCount)
        {
            return Pluralize(Get(key, Language), pluralCount);
        }
        
        /// <summary>
        /// Checks if the key exists for current language, and localizes it if so.
        /// </summary>
        /// <param name="key">Key to localize.</param>
        /// <param name="result">Localized entry if key exists.</param>
        /// <returns>True if the key exists, else false.</returns>
        public static bool TryGet(string key, out string result)
        {
            return _entries[Language].TryGetValue(key, out result);
        }

        /// <summary>
        /// Checks if the key exists for the specified language, and localizes it if so.
        /// </summary>
        /// <param name="key">Key to localize.</param>
        /// <param name="language">Language to use for key localization.</param>
        /// <param name="result">Localized entry if key exists and language is known.</param>
        /// <returns>True if the key exists and the language is known, else false.</returns>
        public static bool TryGet(string key, string language, out string result)
        {
            return _entries[language].TryGetValue(key, out result);
        }

        /// <summary>
        /// Gets the localized key for current language, and formats the result using given arguments.
        /// Localized key is thus considered to have format slots in {i} format.
        /// </summary>
        /// <param name="key">Key to localize and format.</param>
        /// <param name="args">Formatting arguments.</param>
        /// <returns>Localized and formatted key if it exists, else the key itself.</returns>
        public static string Format(string key, params object[] args)
        {
            return string.Format(Get(key), args);
        }
        
        /// <summary>
        /// Gets and pluralizes the localized key for current language, and formats the result using given arguments.
        /// Localized key is thus considered to have format slots in {i} format.
        /// </summary>
        /// <param name="key">Key to localize and format.</param>
        /// <param name="plural">True for plural, false for singular.</param>
        /// <param name="args">Formatting arguments.</param>
        /// <returns>Localized, pluralized and formatted key if it exists, else the key itself.</returns>
        public static string FormatPluralized(string key, bool plural, params object[] args)
        {
            return Pluralize(string.Format(Get(key), args), plural);
        }
        
        /// <summary>
        /// Gets and pluralizes the localized key for current language, and formats the result using given arguments.
        /// Localized key is thus considered to have format slots in {i} format.
        /// </summary>
        /// <param name="key">Key to localize and format.</param>
        /// <param name="pluralCount">Reference count for pluralization, a value higher than 1 will use plural form.</param>
        /// <param name="args">Formatting arguments.</param>
        /// <returns>Localized, pluralized and formatted key if it exists, else the key itself.</returns>
        public static string FormatPluralized(string key, int pluralCount, params object[] args)
        {
            return Pluralize(string.Format(Get(key), args), pluralCount);
        }

        /// <summary>
        /// Gets the localized key for current language, and formats the result using given arguments.
        /// Localized key is thus considered to have format slots in {i} format.
        /// </summary>
        /// <param name="key">Key to localize and format.</param>
        /// <param name="result">Localized and formatted entry if key exists and language is known.</param>
        /// <param name="args">Formatting arguments.</param>
        /// <returns>Localized and formatted key if it exists, else the key itself.</returns>
        public static bool TryFormat(string key, out string result, params object[] args)
        {
            if (!TryGet(key, out string entry))
            {
                result = key;
                return false;
            }

            result = string.Format(entry, args);
            return true;
        }

        /// <summary>
        /// Pluralizes a text.
        /// Pluralized parts format is (s|p) where s is the singular form, and p the plural form.
        /// </summary>
        /// <param name="text">Text to pluralize.</param>
        /// <param name="count">Reference count, a value higher than 1 will use plural form.</param>
        /// <returns>Pluralized text.</returns>
        public static string Pluralize(string text, int count)
        {
            return Pluralize(text, count >= 2);
        }
        
        /// <summary>
        /// Pluralizes a text.
        /// Pluralized parts format is (s|p) where s is the singular form, and p the plural form.
        /// </summary>
        /// <param name="text">Text to pluralize.</param>
        /// <param name="plural">True for plural, false for singular.</param>
        /// <returns>Pluralized text.</returns>
        public static string Pluralize(string text, bool plural)
        {
            return REGEX_PLURAL.Replace(text, match => match.Groups[plural ? 2 : 1].Value);
        }
    }
}