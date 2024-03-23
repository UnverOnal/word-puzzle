namespace UI
{
    public static class UiUtil
    {
        /// <summary>
        /// Formats the input string by capitalizing the first letter of each word and returns the formatted string.
        /// </summary>
        public static string FormatString(string input)
        {
            var words = input.ToLower().Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    var letters = words[i].ToCharArray();
                    letters[0] = char.ToUpper(letters[0]);
                    words[i] = new string(letters);
                }
            }

            return string.Join(" ", words);
        }
    }
}
