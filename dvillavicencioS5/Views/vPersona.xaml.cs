using dvillavicencioS5.Models;

namespace dvillavicencioS5.Views;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}



    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.personRepo.AddNewPerson(txtName.Text);
        lblStatus.Text = App.personRepo.StatusMessage;
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people = App.personRepo.getAllPeople();
        ListaPersona.ItemsSource = people;
        lblStatus.Text = App.personRepo.StatusMessage;
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        Persona selectedPerson = (Persona)ListaPersona.SelectedItem;

        if (selectedPerson != null)
        {
            string newName = txtName.Text;

            if (!string.IsNullOrWhiteSpace(newName))
            {
                App.personRepo.UpdatePerson(selectedPerson.Id, newName);
                btnObtener_Clicked(sender, e);
                lblStatus.Text = "Registro actualizado correctamente.";
            }
            else
            {
                lblStatus.Text = "Por favor, ingrese un nuevo nombre.";
            }
        }
        else
        {
            lblStatus.Text = "Por favor, seleccione una persona para actualizar.";
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        if (ListaPersona.SelectedItem != null)
        {
            Persona selectedPerson = (Persona)ListaPersona.SelectedItem;
            App.personRepo.DeletePerson(selectedPerson.Id);
            btnObtener_Clicked(sender, e);

            lblStatus.Text = "Registro eliminado correctamente.";
        }
        else
        {
            lblStatus.Text = "Por favor, selecciona una persona para eliminar.";
        }
    }

    private void ListaPersona_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Persona selectedPerson = (Persona)ListaPersona.SelectedItem;
    }
}