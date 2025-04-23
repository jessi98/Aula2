using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Aula2
{
    public partial class Form1: Form
    {
        MySqlConnection Conexao;

        // padrão: host user senha
        private string data_source = "datasource=localhost;username=root;password=;database=aulas_uc3";

        public int? id_contato_selecionado = null;
        public Form1()
        {
            InitializeComponent();



            // configurações
            lstContatos.View = View.Details;
            lstContatos.LabelEdit = true;
            lstContatos.AllowColumnReorder = true;
            lstContatos.FullRowSelect = true;
            lstContatos.GridLines = true;


            // Posição dos Cabeçalhos a serem exibidos na tela
            lstContatos.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Telefone", 150, HorizontalAlignment.Left);

            carregar_contatos();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                // Criar a conexão com o MySQL
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                if (id_contato_selecionado == null)
                {
                    // insert
                    cmd.Parameters.Clear(); // limpa os parâmetros antigos
                    cmd.CommandText =
                        "INSERT INTO contato " +
                        "(nome, email, telefone) " +
                        "VALUES " +
                        "(@nome, @email, @telefone)";

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contato Inserido com Sucesso", "Sucesso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    // update
                    cmd.Parameters.Clear(); // limpa os parâmetros antigos
                    cmd.CommandText =
                        "UPDATE contato " +
                        "SET nome = @nome, email = @email, telefone = @telefone " +
                        "WHERE id = @id";

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                    cmd.Parameters.AddWithValue("@id", id_contato_selecionado);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contato Atualizado com Sucesso", "Sucesso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                /* string sql = "INSERT INTO contato (nome, email, telefone) VALUES ('" + txtNome.Text + "','" + txtEmail.Text + "','" + txtTelefone.Text + "')";

               // Executar Comando Insert
               MySqlCommand insert = new MySqlCommand(sql, Conexao);

               Conexao.Open();
               insert.ExecuteReader(); */


                // Mostrando a Mensagem para o Usuário
                //MessageBox.Show("Dados Inseridos com Sucesso!!!");
                // apagando o formulario após a inserção dos dados
                id_contato_selecionado = null;
                txtNome.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtTelefone.Text = String.Empty;

                carregar_contatos();

                #region Código Antigo
                // SQL que desejamos executar - marcadores
                //cmd.CommandText =
                //    "INSERT INTO contato " +
                //    "(nome, email, telefone) " +
                //    "VALUES" +
                //    "(@nome, @email, @telefone)";

                //cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                //cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                //cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

                //cmd.ExecuteNonQuery();

                /* string sql = "INSERT INTO contato (nome, email, telefone) VALUES ('" + txtNome.Text + "','" + txtEmail.Text + "','" + txtTelefone.Text + "')";

                // Executar Comando Insert
                MySqlCommand insert = new MySqlCommand(sql, Conexao);

                Conexao.Open();
                insert.ExecuteReader(); */


                // Mostrando a Mensagem para o Usuário
                //MessageBox.Show("Dados Inseridos com Sucesso!!!");
                //MessageBox.Show("Contato Inserido com Sucesso ", "Sucesso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion


            }
            catch (MySqlException ex)

            {
                MessageBox.Show("Error " + "has occured: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Has occured: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void carregar_contatos()
        {
            try
            {

                // Criar a conexão com o MySQL
                Conexao = new MySqlConnection(data_source);

                string sql = "SELECT * FROM contato ORDER BY id ASC";

                Conexao.Open();

                // Buscar as informações
                MySqlCommand buscar = new MySqlCommand(sql, Conexao);

                // armazena as informacoes que temos na busca para mostrar na tela
                MySqlDataReader reader = buscar.ExecuteReader();

                // como iremos mostrar os dados na tela para o usuário
                lstContatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        // obtendo as informações do banco de dados (vetor de strings)
                        reader.GetInt32(0).ToString(), // id
                        reader.GetString(1),           // nome
                        reader.GetString(2),           // email
                        reader.GetString(3),           // telefone
                    };

                    var linha_list_view = new ListViewItem(row);
                    lstContatos.Items.Add(linha_list_view);
                }


            }
            catch (MySqlException ex)

            {
                MessageBox.Show("Error " + "has occured: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Has occured: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itens_selecionados = lstContatos.SelectedItems;

            /* percorrendo a coleção de itens dentro da lista itens_selecionados
            // Obs¹: A minha linha toda é um item, que contem os subItems (colunas) que desejo selecionar

            foreach (ListViewItem item in itens_selecionados)
            {
                id_contato_selecionado = Convert.ToInt32(item.SubItems[0].Text);
                // extrai o valor de cada uma das variáveis (colunas)
                txtNome.Text = item.SubItems[1].Text;
                txtEmail.Text = item.SubItems[2].Text;
                txtTelefone.Text = item.SubItems[3].Text;

                //  MessageBox.Show("Id Selecionado = " + id_contato_selecionado);*/
        }


        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

                string q = "%" + txtBuscar.Text + "%";

                // Criar a conexão com o MySQL
                Conexao = new MySqlConnection(data_source);

                string sql = "SELECT * FROM contato WHERE nome LIKE @q OR email LIKE @q";


                Conexao.Open();

                // Buscar as informações
                MySqlCommand buscar = new MySqlCommand(sql, Conexao);
                buscar.Parameters.AddWithValue("@q", q);

                // armazena as informacoes que temos na busca para mostrar na tela
                MySqlDataReader reader = buscar.ExecuteReader();


                lstContatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        // obtendo as informações do banco de dados (vetor de strings)
                        reader.GetInt32(0).ToString(), // id
                        reader.GetString(1),           // nome
                        reader.GetString(2),           // email
                        reader.GetString(3),           // telefone
                    };

                    var linha_list_view = new ListViewItem(row);
                    lstContatos.Items.Add(linha_list_view);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void lstContatos_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection itens_selecionados = lstContatos.SelectedItems;

            // percorrendo a coleção de itens dentro da lista itens_selecionados
            // Obs¹: A minha linha toda é um item, que contem os subItems (colunas) que desejo selecionar

            foreach (ListViewItem item in itens_selecionados)
            {
                id_contato_selecionado = Convert.ToInt32(item.SubItems[0].Text);
                // extrai o valor de cada uma das variáveis (colunas)
                txtNome.Text = item.SubItems[1].Text;
                txtEmail.Text = item.SubItems[2].Text;
                txtTelefone.Text = item.SubItems[3].Text;

                //  MessageBox.Show("Id Selecionado = " + id_contato_selecionado);
            }
        }

    }
}
