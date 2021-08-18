using System;
using System.Diagnostics;
using mroot_lib;

namespace evernote_service
{
    class EvernoteAction
    {

        #region api

        static public void ShowCodeNote()
        {
            Evernote.LoginToSession();

            ShowTodayNote();

        }

        static public void CreateCodeNote()
        {
            Evernote.LoginToSession();

            if (ExistTodayCodeNote() == false)
            {
                CreateTodayCodeNote();
            }
            else
            {
                System.Console.WriteLine("Code note already exists");
            }
        }

        static public void RemoveAllDefaultNotes()
        {

            Evernote.LoginToSession();

            Evernote.DeleteAllNotesFromNotebook("default");
        }

        #endregion

        #region private

        static private void ShowTodayNote()
        {
            var title = GetTodayCodeNoteTitle();

            var EvernoteSearchScript = mroot.substitue_enviro_vars("||bin||\\AHK_script\\evernote\\evernote_open_note_with_title.exe");

            Process process = new Process();
            process.StartInfo.FileName = EvernoteSearchScript;
            process.StartInfo.Arguments = $"\"{title}\"";
            process.Start();
        }

        static private bool ExistTodayCodeNote()
        {
            var title = GetTodayCodeNoteTitle();
            var notebook = GetCodeNodeNotebook();

            return Evernote.FindNote(title, notebook) != null;
        }

        static private void CreateTodayCodeNote()
        {
            var title = GetTodayCodeNoteTitle();
            var notebookName = GetCodeNodeNotebook();
            var tag = GetCodeNoteTag();
            var content = GetTodayCodeNoteContent();

            Evernote.CreateNote(title, content, notebookName, tag);
        }

        static private string GetTodayCodeNoteTitle()
        {
            var dateString = DateTime.Now.ToString("yyyy-MM-dd");
            var title = $"auto code note [ {dateString} ]";

            return title;
        }

        static private string GetTodayCodeNoteContent()
        {
            var content = $"code note - { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}" +
                $" \n__________________________________________________________________________\n";
            return content;
        }

        static private string GetCodeNodeNotebook()
        {
            return "code_diary";
        }

        static private string GetCodeNoteTag()
        {
            return "code_diary_auto_note";
        }

        #endregion

    }
}

