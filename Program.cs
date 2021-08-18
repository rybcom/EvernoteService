
using CommandLine;

namespace evernote_service
{

    public class Options
    {
        [Option("show_code_note",
          Default = false,
          HelpText = "Show today code note in evernote app")]
        public bool ShowNote { get; set; }

        [Option("create_code_note",
          Default = false,
          HelpText = "Create today code note in evernote app")]
        public bool CreateNote { get; set; }

        [Option("delete_default_notes",
        Default = false,
        HelpText = "Delete all notes from notebook 'default'")]
        public bool DeleteEmails { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
           .WithParsed(RunOptions);
        }

        static void RunOptions(Options opts)
        {
            if (opts.ShowNote)
            {
                EvernoteAction.ShowCodeNote();
            }
            else if (opts.CreateNote)
            {
                EvernoteAction.CreateCodeNote();
            }
            else if (opts.DeleteEmails)
            {
                EvernoteAction.RemoveAllDefaultNotes();
            }
            else
            {
                ShowHelp();
            }
        }

        static void ShowHelp()
        {
            System.Console.WriteLine("\nuse just one option: \n");
            System.Console.WriteLine("--show_code_note [ Show today code note in evernote app ] ");
            System.Console.WriteLine("--create_code_note [ Create today code note in evernote app ] ");
            System.Console.WriteLine("--delete_default_notes [ Delete all notes from notebook 'default ] ");
        }


    }
}

