namespace DST.Core.Tests
{
    internal static class MiscTests
    {
        // Usage:
        // GetLines("C:\\Users\\bates\\source\\repos\\Messier\\DST.Core\\", "*.cs", SearchOption.AllDirectories)
        public static int GetTotalLineCount(string searchDirectory, string searchPattern, SearchOption option)
        {
            if (string.IsNullOrWhiteSpace(searchDirectory)) return -1;
            if (string.IsNullOrWhiteSpace(searchDirectory)) return -2;
            if (Directory.Exists(searchDirectory) == false) return -3;

            int lines = 0;
            string[] files;

            try
            {
                files = Directory.GetFiles(searchDirectory, searchPattern, option);

                foreach (string file in files)
                {
                    lines += File.ReadLines(file).Count();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                lines = -4;
            }

            return lines;
        }
    }
}
