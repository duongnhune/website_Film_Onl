using System;

namespace WebFilm.Controls.Utilities.Modal
{
    public partial class ModalFailure : System.Web.UI.UserControl
    {
        private string message;
        private bool isBack = false;

        public string Message { get => message; set => message = value; }
        public bool IsBack { get => isBack; set => isBack = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}