for /r %%F in (*.cs) DO (
    move "%%F" tmp.txt
    type copyright.txt > "%%F"
    type tmp.txt >> "%%F"
    del tmp.txt
)