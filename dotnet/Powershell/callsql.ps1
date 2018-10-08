$connection = New-Object System.Data.SqlClient.SqlConnection
$connection.ConnectionString = 'user id=entUser;password=a1DOsncW9gNO2B_JG9;Data Source=tcp:dvmwazeus04.cloudapp.net,1433;Initial Catalog=entitlement;connection timeout=30'
$connection.Open()
$cmd = New-Object System.Data.SqlClient.SqlCommand
$cmd.CommandText = "select * from Users"
$cmd.Connection = $connection
$results = New-Object System.Data.DataTable
$reader = $cmd.ExecuteReader()
$results.Load($reader)
$connection.Close()
Write-Host ("Returned " + $results.Rows.Count + " rows.")
