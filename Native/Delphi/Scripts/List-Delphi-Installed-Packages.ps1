Set-StrictMode -Version Latest

function Get-KnownPackages {
    param(
        [string]$basePath = 'hkcu:\Software\Embarcadero\BDS\8.0'
    )
    $path = $basePath + '\Known Packages'
    ## ErrorAction: http://blogs.msdn.com/b/powershell/archive/2006/04/25/583241.aspx
    $key = Get-Item $path -ErrorAction SilentlyContinue
    if ($key) {
        # $key
        $names = $key.GetValueNames()
        $namevalues = $names | 
            ForEach-Object { [PSCustomObject]@{ Name = $_; Value = $key.GetValue($_) } }
        # $namevalues | Format-Table
    }
    else {
        $namevalues = $null
    }
    $namevalues
}

function Filter-BDS-Packages {
    param(
        [string]$basePath = 'hkcu:\Software\Embarcadero\BDS\8.0',
        [boolean]$excludeMatch = $True
    )
    $namevalues = Get-KnownPackages($basePath)
    
    if ($nameValues) {
        $bdsBinAtStart = '^\$\(BDSBIN\)'
        $bdsAtStart = '^\$\(BDS\)'
        if ($excludeMatch) {
            $matches = $namevalues | Where-Object { 
                $_.Name -notMatch $bdsBinAtStart -and `
                $_.Name -notMatch $bdsAtStart 
          }
        }
        else {
            $matches = $namevalues | Where-Object { 
                $_.Name -match $bdsBinAtStart -or `
                $_.Name -match $bdsAtStart 
            }
        }
    }
    else {
        $matches = $null
    }
    $matches
}

function Get-BDS-CompanyName {
    param (
        $bdsVersion = 1
    )
    <#
    CompanyName=Borland (from BDS 1 until BDS 5)
    CompanyName=CodeGear (from BDS 6 until BDS 7)
    CompanyName=Embarcadero (BDS 8 and up)
    #>
    $borland = 'Borland'
    $codeGear = 'CodeGear'
    $embarcadero = 'Embarcadero'
    ## switch 
    ## http://technet.microsoft.com/en-us/library/ff730937.aspx
    ## http://stackoverflow.com/questions/8015637/powershell-switch-statement
    switch ($bdsVersion) {
        1 { return $borland }
        2 { return $borland }
        3 { return $borland }
        4 { return $borland }
        5 { return $borland }
        6 { return $codeGear }
        7 { return $codeGear }
        Default { return $embarcadero }
    }
}

function Get-BDS-CompanyNames {
    $versions = Get-BDS-Versions
    $versions | ForEach-Object { Get-BDS-CompanyName $_ }
}

function Get-BDS-Versions {
    ## array initialization: 
    ## http://stackoverflow.com/questions/226596/powershell-array-initialization
    ## http://get-powershell.com/post/2008/02/07/Powershell-function-New-Array.aspx
    ## http://technet.microsoft.com/en-us/library/ee692797.aspx
    [int[]] $versions = 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
    $versions
}

function Get-BDS-ProductName {
    param (
        $bdsVersion = 1
    )

    switch ($bdsVersion) {
        1       { 'C# Builder' }
        Default { 'Delphi' }
    }
}

function Get-BDS-ProductNames {
  $versions = Get-BDS-Versions
  $versions | ForEach-Object { Get-BDS-ProductName $_ }
}

function Get-BDS-ProductVersion {
    param (
        $bdsVersion = 1
    )

    switch ($bdsVersion) {
        1 { '1' }
        2 { '8' }
        3 { '2005' }
        4 { '2006' }
        5 { '2007' }
        6 { '2009' }
        7 { '2010' }
        8 { 'XE' }
        9 { 'XE2' }
        10 { 'XE3' }
        11 { 'XE4' }
        12 { 'XE5' }
    }
}

function Get-BDS-ProductVersions {
    $versions = Get-BDS-Versions
    $versions | ForEach-Object { Get-BDS-ProductVersion $_ }
}

function Get-BDS-BaseKeyPath {
    param (
        $bdsVersion = 1,
        $rootKey = 'hkcu'
    )
    $company = Get-BDS-CompanyName $bdsVersion
    ## -f format operator: https://devcentral.f5.com/blogs/us/powershell-abcs-f-is-for-format-operator#.UfExLY1plI0
    $pathFormat = '{0}:\Software\{1}\BDS\{2}.0'
    # 'hkcu:\Software\Embarcadero\BDS\8.0' $True
    $path = $pathFormat -f $rootKey, $company, $bdsVersion
    $path
}

