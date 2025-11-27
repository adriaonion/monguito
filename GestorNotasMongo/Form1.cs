using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson; 

namespace GestorNotasMongo
{
    public partial class Form1 : Form
    {
       
        MongoCRUD db = new MongoCRUD();
        private readonly string NombreColeccion = "Notas";

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            CargarListaNotas();
        }

        private void CargarListaNotas()
        {
            try
            {
                var registros = db.ConectarColeccion<Nota>(NombreColeccion);
                
                var listaNotas = registros.Find(_ => true).ToList();
                dgvNotas.DataSource = listaNotas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar lista: " + ex.Message);
            }
        }

       
        private void dgvNotas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvNotas.Columns.Count > 0)
            {
                if (dgvNotas.Columns.Contains("Id")) dgvNotas.Columns["Id"].Visible = false;
                if (dgvNotas.Columns.Contains("Tags")) dgvNotas.Columns["Tags"].Visible = false;
                dgvNotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        
        private void dgvNotas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow fila = dgvNotas.Rows[e.RowIndex];

                    
                    txtTitulo.Tag = fila.Cells["Id"].Value?.ToString();

                   
                    txtTitulo.Text = fila.Cells["Titulo"].Value?.ToString();
                    txtContenido.Text = fila.Cells["Contenido"].Value?.ToString();

                   
                    if (fila.Cells["Tags"].Value is List<string> tagsList)
                    {
                        txtTags.Text = string.Join(", ", tagsList);
                    }
                    else
                    {
                        txtTags.Text = "";
                    }
                }
                catch {  }
            }
        }

        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtTitulo.Clear();
            txtContenido.Clear();
            txtTags.Clear();
            txtTitulo.Tag = null; 
        }

        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtContenido.Text))
            {
                MessageBox.Show("Pon al menos Título y Contenido.");
                return;
            }

            try
            {
                var coleccion = db.ConectarColeccion<Nota>(NombreColeccion);

                
                List<string> misTags = new List<string>();
                if (!string.IsNullOrWhiteSpace(txtTags.Text))
                {
                    misTags = txtTags.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                          .Select(x => x.Trim()).ToList();
                }

              
                Nota miNota = new Nota
                {
                    Titulo = txtTitulo.Text,
                    Contenido = txtContenido.Text,
                    FechaCreacion = DateTime.Now,
                    Tags = misTags
                };

                
                string idString = txtTitulo.Tag as string;

                if (string.IsNullOrEmpty(idString))
                {
                   
                    coleccion.InsertOne(miNota);
                    MessageBox.Show("¡Guardado correctamente!");
                }
                else
                {
                    
                    miNota.Id = idString; // Asegurar que el objeto tenga el mismo ID
                    var filtro = Builders<Nota>.Filter.Eq("_id", ObjectId.Parse(idString));
                    coleccion.ReplaceOne(filtro, miNota);
                    MessageBox.Show("¡Actualizado correctamente!");
                }

              
                CargarListaNotas();
                btnLimpiar_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Guardar: " + ex.Message);
            }
        }

      
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idString = txtTitulo.Tag as string;
            if (string.IsNullOrEmpty(idString))
            {
                MessageBox.Show("Selecciona una nota primero.");
                return;
            }

            if (MessageBox.Show("¿Borrar nota?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var coleccion = db.ConectarColeccion<Nota>(NombreColeccion);
                    var filtro = Builders<Nota>.Filter.Eq("_id", ObjectId.Parse(idString));
                    coleccion.DeleteOne(filtro);

                    MessageBox.Show("¡Nota eliminada!");
                    CargarListaNotas();
                    btnLimpiar_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }
    }
}