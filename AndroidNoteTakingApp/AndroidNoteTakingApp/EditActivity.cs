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
    [Activity(Label = "EditActivity", NoHistory = true)]
    public class EditActivity : Activity
    {
        EditText etEditTitle;
        TextView tvEditTime;
        EditText etEditNote;
        Button btnEditEdit;
        Button btnEditDelete;
        Button btnEditSave;
        Button btnEditReturn;
        int Id;
        string myTitle;
        string Description;
        string Time;

        DatabaseManager objdb;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Edit);

            etEditTitle = FindViewById<EditText>(Resource.Id.etEditTitle);
            tvEditTime = FindViewById<TextView>(Resource.Id.tvEditTime);
            etEditNote = FindViewById<EditText>(Resource.Id.etEditNote);
            btnEditEdit = FindViewById<Button>(Resource.Id.btnEditEdit);
            btnEditDelete = FindViewById<Button>(Resource.Id.btnEditDelete);
            btnEditSave = FindViewById<Button>(Resource.Id.btnEditSave);
            btnEditReturn = FindViewById<Button>(Resource.Id.btnEditReturn);

            objdb = new DatabaseManager();

             Id = Intent.GetIntExtra("Id",0);
             myTitle = Intent.GetStringExtra("Title");
             Description = Intent.GetStringExtra("Description");
             Time = Intent.GetStringExtra("Time");

            etEditTitle.Text = myTitle;
            tvEditTime.Text = Time;
            etEditNote.Text = Description;

            btnEditEdit.Click += BtnEditEdit_Click;
            btnEditDelete.Click += BtnEditDelete_Click;
            btnEditSave.Click += BtnEditSave_Click;
            btnEditReturn.Click += BtnEditReturn_Click;
        }

        private void BtnEditReturn_Click(object sender, EventArgs e)
        {
            //this.Finish();
            StartActivity(typeof(MainActivity));

            //throw new NotImplementedException();
        }

        private void BtnEditSave_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetCancelable(true);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Date saved");
            alertDialog.SetIcon(Resource.Drawable.Icon);
            alertDialog.SetMessage("Do you wish to save this note with the original date or todays date?");

            //YES
            alertDialog.SetButton("Original date", (s, ev) =>
            {
                Description = etEditNote.Text;

                objdb.SaveNoteOldDate(Id, myTitle, Description);

                this.Finish();
                StartActivity(typeof(MainActivity));

            });

            //no
            alertDialog.SetButton2("CANCEL", (s, ev) =>
            {
                // do something
            });

            alertDialog.SetButton3("Todays date", (s, ev) =>
            {
                 Description = etEditNote.Text;

                objdb.SaveNoteTodaysDate(Id, myTitle, Description);

                this.Finish();
                StartActivity(typeof(MainActivity));
            });

            alertDialog.Show();
        }

        private void BtnEditDelete_Click(object sender, EventArgs e)
        {                   
            Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Delete Note");
            alertDialog.SetIcon(Resource.Drawable.Icon);
            alertDialog.SetMessage("Are you sure you wish to delete this Note?");

            //YES
            alertDialog.SetButton("YES", (s, ev) =>
            {
                // do something
                objdb.DeleteNote(Id);
                this.Finish();
                StartActivity(typeof(MainActivity));
            });

            //no
            alertDialog.SetButton2("NO", (s, ev) =>
            {
                // do something
            });

            alertDialog.Show();                  
        }

        private void BtnEditEdit_Click(object sender, EventArgs e)
        {
            etEditNote.Enabled = true;           
        }
    }
}