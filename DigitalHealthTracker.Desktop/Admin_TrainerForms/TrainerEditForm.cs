using System;
using System.Windows.Forms;
using DigitalHealthTracker.Data.Entities;

namespace DigitalHealthTracker.Desktop
{
	public partial class TrainerEditForm : Form
	{
		private int? trainerIdEdit;
		private Trainer _trainer = new Trainer();

		public Trainer EditedTrainer => _trainer;

		public TrainerEditForm()
		{
			InitializeComponent();
		}

		// ✅ Admin editlerken hideApproval=false, Trainer editlerken hideApproval=true göndereceğiz
		public TrainerEditForm(Trainer tempTrainer, bool hideApproval = false) : this()
		{
			trainerIdEdit = tempTrainer.Id;

			txtName.Text = tempTrainer.Name;
			txtSurname.Text = tempTrainer.Surname;
			txtPhone.Text = tempTrainer.Phone;
			txtEmail.Text = tempTrainer.Email;


			chkIsApproved.Checked = tempTrainer.IsApproved;

			if (hideApproval)
			{
				chkIsApproved.Visible = false;
				// lblIsApproved.Visible = false;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			// ✅ Zorunlu alanlar: Name, Surname, Phone (BirthYear zorunlu değil)
			if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text))
			{
				MessageBox.Show("Name and Surname are required.");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtPhone.Text))
			{
				MessageBox.Show("Phone number is required.");
				return;
			}

			// ✅ BirthYear opsiyonel: boşsa 0, doluysa validate et
			int birthYear = 0;

			_trainer = new Trainer
			{
				Id = trainerIdEdit ?? 0,
				Name = txtName.Text.Trim(),
				Surname = txtSurname.Text.Trim(),
				Phone = txtPhone.Text.Trim(),
				Email = txtEmail.Text.Trim(),

				// ✅ Opsiyonel alan
				BirthYear = birthYear,

				// ✅ Admin checkbox'ı (görünmez olsa bile value taşınır)
				IsApproved = chkIsApproved.Checked
			};

			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
