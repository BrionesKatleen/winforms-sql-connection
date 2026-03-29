using System.Drawing.Text;
using TestWins.Controller;

using System.Drawing.Text;
using TestWins.Controller;

namespace TestWins;

public partial class Form1 : Form
{
    //business

    private readonly StudentController controller = new StudentController();
    public Form1()
    {
        InitializeComponent();


        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.AutoGenerateColumns = true;
        dataGridView1.MultiSelect = false;

        loadData();
    }

    private void loadData()
    {
        try
        {
            dataGridView1.DataSource = controller.getAll();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading data: " + ex.Message);
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var student = new Student
        {
            studentId = Guid.NewGuid().ToString(),
            Name = txtName.Text,
            age = int.TryParse(txtAge.Text, out int a) ? a : 0,
            course = txtCourse.Text
        };

        controller.createStudent(student);
        LoadData();
        ClearInputs();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        var currentRow = dataGridView1.CurrentRow;
        if (currentRow == null) return;

        var id = currentRow.Cells["studentId"]?.Value?.ToString();
        if (string.IsNullOrEmpty(id)) return;

        var student = new Student
        {
            studentId = id,
            Name = txtName.Text,
            age = int.TryParse(txtAge.Text, out int a) ? a : 0,
            course = txtCourse.Text
        };

        controller.update(student);
        LoadData();
        ClearInputs();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var currentRow = dataGridView1.CurrentRow;
        if (currentRow == null) return;

        var id = currentRow.Cells["studentId"]?.Value?.ToString();
        if (string.IsNullOrEmpty(id)) return;

        controller.delete(id);
        LoadData();
        ClearInputs();
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        var row = dataGridView1.Rows[e.RowIndex];
        txtName.Text = row.Cells["Name"]?.Value?.ToString() ?? "";
        txtAge.Text = row.Cells["age"]?.Value?.ToString() ?? "";
        txtCourse.Text = row.Cells["course"]?.Value?.ToString() ?? "";
    }

    private void ClearInputs()
    {
        txtName.Text = "";
        txtAge.Text = "";
        txtCourse.Text = "";
    }

}
