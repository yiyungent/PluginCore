
# 设置要替换的目标字符串和替换字符串
$targetString = "//  License: Apache-2.0"
# $targetString = "//  Project: https://moeci.com/PluginCore"
$replacementString = "//  License: GNU LGPLv3"
# $replacementString = "//  Project: https://yiyungent.github.io/PluginCore"

# 指定目录路径
$directoryPath = "../src/"

# 获取目录内所有的 .cs 文件
$csFiles = Get-ChildItem -Path $directoryPath -Filter *.cs -Recurse

# 遍历每个 .cs 文件并替换目标字符串
foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw
    $updatedContent = $content -replace [regex]::Escape($targetString), $replacementString
    Set-Content -Path $file.FullName -Value $updatedContent
}

Write-Host "Replaced target string ($targetString -> $replacementString) in all .cs files in directory: $directoryPath"


