using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroup2_Midterm_ver0902
{
    public partial class Modify : Form
    {
        public Modify()
        {
            InitializeComponent();
        }
        MingSuEntities ms = new MingSuEntities();
        int id;
        string name;
        DateTime date;
        int capacity;
        string status;
        string info;
        private void getData()
        {
            if (txt_aID.Text != "")
                id = int.Parse(txt_aID.Text);
            name = txt_aName.Text;
            date = Convert.ToDateTime(txt_aDate.Text);
            capacity = Convert.ToInt32(txt_aCapacity.Text);
            status = txt_aStatus.Text;
            info = txt_aInfo.Text;
        }
        private void query()
        {
            var q = from a in ms.Activities
                    select a;
            this.dataGridView1.DataSource = q.ToList();
            setGridStyle();
        }
        private void CellClick()
        {            
            txt_aName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_aDate.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_aCapacity.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txt_aStatus.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_aInfo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txt_aID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
        private void setGridStyle()
        {
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 140;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 100;
            bool isColorChanged = false;
            Color bgc;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;
                bgc = Color.AliceBlue;
                if (isColorChanged)
                {
                    bgc = Color.LavenderBlush;
                }
                foreach (DataGridViewCell c in r.Cells)
                {
                    c.Style.BackColor = bgc;
                }
            }
        }
        private void txtClear()
        {
            txt_aName.Text = "";
            txt_aDate.Text = "";
            txt_aCapacity.Text = "";
            txt_aStatus.Text = "";
            txt_aInfo.Text = "";
            txt_aID.Text = "";
        }
        private void btn_Show_Click(object sender, EventArgs e)
        {
            try
            {
                query();
                setGridStyle();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("查無資料");
            }
            catch (Exception)
            {
                MessageBox.Show("程式執行發生問題，請重試");
            }
        }
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            try
            {
                getData();
                Activity act = new Activity
                {
                    ActivityName = name,
                    ActivityDate = date,
                    ActivityCapacity = capacity,
                    ActivityStatus = status,
                    ActivityInfo = info,
                    //ActivityID = id
                };
                ms.Activities.Add(act);
                ms.SaveChanges();
                MessageBox.Show("Insert Success");
                query();
            }
            catch (Exception)
            {
                MessageBox.Show("Insert error, please retry !");
            }
        }
        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                getData();
                var q = from a in ms.Activities
                        where a.ActivityID == id
                        select a;
                foreach (var a in q)
                {
                    a.ActivityName = name;
                    a.ActivityDate = date;
                    a.ActivityCapacity = capacity;
                    a.ActivityStatus = status;
                    a.ActivityInfo = info;
                }
                ms.SaveChanges();
                MessageBox.Show("Update Success");
                query();
            }
            catch (Exception)
            {
                MessageBox.Show("Update error, please retry !");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                getData();
                var q = (from a in ms.Activities
                         where (a.ActivityName == name &&
                         a.ActivityDate ==  date &&
                         a.ActivityCapacity == capacity &&
                         a.ActivityStatus == status &&
                         a.ActivityInfo == info &&
                         a.ActivityID == id)
                         select a).FirstOrDefault();
                ms.Activities.Remove(q);
                ms.SaveChanges();
                MessageBox.Show("Deleted !!");
                query();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("資料庫已無資料");
            }
            catch (Exception)
            {
                MessageBox.Show("Delete error, please retry !");
            }
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if ("".Equals(txt_aID.Text) && "".Equals(txt_aDate.Text) && "".Equals(txt_aCapacity.Text) && "".Equals(txt_aStatus.Text) && "".Equals(txt_aInfo.Text) && "".Equals(txt_aName.Text))
            {
                MessageBox.Show("No data entered !");
            }
            else
            {
                try
                {
                    IQueryable<Activity> q;                    
                    if ("".Equals(txt_aID.Text))
                    {
                        //i0
                        if ("".Equals(txt_aDate.Text))
                        {
                            //i0d0
                            if ("".Equals(txt_aCapacity.Text))
                            {
                                //i0d0c0
                                name = txt_aName.Text;
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                                id = -1;
                                capacity = -1;
                                date = Convert.ToDateTime("1900/1/1");
                            }
                            else
                            {
                                //i0d0c1
                                name = txt_aName.Text;
                                capacity = Convert.ToInt32(txt_aCapacity.Text);
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                                id = -1;
                                date = Convert.ToDateTime("1900/1/1");
                            }
                        }
                        else
                        {
                            //i0d1
                            if ("".Equals(txt_aCapacity.Text))
                            {
                                //i0d1c0
                                name = txt_aName.Text;
                                date = Convert.ToDateTime(txt_aDate.Text);
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                                id = -1;
                                capacity = -1;
                            }
                            else
                            {
                                //i0d1c1
                                name = txt_aName.Text;
                                date = Convert.ToDateTime(txt_aDate.Text);
                                capacity = Convert.ToInt32(txt_aCapacity.Text);
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                                id = -1;
                            }
                        }
                    }
                    else
                    {
                        //i1
                        if ("".Equals(txt_aDate.Text))
                        {
                            //i1d0
                            if ("".Equals(txt_aCapacity.Text))
                            {
                                //i1d0c0
                                id = int.Parse(txt_aID.Text);
                                name = txt_aName.Text;
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                                capacity = -1;
                                date = Convert.ToDateTime("1900/1/1");
                            }
                            else
                            {
                                //i1d0c1
                                id = int.Parse(txt_aID.Text);
                                name = txt_aName.Text;
                                capacity = Convert.ToInt32(txt_aCapacity.Text);
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                                date = Convert.ToDateTime("1900/1/1");
                            }
                        }
                        else
                        {
                            //i1d1
                            if ("".Equals(txt_aCapacity.Text))
                            {
                                //i1d1c0
                                id = int.Parse(txt_aID.Text);
                                name = txt_aName.Text;
                                date = Convert.ToDateTime(txt_aDate.Text);
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                                capacity = -1;
                            }
                            else
                            {
                                //i1d1c1
                                id = int.Parse(txt_aID.Text);
                                name = txt_aName.Text;
                                date = Convert.ToDateTime(txt_aDate.Text);
                                capacity = Convert.ToInt32(txt_aCapacity.Text);
                                status = txt_aStatus.Text;
                                info = txt_aInfo.Text;
                            }
                        }
                    }
                    q = from a in ms.Activities
                        where ((a.ActivityName.Contains(name) &&
                             a.ActivityStatus.Contains(status) &&
                             a.ActivityInfo.Contains(info)) ||
                             (a.ActivityCapacity == capacity ||
                             a.ActivityDate == date ||
                             a.ActivityID == id))
                        select a;
                    this.dataGridView1.DataSource = q.ToList();
                    setGridStyle();
                    if (this.dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found !");
                    }
                    else
                    {
                        CellClick();
                    }

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Data not found !");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error, please retry !!!!");
                }
            }
        }
        private void btn_SaveandClose_Click(object sender, EventArgs e)
        {
            ms.SaveChanges();
            MessageBox.Show("Saved !");
            this.Close();
        }

        private void Modify_Load(object sender, EventArgs e)
        {
            try
            {
                query();
                CellClick();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("資料庫已無資料");
            }
            catch (Exception)
            {
                MessageBox.Show("Error, please retry !");
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CellClick();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txtClear();
        }
    }
}
