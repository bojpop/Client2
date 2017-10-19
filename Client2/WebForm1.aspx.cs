using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Client2.EmployeeService;

namespace Client2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetEmployee_Click(object sender, EventArgs e)
        {
            EmployeeServiceClient client = new EmployeeServiceClient();
            Employee employee = client.GetEmployee(Convert.ToInt32(txtID.Text));

            if (employee.Type == Employee.EmployeeType.FullTimeEmployee)
            {
                txtAnnualSalary.Text =
                    ((EmployeeService.FullTimeEmployee)employee).AnnualSalary.ToString();
                trAnnualSalary.Visible = true;
                trHourlPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                txtHourlyPay.Text =
                    ((EmployeeService.PartTimeEmployee)employee).HourlyPay.ToString();
                txtHoursWorked.Text =
                    ((EmployeeService.PartTimeEmployee)employee).HoursWorked.ToString();
                trAnnualSalary.Visible = false;
                trHourlPay.Visible = true;
                trHoursWorked.Visible = true;
            }
            ddlEmployeeType.SelectedValue = ((int)employee.Type).ToString();

            txtName.Text = employee.Name;
            txtGender.Text = employee.Gender;
            txtDateOfBirth.Text = employee.DateOfBirth.ToShortDateString();
            lblMessage.Text = "Employee retreived";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeServiceClient client = new EmployeeServiceClient();
            Employee employee = null;

            if (((Employee.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) 
                == Employee.EmployeeType.FullTimeEmployee)
            {
                employee = new EmployeeService.FullTimeEmployee
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = Employee.EmployeeType.FullTimeEmployee,
                    AnnualSalary = Convert.ToInt32(txtAnnualSalary.Text),
                };
                client.SetEmployee(employee);
                lblMessage.Text = "Employee saved";
            }
            else if (((Employee.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) 
                     == Employee.EmployeeType.PartTimeEmployee)
            {
                employee = new EmployeeService.PartTimeEmployee
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = Employee.EmployeeType.PartTimeEmployee,
                    HourlyPay = Convert.ToInt32(txtHourlyPay.Text),
                    HoursWorked = Convert.ToInt32(txtHoursWorked.Text),
                };
                client.SetEmployee(employee);
                lblMessage.Text = "Employee saved";
            }
            else
            {
                lblMessage.Text = "Please select Employee Type";
            }
        }

        protected void ddlEmployeeType_SelectedIndexChanged
            (object sender, EventArgs e)
        {
            if (ddlEmployeeType.SelectedValue == "-1")
            {
                trAnnualSalary.Visible = false;
                trHourlPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else if (ddlEmployeeType.SelectedValue == "1")
            {
                trAnnualSalary.Visible = true;
                trHourlPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                trAnnualSalary.Visible = false;
                trHourlPay.Visible = true;
                trHoursWorked.Visible = true;
            }
        } 

    }
}