function Get-BDS-ProductFullName {
    param (
        $bdsVersion = 1
    )
    <#
    1. Borland C# Builder (contained only C# Builder)
    2. Borland Delphi 8 (added Delphi .net)
    3. Borland Delphi 2005 (added Delphi win32)
    4. Borland Delphi 2006 (added C++ Builder)
    5. Borland Delphi 2007
    6. CodeGear Delphi 2009
    7. CodeGear Delphi 2010
    8. Embarcadero Delphi XE
    9. Embarcadero Delphi XE2 (added Delphi win64)
    10. Embarcadero Delphi XE3 (added C++ Builder win64, OS X x86 and iOS x86/arm through FreePascal)
    11. Embarcadero Delphi XE4 (replaced FreePascal with native Delphi compiler for iOS x86/arm)
    12. Embarcadero Delphi XE5 (added Android ARMv7)
    #>
    $company = Get-BDS-CompanyName $bdsVersion
    $name = Get-BDS-ProductName $bdsVersion   
    $version = Get-BDS-ProductVersion $bdsVersion
    $fullNameFormat = '{0} {1} {2}'
    $fullName = $fullNameFormat -f $company, $name, $version   
    $fullName
}

function Get-BDS-ProductFullNames {
    $fullNames = Get-BDS-FullNames
    $fullNames | ForEach-Object { Get-BDS-ProductFullName $_ }
    $fullNames
}

function Get-BDS-BaseKeyPaths {
    param (
        $rootKey = 'hkcu'
    )
    $bdsVersions = Get-BDS-Versions
    $bdsVersions | ForEach-Object { 
        $path = Get-BDS-BaseKeyPath $_ $rootKey
        $path
    }
}

function Get-BDS-HKCU-BaseKeyPath {
    param (
        $bdsVersion = 1
    )
    $path = Get-BDS-BaseKeyPath $bdsVersion
    $path
}

function Get-BDS-HKCU-BaseKeyPaths {
    $bdsVersions = Get-BDS-Versions
    $bdsVersions | ForEach-Object { 
        $path = Get-BDS-HKCU-BaseKeyPath $_
        $path
    }
}

function Get-BDS-HKLM-BaseKeyPath {
    param (
        $bdsVersion = 1
    )
    $path = Get-BDS-BaseKeyPath $bdsVersion 'hklm'
    $path
}

function Get-BDS-HKLM-BaseKeyPaths {
    $bdsVersions = Get-BDS-Versions
    $bdsVersions | ForEach-Object { 
        $path = Get-BDS-HKLM-BaseKeyPath $_
        $path
    }
}

function Filter-BDS-Packages-For-All-Versions {
    $anyInstalledPackages = $False
    [bool[]]$filters = $True, $False
    $filters | ForEach-Object {
        $filter = $_
        $versions = Get-BDS-Versions
        $versions | ForEach-Object {
            $version = $_
            $basePath = Get-BDS-HKCU-BaseKeyPath $version
            # $state = [PSCustomObject]@{ Version = $version; Filter = $filter; Path = $basePath }
            # $state
            $userPacakgesNameValues = Filter-BDS-Packages $basePath $filter
            if ($userPacakgesNameValues) {
                $productFullName = Get-BDS-ProductFullName $version
                switch ($filter) {
                  $False  { $kind = 'System' }
                  Default { $kind = 'User' }
                }
                $line = '{0} installed packages for "{1}" in {2}' -f $kind, $productFullName, $basePath
                Write-Host $line
                ## Fix The object of type "Microsoft.PowerShell.Commands.Internal.Format.FormatStartData" is not valid or not in the correct sequence. This is likely caused by a user-specified "format-table" command which is conflicting with the default formatting.
                ## http://stackoverflow.com/questions/7517581/powershell-format-table-error
                $userPacakgesNameValues | Format-Table | Out-String
                $anyInstalledPackages = $True
            }
        }
    }
    if (-not $anyInstalledPackages) {
        Write-Host 'No installed Delphi packages found'
    }

}

#Get-BDS-Versions
#Get-BDS-ProductNames
#Get-BDS-CompanyNames
#Get-BDS-BaseKeyPaths
#Get-BDS-HKCU-BaseKeyPaths
#Get-BDS-HKLM-BaseKeyPaths
#Get-BDS-ProductVersions
#Get-BDS-ProductFullNames
Filter-BDS-Packages-For-All-Versions

<#
Write-Host 'Delphi XE user installed packages:'
Filter-BDS-Packages 'hkcu:\Software\Embarcadero\BDS\8.0' $True

Write-Host 'Delphi XE2 user installed packages:'
Filter-BDS-Packages 'hkcu:\Software\Embarcadero\BDS\9.0' $True

Write-Host 'Delphi XE Embarcadero installed packages:'
Filter-BDS-Packages 'hkcu:\Software\Embarcadero\BDS\8.0' $False

Write-Host 'Delphi XE2 Embarcadero installed packages:'
Filter-BDS-Packages 'hkcu:\Software\Embarcadero\BDS\9.0' $False
#>
