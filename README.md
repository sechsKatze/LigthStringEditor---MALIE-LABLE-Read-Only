LightStringEditor - MALIE LABEL Read Only and String table Edit.
======
※Warning※

Currently, the MALIE LABEL section is read-only and cannot be edited.
This is because increasing its size causes the .rsrc (resource section) to become corrupted when replacing the EXEC using a resource editor, which in turn breaks the .exe file and prevents the game from running.
This is a critical bug that cannot be fixed without rebuilding the entire executable from scratch.

Therefore, if you wish to translate the chapter titles, character names, or choice texts contained within the MALIE LABEL section, you must use API hooking instead.

An editable version that uses azure9’s MalieVM parser functions is included in the Dummy folder.

Caution
======
1. The differences from the original LightStringEditor are as follows:
You can now press Enter to save the string table and move to the next line, and the tool supports saving and loading not only .dat files but also .bin files.
Don’t worry — the string table is saved exactly the same way as in the original version!

2. As mentioned above, the tool allows you to view the MALIE LABEL section.
Using the filter box, you can display only chapter titles, character names, or choice lines.
(Although the filter feature is still incomplete and may show some MALIE LABEL code lines, this does not affect usability.)

3. This version extends LightStringEditor so it can be used not only for light-developed games but also for Karin Entertainment titles (BL game brand: Karin Château Noir Ω), allowing you to view and translate the MALIE LABEL section as well.


Special Thanks
======
* Marcus André
* azure9
