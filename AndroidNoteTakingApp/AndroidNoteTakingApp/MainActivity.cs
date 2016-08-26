using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using System.Collections.Generic;
using System.IO;

namespace AndroidNoteTakingApp
{
    [Activity(Label = "AndroidNoteTakingApp")]
    public class MainActivity : Activity
    {
        EditText etSearchTitle;
        Button btnSearchAdd;
        ListView lvNotes;
        DatabaseManager objdb;
        List<Note> myList;
        ArrayAdapter listAdapter;
        string searchString;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            etSearchTitle = FindViewById<EditText>(Resource.Id.etSearchTitle);
            btnSearchAdd = FindViewById<Button>(Resource.Id.btnSearchAdd);
            lvNotes = FindViewById<ListView>(Resource.Id.lvNotes);

            myList = new List<Note>();

            objdb = new DatabaseManager();
            objdb.InitializeDatabase();

            listAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1);
            lvNotes.Adapter = listAdapter;
            
            displayAllNotes();

            etSearchTitle.TextChanged += EtSearchTitle_TextChanged;

            btnSearchAdd.Click += btnSearchAdd_Click;
            lvNotes.ItemClick += LvNotes_ItemClick;
        }

        private void EtSearchTitle_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            searchString = etSearchTitle.Text;

            List<Note> searchList = objdb.SearchNotes(searchString);

            listAdapter.Clear();
           
            if (searchList != null)
            {
                foreach (var item in searchList)
                {
                    listAdapter.Add(item.Title + "\n" + item.Time);
                }

                listAdapter.NotifyDataSetChanged();
            }
            else
            {
                displayAllNotes();
            }

            //throw new NotImplementedException();
        }

        private void LvNotes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Note temp = myList[e.Position];

            var myIntent = new Intent(this, typeof(EditActivity));

            myIntent.PutExtra("Id", temp.Id);
            myIntent.PutExtra("Title", temp.Title);
            myIntent.PutExtra("Description", temp.Description);
            myIntent.PutExtra("Time", temp.Time.ToString());

            StartActivity(myIntent);

            //throw new NotImplementedException();
        }

        private void btnSearchAdd_Click(object sender, EventArgs e)
        {
            var myIntent = new Intent(this, typeof(AddActivity));
            myIntent.PutExtra("Title", etSearchTitle.Text);

            Finish();
            StartActivity(myIntent);
        }
        
        private void displayAllNotes()
        {
            listAdapter.Clear();
            myList = objdb.ShowAllNotes();

            if (myList != null)
            {
                foreach (var item in myList)
                {
                    listAdapter.Add(item.Title + "\n" + item.Time);
                }

                listAdapter.NotifyDataSetChanged();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            objdb = new DatabaseManager();
            displayAllNotes();
        }
    }
}

