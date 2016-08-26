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
    public class Note
    {
        [PrimaryKey, AutoIncrement ]
        public int Id { get; set; }
        [Indexed]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
}