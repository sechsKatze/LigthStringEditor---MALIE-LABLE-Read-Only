## LightStringEditor - MALIE LABEL Read Only and String table Edit.
※Warning※
Currently, the MALIE LABEL section is read-only and cannot be edited.
This is because increasing its size causes the .rsrc (resource section) to become corrupted when replacing the EXEC using a resource editor, which in turn breaks the .exe file and prevents the game from running.
This is a critical bug that cannot be fixed without rebuilding the entire executable from scratch.

Therefore, if you wish to translate the chapter titles, character names, or choice texts contained within the MALIE LABEL section, you must use API hooking instead.

An editable version that uses azure9’s MalieVM parser functions is included in the Dummy folder.


## Special Thanks
* Marcus André
* azure9
