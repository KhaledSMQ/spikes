$tenant = "genappsdt.onmicrosoft.com"
$tenantGuid = "a1c27f76-904d-4f83-bad6-0cb4611a8e59"
$graphver = "1.5"
$appID = "f478bb58-7dae-42e2-bce3-537a4cd5ab93"

$userVal = "useradmin@" + $tenant
$pass = "us3r@dm1n"
$Creds = New-Object System.Management.Automation.PsCredential($userVal, (ConvertTo-SecureString $pass -AsPlainText -Force))

Connect-MSOLSERVICE -Credential $Creds
$msSP = Get-MsolServicePrincipal -AppPrincipalId $appID -TenantID $tenantGuid

#ID of the Application
$objectId = $msSP.ObjectId

Add-MsolRoleMember -RoleName "Company Administrator" -RoleMemberType ServicePrincipal -RoleMemberObjectId $objectId