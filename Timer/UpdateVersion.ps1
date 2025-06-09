# UpdateVersion.ps1
$assemblyInfoPath = "$PSScriptRoot\Properties\AssemblyInfo.cs"

if (-Not (Test-Path $assemblyInfoPath)) {
    Write-Host "❌ AssemblyInfo.cs not found at: $assemblyInfoPath"
    exit 1
}

# 设置版本号各段
$major = 1
$now = Get-Date
$yearMonth = $now.ToString("yyMM")        # 第二段：年月，如 2506
$day = [int]$now.ToString("dd")           # 第三段：日，无前导0
$revision = 1                             # 第四段：构建次数（将自动递增）

# 读取现有内容
$content = Get-Content $assemblyInfoPath

# 查找现有版本号是否匹配今天，决定是否自增 revision
foreach ($line in $content) {
    if ($line -match 'AssemblyVersion\("1\.(\d{4})\.(\d{1,2})\.(\d+)"\)') {
        $prevYM = $matches[1]
        $prevDay = $matches[2]
        $prevRev = [int]$matches[3]

        if ($prevYM -eq $yearMonth -and $prevDay -eq $day) {
            $revision = $prevRev + 1
        }
        break
    }
}

# 拼接最终版本号
$newVersion = "$major.$yearMonth.$day.$revision"
Write-Host "✅ New version: $newVersion"

# 替换或插入版本信息
$updatedContent = $content | ForEach-Object {
    if ($_ -match 'AssemblyVersion\(".*"\)') {
        "[assembly: AssemblyVersion(`"$newVersion`")]"
    } elseif ($_ -match 'AssemblyFileVersion\(".*"\)') {
        "[assembly: AssemblyFileVersion(`"$newVersion`")]"
    } else {
        $_
    }
}

if (-Not ($updatedContent -match 'AssemblyVersion')) {
    $updatedContent += "`r`n[assembly: AssemblyVersion(`"$newVersion`")]"
}
if (-Not ($updatedContent -match 'AssemblyFileVersion')) {
    $updatedContent += "`r`n[assembly: AssemblyFileVersion(`"$newVersion`")]"
}

# 写入修改
$updatedContent | Set-Content $assemblyInfoPath -Encoding UTF8
Write-Host "✅ AssemblyInfo.cs updated."
