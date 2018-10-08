<#
# Script relies on the MSonline powershell modules which must be zipped and included with the Azure functions
#
#>
function Write-Message
{
	param (
		$message,
		$messageType
	)
	switch ($messageType.toLower())
	{
		"error" {
			Write-Output "ERROR::$(Get-Date):: $message"
		}
		"success" {
			Write-Output "SUCCESS::$(Get-Date):: $message"
		}
		"info" {
			Write-Output "INFO::$(Get-Date):: $message"
		}
		default
		{
			Write-Output "INFO::$(Get-Date):: $message"
		}
	}
}

#Connect to the service
function Connect-ToMsolService
{
	param (
		[string]$Username,
		[string]$Password
	)
	#Create Credential for login to  MSOnline
	$Credential = New-Object System.Management.Automation.PSCredential $Username, (ConvertTo-SecureString -AsPlainText $Password -Force)
	
	#Connect MSOLService
	Connect-MsolService -Credential $Credential
}

#Connect to Database
function Connect-ToDatabase
{
	param (
		$ConnectionString
	)
	
	[System.Data.SqlClient.SqlConnection]$conn = New-Object System.Data.SqlClient.SqlConnection
	$conn.ConnectionString = $ConnectionString
	return $conn
}

#Execute Stored Procedure
function Execute-StoredProcecdure
{
	param (
		[system.Data.SqlClient.SqlConnection]$connection,
		[System.Data.DataTable]$DataTable
	)
	try
	{
		$connection.Open()
		try
		{
			#Build Cmd
			$cmd = New-Object System.Data.SqlClient.SqlCommand
			$cmd.CommandType = [System.Data.CommandType]'StoredProcedure'
			$cmd.CommandText = 'AddPasswordExpirationForUsers'
			$cmd.Connection = $connection
			
			#Add Params
			$cmd.Parameters.Add("@PasswordExpirations", [System.Data.SqlDbType]::Structured)
			$cmd.Parameters["@PasswordExpirations"].Value = $DataTable
			
			#Create SQL Command Object that will execute our Stored Procedure
			$adapter = New-Object System.Data.SqlClient.SqlDataAdapter($cmd)
			$adapter.SelectCommand = $cmd
			$dataSet = New-Object System.Data.DataSet
			$rows = $adapter.Fill($dataSet)
			
		}
		catch
		{
			#TODO
			$connection.Close()
		}
	}
	catch [System.Data.SqlClient.SqlException] {
		$e = New-Object System.Data.SqlClient.SqlException
		throw $e
	}
	finally
	{
		$connection.Close()
	}
}
#Get all users from database
function Get-AllActiveUsers
{
	param (
		[system.Data.SqlClient.SqlConnection]$connection
	)
	[System.Collections.ArrayList]$Users = @()
	try
	{
		$connection.Open()
		Try
		{
			$cmd = New-Object System.Data.SqlClient.SqlCommand
			$cmd.CommandText = "SELECT ObjectId FROM Users WHERE IsDeleted != '1' AND ObjectId IS NOT NULL AND ObjectId != ''"
			$cmd.Connection = $connection
			
			$adapter = New-Object System.Data.SqlClient.SqlDataAdapter
			$adapter.SelectCommand = $cmd
			$Dataset = New-Object System.Data.DataSet
			$adapter.Fill($Dataset) | Out-Null
			$Users.Add($Dataset.Tables[0].ObjectId) | Out-Null
			
			$connection.Close()
		}
		catch
		{
			$connection.Close()
		}
	}
	catch
	{
	}
	return $Users.ToArray()
}

function Get-AllMSOLUsers
{
	$users = (Get-MsolUser -All)
	return $users
}

function Get-OnlyActiveUsers
{
	param (
		[system.Data.SqlClient.SqlConnection]$connection
	)
	$adUsers = Get-AllMSOLUsers
	$dbUsers = Get-AllActiveUsers -connection $connection
	$result = @()
	foreach ($au in $adUsers)
	{
		foreach ($du in $dbUsers)
		{
			if ($du.ToString() -eq $au.ObjectId.ToString())
			{
				$result += $au
			}
		}
	}
	return $result
}

