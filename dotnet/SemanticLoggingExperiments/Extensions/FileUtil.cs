using System;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;

namespace SemanticLoggingExperiments.Extensions
{
    internal class FileUtil
    {
        public static FileInfo ProcessFileNameForLogging(string fileName)
        {
            ValidFile(fileName, "fileName");

            var s = ReplaceEnvironmentVariables(fileName);

            if (!string.Equals(fileName, s, StringComparison.OrdinalIgnoreCase))
            {
                ValidFile(s, "fileName", true);
            }

            return new FileInfo(RootFileNameAndEnsureTargetFolderExists(s));
        }

        public static void ValidFile(string fileName, string argumentName = "fileName", bool replaced = false)
        {
            Guard.ArgumentNotNullOrEmpty(fileName, argumentName);

            // FileInfo will perform extra validations (chars, path length, etc)
            var file = new FileInfo(fileName);

            // Check for relative file path expansions and the actual file name.
            // Samples of invalid entries (without quotes):
            // ".", "..\..", "C:\Test\.."  
            if (string.IsNullOrWhiteSpace(file.Name) ||
                Path.GetFileName(file.FullName) != file.Name)
            {
                if (!replaced)
                {
                    throw new ArgumentException("A file name with a relative path is not allowed. Provide only the file name or the full path of the file.");
                }
                else
                {
                    throw new ArgumentException("A file name with a relative path after replacing environment variables is not allowed. Provide only the file name or the full path of the file.");
                }
            }
        }

        public static string CreateRandomFileName()
        {
            return Path.ChangeExtension(Path.GetRandomFileName(), ".log");
        }

        private static string RootFileNameAndEnsureTargetFolderExists(string fileName)
        {
            string rootedFileName = fileName;
            if (!Path.IsPathRooted(rootedFileName))
            {
                // GetFullPath will resolve any relative path in rootedFileName
                // AppDomain.CurrentDomain.BaseDirectory will be used as root to decouple from Environment.CurrentDirectory value.                
                rootedFileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rootedFileName));
            }

            string directory = Path.GetDirectoryName(rootedFileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return rootedFileName;
        }

        private static string ReplaceEnvironmentVariables(string fileName)
        {
            // Check EnvironmentPermission for the ability to access the environment variables.
            try
            {
                string variables = Environment.ExpandEnvironmentVariables(fileName);

                // If an Environment Variable is not found then remove any invalid tokens
                Regex filter = new Regex("%(.*?)%", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

                string filePath = filter.Replace(variables, string.Empty);

                if (Path.GetDirectoryName(filePath) == null)
                {
                    filePath = Path.GetFileName(filePath);
                }

                return filePath;
            }
            catch (SecurityException)
            {
                throw new InvalidOperationException("Environment Variables access denied.");
            }
        }
    }
}
