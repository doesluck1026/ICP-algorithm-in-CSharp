namespace ICP_Algorithm.WFA.Methods
{
    internal class Websites
    {
        internal static void OpenGithubProfile()
        {
            RunProcess.RunInternetBrowser("https://github.com/doesluck1026");
        }

        internal static void OpenGithubProject()
        {
            RunProcess.RunInternetBrowser("https://github.com/doesluck1026/ICP-algorithm-in-CSharp");
        }

        internal static void OpenGithubIssues()
        {
            RunProcess.RunInternetBrowser("https://github.com/doesluck1026/ICP-algorithm-in-CSharp/issues");
        }

        internal static void OpenATutorialOnRigidRegistration()
        {
            RunProcess.RunInternetBrowser("http://www.sci.utah.edu/~shireen/pdfs/tutorials/Elhabian_ICP09.pdf");
        }
    }
}