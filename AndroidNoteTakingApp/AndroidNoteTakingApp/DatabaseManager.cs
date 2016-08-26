using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace AndroidNoteTakingApp
{

    public class DatabaseManager
    {
        static String databaseName = "note.db";
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);        
              
        public DatabaseManager()
        {

        }

        public void InitializeDatabase()
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, databaseName)))
                {
                    conn.CreateTable<Note>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public List<Note> ShowAllNotes()
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, databaseName)))
                {
                    List<Note> myList = new List<Note>();

                    var query = conn.Table<Note>().Select(p => p);

                    foreach (var item in query)
                    {
                        myList.Add(item);
                    }

                    return myList;

                    //    var query = conn.Query<Note>("Select * from ?");
                    //    return query;
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public void AddNote(string title, string note)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, databaseName)))
                {
                    var myNote = new Note { Title = title, Description = note, Time = System.DateTime.Now };
                    conn.Insert(myNote);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void DeleteNote(int Id)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, databaseName)))
                {                   
                    var query = conn.Table<Note>().Where(p => p.Id.Equals(Id));

                    foreach (var item in query)
                    {
                        conn.Delete(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void SaveNoteTodaysDate(int Id,string title, string note)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, databaseName)))
                {
                    Note myNote = new Note { Id = Id, Title = title, Description = note, Time = System.DateTime.Now };

                    var query = conn.Update(myNote);
                    

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void SaveNoteOldDate(int Id, string title, string note)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, databaseName)))
                {
                    Note myNote = conn.Get<Note>(p => p.Id.Equals(Id));
                    DateTime myTime = myNote.Time;

                    Note myNote1 = new Note { Id = Id, Title = title, Description = note, Time = myTime };

                    var query = conn.Update(myNote1);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public List<Note> SearchNotes(string searchString)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(folder, databaseName)))
                {
                    List<Note> myList = new List<Note>();

                    var query = conn.Table<Note>().Where(p => p.Title.Contains(searchString));

                    foreach (var item in query)
                    {
                        myList.Add(item);
                    }
                    return myList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}