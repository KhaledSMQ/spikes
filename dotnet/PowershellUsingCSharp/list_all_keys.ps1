# loading all types from an assembly
Add-Type -Path SpikesCo.Platform.Cache.dll
#Add-Type -AssemblyName mscorlib

# instantiating one of the types
$Configuration = New-Object SpikesCo.Platform.Cache.CacheConfiguration

# we can now use the type directly
$Configuration.ConnectionString = "genentitlements-dev.redis.cache.windows.net,ssl=true,password=y9molWPywX5h1uLq+v55/YuRX3U/e0sOhfUDevnDGvQ="
$Configuration.CacheType = "SpikesCo.Platform.Cache.CacheClient, SpikesCo.Platform.Cache"

# let's instantiate the spikeco redis cache wrapper
$CacheClient = New-Object SpikesCo.Platform.Cache.CacheClient -argumentlist $Configuration

# report how many servers we find behind that Azure instance
$servers = $CacheClient.Servers
$serverCount = $servers.Count
write-host "Got $serverCount servers"

# for each of the servers, enumerate the available keys
foreach($server in $servers)
{
    $keys = $server.Keys(0, "*", 1500)
	foreach($key in $keys)
	{
	    #$keyStr = $key
	    write-host "Found key: $key"
	}
}