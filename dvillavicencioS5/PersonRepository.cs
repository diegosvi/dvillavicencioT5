using dvillavicencioS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvillavicencioS5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
                    conn = new(_dbPath);
                    conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath=dbPath;
        }

        public void AddNewPerson(string name)
        {
            int result = 0;

            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre es requerido");
                Persona person = new() { Name = name};
                result = conn.Insert(person);
                StatusMessage = string.Format("Se inserto una persona", result, name);
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error no se inserto", name, ex.Message);
            }
        }

        public List <Persona> getAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Erro al listar", ex.Message);
            }

            return new List<Persona>();
        }


        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();
                if (string.IsNullOrEmpty(newName))
                    throw new Exception("El nombre es requerido para la actualización.");

                var person = conn.Table<Persona>().FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    person.Name = newName;
                    conn.Update(person);
                    StatusMessage = $"Se actualizó la persona con ID {id} correctamente.";
                }
                else
                {
                    StatusMessage = $"No se encontró ninguna persona con ID {id}.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al actualizar la persona: {ex.Message}";
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                var person = conn.Table<Persona>().FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    conn.Delete(person);
                    StatusMessage = $"Se eliminó la persona con ID {id} correctamente.";
                }
                else
                {
                    StatusMessage = $"No se encontró ninguna persona con ID {id}.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar la persona: {ex.Message}";
            }
        }

    }
}
