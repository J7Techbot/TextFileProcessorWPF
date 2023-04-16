
using Microsoft.Win32;

namespace ViewLayer.Helpers
{
    public static class FileSystemHelper
    {
        const string fileNameDialogTitle = "Select file";
        const string fileNamesDialogTitle = "Select files";
        const string defaultFilter = "All(*.*) | *.*";
        /// <summary>
        /// Open a dialog for file selection and return the selected file paths.
        /// </summary>
        /// <param name="filter">Define allowed file extensions.</param>
        /// <param name="multiselect">If true, more then one file can be selected.</param>
        /// <param name="title">Dialog header.</param>
        /// <returns>Fullnames of selected files.</returns>
        public static string[] GetFileNames(string filter = defaultFilter, bool multiselect = false, string title = null)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = filter;
            dialog.Multiselect = multiselect;
            dialog.Title = title ??= multiselect ? fileNamesDialogTitle : fileNameDialogTitle;

            bool? result = dialog.ShowDialog();

            if (result != null && result == true)
            {
                return dialog.FileNames;
            }

            return null;
        }
        /// <summary>
        /// Open a dialog for file selection and return the selected file path.
        /// </summary>
        /// <param name="filter">Define allowed file extensions.</param>
        /// <param name="title">Dialog header.</param>
        /// <returns></returns>
        public static string GetFileName(string filter = defaultFilter, string title = null)
        {
            var fileNames = GetFileNames(filter, title: title);

            if (fileNames != null && fileNames.Length > 0)
                return fileNames[0];

            return null;
        }
    }
}
