$workingDir = "%system.teamcity.build.checkoutDir%"

$outputDirectory = "OctopusNuGetPackages"
$fullOutputDirectory = "$workingDir\$outputDirectory"

echo "Output directory is $fullOutputDirectory"

# clean full output directory
echo "Cleaning $fullOutputDirectory..."
Remove-Item $fullOutputDirectory -Recurse -Force -ErrorAction SilentlyContinue -ErrorVariable cleaningError
if ($cleaningError -ne $null)
{
    if ("$cleaningError" -eq "Cannot find path '$fullOutputDirectory' because it does not exist.")
    {
        echo "No clean up needed."
    }
}

New-Item -ItemType directory -Path $fullOutputDirectory > $null

$nuspecRootLocation = "Main\Src"
$nuspecFilespec = "*.nuspec-octopus"
$fullNuspecRootLocation = "$workingDir\$nuspecRootLocation"

# find all matching nuspecs
echo "Looking for files matching filespec $fullNuspecRootLocation\$nuspecFilespec..."
$files = Get-ChildItem -Path "$fullNuspecRootLocation" -Filter $nuspecFilespec -Recurse

# exit if no nuspecs are found
if ($files -eq $null)
{
    $fileCount = 0
}
else
{
    $fileCount = $files.count
}

if ($fileCount -eq $null -or $fileCount -eq 0)
{
    echo "No nuspec files to process."
    exit
}
else
{
    echo "Found $fileCount nuspec files:"
    foreach($file in $files)
    {
        $fileOriginalName = $file.FullName
        echo "$fileOriginalName"
    }
}

$nugetTool = "%teamcity.tool.NuGet.CommandLine.DEFAULT%\tools\NuGet.exe"
echo "Using nuget executable $nugetTool"

$outputDirectoryOption = "-OutputDirectory $fullOutputDirectory"
$propertiesOption = "-Properties Configuration=Release"

foreach($file in $files)
{
    # nuget only processes .nuspec files so we need to 
    # copy *.nuspec-* files to *.nuspec, invoke nuget pack
    # on them and then delete them
    $fileOriginalName = $file.FullName

    echo "Processing $fileOriginalName..."
	
    $extensionPosition = $fileOriginalName.IndexOf(".nuspec")
    if ($extensionPosition -lt 0)
    {
        echo "File $fileOriginalName does not have a .nuspec extension. Skipping."
        continue
    }
	
    $fileSafeName = $fileOriginalName
	
    $suffixPosition = $extensionPosition + 7
    $copyNeeded = $fileOriginalName.Length -ge $suffixPosition
    if ($copyNeeded)
    {
        $fileSafeName = $fileOriginalName.Substring(0, $suffixPosition)
        echo "Copying $fileOriginalName to temporary file $fileSafeName for processing."
        Copy-Item $fileOriginalName -destination $fileSafeName
    }	
	
    $nugetCommand = "$nugetTool pack $fileSafeName $outputDirectoryOption $propertiesOption"
    echo "NuGet command: $nugetCommand"
	
    Invoke-Expression $nugetCommand

    if ($copyNeeded)
    {
        echo "Deleting temporary file $fileSafeName."
        Remove-Item -path $fileSafeName
    }

    echo "Finished processing $fileOriginalName."
}