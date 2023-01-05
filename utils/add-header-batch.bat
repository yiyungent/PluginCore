@REM 注意: 使用时, 请将本文件与 add-header-template.txt 文件放到项目根目录 再执行
@REM 由于 copyright 关键词 总是被 GitHub 误识别为 开源协议, 因此改名为 add-header
@REM 测试 banquan 好像也被误识别了

for /r %%F in (*.cs) DO (
    move "%%F" tmp.txt
    type add-header-template.txt > "%%F"
    type tmp.txt >> "%%F"
    del tmp.txt
)
