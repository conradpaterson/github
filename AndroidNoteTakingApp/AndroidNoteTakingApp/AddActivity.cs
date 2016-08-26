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

namespace AndroidNoteTakingApp
{
    [Activity(Label = "AddActivity",NoHistory = true)]
    public class AddActivity : Activity
    {
        EditText etNoteTitle;
        EditText etNoteDescription;
        Button btnAddSave;
        Button btnAddReturn;

        DatabaseManager objdb;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Add);

            etNoteTitle = FindViewById<EditText>(Resource.Id.etNoteTitle);
            etNoteDescription = FindViewById<EditText>(Resource.Id.etNoteDescription);
            btnAddSave = FindViewById<Button>(Resource.Id.btnAddSave);
            btnAddReturn = FindViewById<Button>(Resource.Id.btnAddReturn);

            objdb = new DatabaseManager();

            etNoteTitle.Text = Intent.GetStringExtra("Title");

            btnAddSave.Click += BtnAddSave_Click;
            btnAddReturn.Click += BtnAddReturn_Click;
        }

        private void BtnAddReturn_Click(object sender, EventArgs e)
        {
            //this.Finish();
            StartActivity(typeof(MainActivity));

            //throw new NotImplementedException();
        }

        private void BtnAddSave_Click(object sender, EventArgs e)
        {
            if (etNoteTitle.Text != "" && etNoteDescription.Text != "")
            {
                objdb.AddNote(etNoteTitle.Text, etNoteDescription.Text);
                Toast.MakeText(this, "Note Added", ToastLength.Long).Show();

                this.Finish();
                StartActivity(typeof(MainActivity));
            }
        }

      
    }
}