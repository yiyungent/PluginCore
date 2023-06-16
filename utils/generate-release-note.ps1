
# 从环境变量中获取
# $projectTagName = "PluginCore.IPlugins"
$projectTagName = $env:GitProjectTagName
# $projectDir = "src/PluginCore.IPlugins/*"
$projectPath = $env:GitProjectPath

# 一定要确保在仓库根目录执行

$targetTags = git tag | where { $_ -like "${projectTagName}-v*" }

if ($targetTags.Count -lt 2) {
    # <2
    # $projectTagScope = $targetTags[$targetTags.Count - 1]
    # 注意: 这里尤其需要注意: 当它只有一个时, 就不再是数组, 而是一个值
    # 只有一个,即为最开始, 此时不再限定 tag 范围, 因为之前历史全为 changelog
    $projectTagScope = ""
} else {
    # >=2
    $projectTagScope = $targetTags[$targetTags.Count - 2] + ".." + $targetTags[$targetTags.Count - 1]
}


$projectLogs = git log $projectTagScope --format=%H----DELIMITER----%s -- $projectPath

# TODO: 顺序可能不一致, 导致无法一一对应
# $projectCommitIds = git log $projectTagScope --format=%H $projectPath
# $projectMessages = git log $projectTagScope --format=%s $projectPath
# $projectCommitIds
# $projectMessages

$projectCommits = @()

if ($projectLogs.Count -lt 2) {
    # <2
    # 注意: 这里尤其需要注意: 当它只有一个时, 就不再是数组, 而是一个值
    $itemTemp = $projectLogs -split "----DELIMITER----"
    $commitId = $itemTemp[0]
    $message = $itemTemp[1]

    # $itemObject = New-Object PSObject –Property @{CommitId = $projectCommitIds[$i]; Message = $projectMessages[$i - 1] }
    $itemObject = New-Object -TypeName PSObject
    $itemObject | Add-Member -Name 'CommitId' -MemberType Noteproperty -Value $commitId
    $itemObject | Add-Member -Name 'Message' -MemberType Noteproperty -Value $message
    $projectCommits += $itemObject
} else {
    # >=2
    for ($i = 0; $i -lt $projectLogs.Count; $i++) {
        $itemTemp = $projectLogs[$i] -split "----DELIMITER----"
        $commitId = $itemTemp[0]
        $message = $itemTemp[1]
    
        # $itemObject = New-Object PSObject –Property @{CommitId = $projectCommitIds[$i]; Message = $projectMessages[$i - 1] }
        $itemObject = New-Object -TypeName PSObject
        $itemObject | Add-Member -Name 'CommitId' -MemberType Noteproperty -Value $commitId
        $itemObject | Add-Member -Name 'Message' -MemberType Noteproperty -Value $message
        $projectCommits += $itemObject
    }
}


$projectCommits | Format-Table -Auto

# TODO: 若需要用到 projectLatestTag
$projectLatestTag = $targetTags[$targetTags.Count - 1]

$tempReleaseNoteFilePath = "temp-release-note.md"
"" > $tempReleaseNoteFilePath

foreach ($currentItem in $projectCommits) {
    $iteCommitId = $currentItem.CommitId
    $itemMessage = $currentItem.Message
    "- ${itemMessage} (${iteCommitId})  " >> $tempReleaseNoteFilePath
}