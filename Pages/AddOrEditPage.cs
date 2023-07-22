using AutoPixAiCreditClaimer.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoPixAiCreditClaimer.Pages
{
    // A partial class representing a form for adding or editing user items
    public partial class AddOrEditPage : Form
    {
        bool isEdit = false; // Flag to indicate whether it's an edit operation or not
        UserItems selected; // Selected UserItems object for editing

        public AddOrEditPage()
        {
            InitializeComponent();
        }

        // Constructor for editing an existing user item
        public AddOrEditPage(UserItems _selected)
        {
            InitializeComponent();
            selected = _selected;
            isEdit = true;

            // Fill the text boxes with the selected UserItems data for editing
            txtName.Text = selected.name;
            txtMail.Text = selected.email;
            txtPass.Text = selected.pass;
        }

        // Event handler for the "Cancel" button click
        private void btnhideform_Click(object sender, EventArgs e)
        {
            // Set the form's DialogResult to Cancel, indicating the cancel action
            DialogResult = DialogResult.Cancel;
        }

        // Event handler for the "Save" button click
        private void btnsave_Click(object sender, EventArgs e)
        {
            // Check if any of the required fields (name, email, password) are empty, and return if so
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtPass.Text))
                return;

            var list = ListHelper.UserList;
            list = list.OrderBy(x => x.id).ToList<UserItems>();

            if (isEdit)
            {
                // If it's an edit operation, update the selected UserItems object in the list
                int index = list.FindIndex(x => x.id == selected.id);
                var edited = new UserItems
                {
                    id = list[index].id,
                    name = txtName.Text,
                    email = txtMail.Text,
                    pass = txtPass.Text,
                };

                list.RemoveAt(index);
                list.Insert(index, edited);
                list = list.OrderBy(x => x.id).ToList<UserItems>();
            }
            else
            {
                // If it's not an edit operation, add a new UserItems object to the list
                list.Add(new UserItems
                {
                    id = (list.Count == 0) ? 0 : list[list.Count - 1].id + 1,
                    name = txtName.Text,
                    email = txtMail.Text,
                    pass = txtPass.Text,
                });
            }

            // Update the UserList in ListHelper with the modified list
            ListHelper.UserList = list;

            // Set the form's DialogResult to OK, indicating the successful completion of the operation
            DialogResult = DialogResult.OK;
        }


        #region Mouse move codes

        // Track the mouse location when the form is clicked
        private Point _mouseLoc;

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            _mouseLoc = e.Location;
        }

        // Move the form with the mouse movement
        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouseLoc.X;
                int dy = e.Location.Y - _mouseLoc.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        #endregion
    }
}