function Send-EmailToUsers
{
	Invoke-RestMethod -Method GET -Uri "$($env:EntitlementsURI)/api/users/notifications/passwordexpirations/notify"
	if ($?)
	{
		Write-Message "Successfully emailed user(s)" -messageType "success"
	}
	else
	{
		Write-Message "Cannot execute sending password notification" -messageType "error"
	}
}

#NOW LETS DO WORK
try
{
	Write-Message -message "Connecting to MSOnline" -messageType "info"
	Connect-ToMsolService -Username $env:MSOLAdminUsername -Password $env:MSOLAdminPassword
	if ($?)
	{
		Write-Message -message "Successfully Connected to MSOnline" -messageType "success"
	}
	try
	{
		#Connect to DB and get all active users.
		Write-Message -message "Connecting to Database" -messageType "info"
		$dbconn = Connect-ToDatabase -ConnectionString $env:EntitlementsConnectionString
		try
		{
			Write-Message -message "Sorting Active Users List" -messageType "info"
			$allUsers = Get-OnlyActiveUsers -connection $dbconn
			Write-Message -message "$($allUsers.Count) users will be added" -messageType = "info"
			try
			{
				$userInserted = 0
				Write-Message -message "Starting to loop users" -messageType "info"
				#Initialize TVP
				[System.Data.DataTable]$expTvp = New-Object System.Data.DataTable
				$objIdColumn = New-Object System.Data.DataColumn
				$objIdColumn.ColumnName = "ObjectId"
				$objIdColumn.DataType = [Guid]
				$expDateColumn = New-Object System.Data.DataColumn
				$expDateColumn.ColumnName = "Expiration"
				$expDateColumn.DataType = [DateTime]
				$expTvp.Columns.Add($objIdColumn)
				$expTvp.Columns.Add($expDateColumn)
				try
				{
					#Loop through users
					foreach ($u in $allUsers)
					{
						$date = $u.LastPasswordChangeTimestamp.AddDays($env:PasswordExpirationInDays)
						if ($date -ne $null)
						{
							#Add The datas to Table Value Parameters object
							$row = $expTvp.NewRow()
							$row.Item('ObjectId') = $u.ObjectId
							$row.Item('Expiration') = $date
							$expTvp.Rows.Add($row)
							$userInserted = $userInserted + 1
							if ($expTvp.Rows.Count -ge 1)
							{
								Execute-StoredProcecdure -connection $dbconn -DataTable $expTvp | Out-Null
								if ($?)
								{
									$expTvp.Rows.Clear()
								}
							}
							
						}
						
					}
					Execute-StoredProcecdure -connection $dbconn -DataTable $expTvp | Out-Null
					if ($?)
					{
						Write-Message -message "Successfully added $userInserted users and password expiration date to database" -messageType "success"
						#Write-Message -message "Successfully added $($expTvp.Rows.Count) users and password expiration date to database" -messageType "success"
						
					}
					
					#email users
					#Send-EmailToUsers
					
					#cleanup
					$dbconn = $null
					$expTvp = $null
					$allUsers = $null
				}
				catch
				{
					Write-Message -message "Failed to finish loop. Something went terrbily wrong" -messageType "error"
				}
				
			}
			catch
			{
				Write-Message -message "Unable to create TVP." -messageType "error"
			}
		}
		catch
		{
			#unable to get password expiration date	
			Write-Message -message "We are unable to get all users at this time." -messageType "error"
			exit
		}
	}
	catch
	{
		# TODO UNABLE TO CONNECT TO DATABASE/ GET USERS
		Write-Message -message "Unable to connect to database. Stopping Script" -messageType "error"
		exit
	}
}
catch
{
	#TODO Unable to connect to MSOL Service
	Write-Message -message "Unable to connect to MSOnline. Stopping Script" -messageType "error"
	exit
}