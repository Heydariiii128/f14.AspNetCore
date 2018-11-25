$csprojFiles = @(	
    "f14.AspNetCore\f14.AspNetCore.csproj",    
	"f14.AspNetCore.Data\f14.AspNetCore.Data.csproj",
	"f14.AspNetCore.Identity\f14.AspNetCore.Identity.csproj",
	"f14.AspNetCore.Mail\f14.AspNetCore.Mail.csproj",
	"f14.AspNetCore.Mvc\f14.AspNetCore.Mvc.csproj",
	"f14.AspNetCore.Navigation\f14.AspNetCore.Navigation.csproj"
)

$newVer = Read-Host "Enter new version number"

foreach($f in $csprojFiles){
    $xmlFilePath = "$PSScriptRoot\$f"
    $xml = New-Object XML
    $xml.Load($xmlFilePath)
    $verNode = $xml.DocumentElement.SelectSingleNode("//Version")
    $verNode.InnerText = $newVer

    $utf8enc = New-Object System.Text.UTF8Encoding($true)
    $sw = New-Object System.IO.StreamWriter($xmlFilePath, $false, $utf8enc)
    $xml.Save($sw)
    $sw.Close()
}