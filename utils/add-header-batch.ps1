# 从文件读取版权协议声明的内容
$headerStr = Get-Content "./add-header-template.txt"

# 指定目录路径
$directoryPath = "../src/"

# 获取目录内所有的 .cs 文件
$csFiles = Get-ChildItem -Path $directoryPath -Filter *.cs -Recurse

# 遍历每个 .cs 文件并在开头添加版权协议声明
foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw

    # 检查文件是否已经包含协议声明，如果没有则添加
    if ($content -notmatch [regex]::Escape($headerStr)) {
        $updatedContent = $headerStr + "`r`n" + $content
        Set-Content -Path $file.FullName -Value $updatedContent
        # Write-Host "已为文件 $($file.FullName) 添加版权协议声明。"
        Write-Host "Added header to file: $($file.FullName)"
    } else {
        # Write-Host "文件 $($file.FullName) 已包含版权协议声明，跳过。"
        Write-Host "File already contains header: $($file.FullName)"
    }
}

Write-Host "Added header to all .cs files in directory: $directoryPath"
