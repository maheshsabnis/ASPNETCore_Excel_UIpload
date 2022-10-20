using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Read_Excel.Models;
using ExcelDataReader;
using System.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_Read_Excel.Controllers
{
    public class DataUploadController : Controller
    {
        IConfiguration configuration;
        IWebHostEnvironment hostEnvironment;
        ServicingContext context;
        IExcelDataReader reader;
        public DataUploadController(IConfiguration configuration, IWebHostEnvironment hostEnvironment, ServicingContext context)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            this.context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var customerResponseDetails = await context.CustomerResponseDetails.ToListAsync();
            return View(customerResponseDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            try
            {
                // Check the File is received

                if (file == null)
                    throw new Exception("File is Not Received...");


                // Create the Directory if it is not exist
                string dirPath = Path.Combine(hostEnvironment.WebRootPath, "ReceivedReports");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                // MAke sure that only Excel file is used 
                string dataFileName = Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(dataFileName);

                string[] allowedExtsnions = new string[] { ".xls", ".xlsx"};

                if (!allowedExtsnions.Contains(extension))
                    throw new Exception("Sorry! This file is not allowed, make sure that file having extension as either .xls or .xlsx is uploaded.");

                // Make a Copy of the Posted File from the Received HTTP Request
                string saveToPath = Path.Combine(dirPath, dataFileName);

                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

// USe this to handle Encodeing differences in .NET Core
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                // read the excel file
                using (var stream = new FileStream(saveToPath,FileMode.Open))
                {
                    if (extension == ".xls")
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        // Read the the Table
                        DataTable serviceDetails = ds.Tables[0];
                        for (int i = 1; i < serviceDetails.Rows.Count; i++)
                        {
                            CustomerResponseDetail details = new CustomerResponseDetail();
                            details.ServiceEngineerName = serviceDetails.Rows[i][0].ToString();
                            details.CustomerName = serviceDetails.Rows[i][1].ToString();
                            details.Address = serviceDetails.Rows[i][2].ToString();
                            details.City = serviceDetails.Rows[i][3].ToString();
                            details.ComplaintType = serviceDetails.Rows[i][4].ToString();
                            details.DeviceName = serviceDetails.Rows[i][5].ToString();
                            details.ComplaintDate = Convert.ToDateTime( serviceDetails.Rows[i][6].ToString());
                            details.VisitDate = Convert.ToDateTime(serviceDetails.Rows[i][7].ToString());
                            details.ComplaintDetails = serviceDetails.Rows[i][8].ToString();
                            details.RepairDetails = serviceDetails.Rows[i][9].ToString();
                            details.ResolveDate = Convert.ToDateTime(serviceDetails.Rows[i][10].ToString());

                            details.Fees = Convert.ToDecimal(serviceDetails.Rows[i][11].ToString());
                            details.UploadDate = DateTime.Now;


                            // Add the record in Database
                            await context.CustomerResponseDetails.AddAsync(details);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = this.RouteData.Values["controller"].ToString(),
                    ActionName = this.RouteData.Values["action"].ToString(),
                    ErrorMessage = ex.Message
                });
            }
        }

    }
}

