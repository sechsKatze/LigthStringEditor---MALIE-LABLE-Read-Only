## LightStringEditor - MALIE LABEL Read Only and String table Edit.
※Warning※
Currently, the MALIE LABEL section is read-only and cannot be edited.
This is because increasing its size causes the .rsrc (resource section) to become corrupted when replacing the EXEC using a resource editor, which in turn breaks the .exe file and prevents the game from running.
This is a critical bug that cannot be fixed without rebuilding the entire executable from scratch.

Therefore, if you wish to translate the chapter titles, character names, or choice texts contained within the MALIE LABEL section, you must use API hooking instead.

An editable version that uses azure9’s MalieVM parser functions is included in the Dummy folder.


* 1. Supports both the String Table and the MALIE LABEL section.
Unlike ordinary versions that only handle dialogue/narration strings, this one can also translate the MALIE LABEL area. Karin stores character names and branching-choice captions inside MALIE LABEL blocks within the EXEC file, so these can now be edited as well.

* 2. Includes MalieVM parser code analyzed by azure9.
Because MALIE LABEL sections belong to executable code regions of the running game, changing text there can alter the underlying bytecode. That may corrupt bytecode or offsets and cause in-game instability.
Therefore, this version uses an opcode parser to modify bytecode safely whenever translated strings change.

## Special Thanks
* Marcus André
* azure9
