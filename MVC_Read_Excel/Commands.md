# Installing PAckages

dotnet add package Microsoft.EntityFrameworkCore -v 6.0.0          

dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 6.0.0

dotnet add package Microsoft.EntityFrameworkCore.Design -v 6.0.0

dotnet add package Microsoft.EntityFrameworkCore.Tools -v 6.0.0

dotnet add package ExcelDataReader.DataSet

dotnet add package System.Text.Encoding.CodePages


# The Db First Approach

dotnet ef dbcontext scaffold "Data Source=127.0.0.1;Initial Catalog=Servicing;User Id=sa;Password=MyPass@word" Microsoft.EntityFrameworkCore.SqlServer -o Models
