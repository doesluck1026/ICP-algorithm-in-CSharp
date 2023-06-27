using System.Diagnostics;

namespace ICP_Algorithm.WFA.Methods
{
    internal class RunProcess
    {
        private static void RunPs(string FileName, string Args = "")
        {
            Process ps = new Process();
            ps.StartInfo.FileName = FileName;
            ps.StartInfo.Arguments = Args;
            ps.Start();
        }

        internal static void RunInternetBrowser(string url = "https://github.com/doesluck1026/ICP-algorithm-in-CSharp")
        {
            RunPs(url);
        }
    }
}