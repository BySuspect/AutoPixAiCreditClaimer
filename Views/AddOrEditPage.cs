using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AutoPixAiCreditClaimer.Helpers;

namespace AutoPixAiCreditClaimer.Views
{
    public partial class AddOrEditPage : Form
    {
        private bool isEdit = false;
        private UserModel selected;

        public AddOrEditPage()
        {
            InitializeComponent();
        }

        public AddOrEditPage(UserModel _selected)
        {
            InitializeComponent();
            selected = _selected;
            isEdit = true;

            txtName.Text = selected.name;
            txtMail.Text = selected.email;
            txtPass.Text = selected.pass;
        }

        private void btnhideform_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrEmpty(txtName.Text)
                || string.IsNullOrEmpty(txtMail.Text)
                || string.IsNullOrEmpty(txtPass.Text)
            )
                return;

            var list = ListHelper.UserList;
            list = list.OrderBy(x => x.id).ToList<UserModel>();

            if (isEdit)
            {
                int index = list.FindIndex(x => x.id == selected.id);
                var edited = new UserModel
                {
                    id = list[index].id,
                    name = txtName.Text,
                    email = txtMail.Text,
                    pass = txtPass.Text,
                };

                list.RemoveAt(index);
                list.Insert(index, edited);
                list = list.OrderBy(x => x.id).ToList<UserModel>();
            }
            else
            {
                list.Add(
                    new UserModel
                    {
                        id = (list.Count == 0) ? 0 : list[list.Count - 1].id + 1,
                        name = txtName.Text,
                        email = txtMail.Text,
                        pass = txtPass.Text,
                    }
                );
            }

            ListHelper.UserList = list;

            DialogResult = DialogResult.OK;
        }

        #region Mouse move codes
        private Point mouseLoc;

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;
        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouseLoc.X;
                int dy = e.Location.Y - mouseLoc.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        #endregion
    }
}
