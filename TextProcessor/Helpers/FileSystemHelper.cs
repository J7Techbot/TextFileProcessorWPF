
using Microsoft.Win32;

namespace ViewLayer.Helpers
{
    public static class FileSystemHelper
    {
        public static string[] GetFileNames(string filter = "All(*.*) | *.*", bool multiselect = false, string title = null)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = filter;
            dialog.Multiselect = multiselect;
            dialog.Title = title ??= multiselect ? "Select files" : "Select file";

            bool? result = dialog.ShowDialog();

            if (result != null && result == true)
            {
                return dialog.FileNames;
            }

            return null;
        }
        public static string GetFileName(string filter = "All(*.*) | *.*", string title = null)
        {
            var fileNames = GetFileNames(filter, title: title);

            if (fileNames != null && fileNames.Length > 0)
                return fileNames[0];

            return null;
        }
    }
}
