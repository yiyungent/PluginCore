@REM 注意: 使用时, 请将本文件与 copyright-template.txt 文件放到项目根目录 再执行

for /r %%F in (*.cs) DO (
    move "%%F" tmp.txt
    type copyright-template.txt > "%%F"
    type tmp.txt >> "%%F"
    del tmp.txt
)
