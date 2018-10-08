# declare TVP structure

$expirationsTvp = New-Object System.Data.DataTable
$objectIdColumn = New-Object System.Data.DataColumn
$objectIdColumn.ColumnName = 'ObjectId'
$objectIdColumn.DataType = [Guid]
$expirationColumn = New-Object System.Data.DataColumn
$expirationColumn.ColumnName = 'Expiration'
$expirationColumn.DataType = [DateTime]
$expirationsTvp.Columns.Add($objectIdColumn)
$expirationsTvp.Columns.Add($expirationColumn)

# add rows to TVP

$row1 = $expirationsTvp.NewRow()
$row1.Item('ObjectId') = '00000000-0000-0000-0000-000000000001'
$row1.Item('Expiration') = '2000-01-01 00:00:00.000'
$expirationsTvp.Rows.Add($row1)

$row2 = $expirationsTvp.NewRow()
$row2.Item('ObjectId') = '00000000-0000-0000-0000-000000000002'
$row2.Item('Expiration') = '2000-01-02' # dates are accepted without time part
$expirationsTvp.Rows.Add($row2)

# open connection

$connection = New-Object System.Data.SqlClient.SqlConnection
$connection.ConnectionString = 'user id=entUser;password=a1DOsncW9gNO2B_JG9;Data Source=tcp:dvmwazeus04.cloudapp.net,1433;Initial Catalog=entitlement;connection timeout=30'
$connection.Open()

# create command and pass TVP as parameter

$cmd = New-Object System.Data.SqlClient.SqlCommand
$cmd.CommandType = [System.Data.CommandType]’StoredProcedure’
$cmd.CommandText = "AddPasswordExpirationForUsers"
$cmd.Connection = $connection

$cmd.Parameters.Add("@PasswordExpirations", [System.Data.SqlDbType]::Structured)
$cmd.Parameters["@PasswordExpirations"].Value = $expirationsTvp

# execute command and get results into a dataset

$adapter = New-Object System.Data.SqlClient.SqlDataAdapter
$adapter.SelectCommand = $cmd
$dataSet = New-Object System.Data.DataSet
$adapter.Fill($dataSet)
$connection.Close()

# access resulting tables

#$dataSet.Tables[0]