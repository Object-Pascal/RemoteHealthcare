using System;
using System.IO;
using VrDemo.Utils;

namespace VrDemo.IO
{
    public static class FolderData
    {
        public readonly static string[] SENDABLES_FOLDER_SEGMENTS = new Uri(Directory.GetCurrentDirectory()).Segments;
        public readonly static string SENDABLES_FOLDER = string.Concat(SENDABLES_FOLDER_SEGMENTS.SubArray(0, SENDABLES_FOLDER_SEGMENTS.Length - 2)) + "Sendables/";
    }
}