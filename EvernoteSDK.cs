using System;
using System.Collections.Generic;
using System.Linq;
using EvernoteSDK;

namespace evernote_service
{

    #region evernote

    internal class Evernote
    {

        internal static ENNotebook GetNotebook(string notebookName)
        {
            return ENSession.SharedSession.ListNotebooks().Find(x => x.Name.Equals(notebookName));

        }

        internal static void LoginToSession()
        {
            ENSession.SetSharedSessionConsumerKey("your-key", "secret-key");

            if (ENSession.SharedSession.IsAuthenticated == false)
            {
                ENSession.SharedSession.AuthenticateToEvernote();
            }

            if (ENSession.SharedSession.IsAuthenticated == false)
            {
                Console.WriteLine("Authentication has failed");
            }

        }

        internal static void DeleteAllNotes()
        {
            List<ENSessionFindNotesResult> myResultsList = ENSession.SharedSession.FindNotes
                (ENNoteSearch.NoteSearch(""), null,
                ENSession.SearchScope.All,
                ENSession.SortOrder.RecentlyUpdated,
                5000);

            foreach (var note in myResultsList)
            {
                ENSession.SharedSession.DeleteNote(note.NoteRef);
            }
        }

        internal static ENSessionFindNotesResult FindNote(string title, string notebook)
        {

            List<ENSessionFindNotesResult> myResultsList = ENSession.SharedSession.FindNotes
                          (ENNoteSearch.NoteSearch(""), GetNotebook(notebook),
                          ENSession.SearchScope.None,
                          ENSession.SortOrder.RecentlyUpdated,
                          5000).Where(x => x.Title == title).ToList();


            if (myResultsList.Count > 0)
            {
                return myResultsList[0];
            }

            return null;
        }

        internal static void DeleteAllNotesFromNotebook(string notebookName)
        {

            var notebook = GetNotebook(notebookName);

            if (notebook != null)
            {
                List<ENSessionFindNotesResult> myResultsList = ENSession.SharedSession.FindNotes
                    (ENNoteSearch.NoteSearch(""), notebook,
                    ENSession.SearchScope.None,
                    ENSession.SortOrder.RecentlyUpdated,
                    1000);

                foreach (var note in myResultsList)
                {
                    ENSession.SharedSession.DeleteNote(note.NoteRef);

                    System.Console.WriteLine($"Note [ {note.Title} ] has been deleted");
                }

                System.Console.WriteLine($"\n{myResultsList.Count} notes has been deleted");


            }
            else
            {
                System.Console.WriteLine($"Notebook {notebookName} does not exist");
            }
        }

        internal static void CreateNote(string title, string content, string notebookName, string tag = null)
        {
            var notebook = GetNotebook(notebookName);

            if (notebook != null)
            {
                ENNote newNote = new ENNote();
                newNote.Title = title;
                newNote.Content = ENNoteContent.NoteContentWithString(content);

                if (String.IsNullOrWhiteSpace(tag) == false)
                {
                    newNote.TagNames = new List<string>() { tag };
                }

                ENSession.SharedSession.UploadNote(newNote, notebook);

                System.Console.WriteLine($"Code note {title} has been created");
            }
            else
            {
                System.Console.WriteLine($"Notebook {notebookName} does not exist");
            }
        }

    }


    #endregion

}
