$tenant = "genappsqa.onmicrosoft.com"
$tenantGuid = "d2b2fdfb-8c27-4ee5-a111-db97d5fe3c55"
$graphver = "1.5"
$appID = "529b1f64-2cb5-4e2e-bf02-1cf277608ffa"

$userVal = "useradmin@" + $tenant
$pass = "us3r@dm1n"
$Creds = New-Object System.Management.Automation.PsCredential($userVal, (ConvertTo-SecureString $pass -AsPlainText -Force))

Connect-MSOLSERVICE -Credential $Creds
$msSP = Get-MsolServicePrincipal -AppPrincipalId $appID -TenantID $tenantGuid

#ID of the Application
$objectId = $msSP.ObjectId

Add-MsolRoleMember -RoleName "Company Administrator" -RoleMemberType ServicePrincipal -RoleMemberObjectId $objectId
