## LightStringEditor - Karin EXEC file ver.
It is an extended, general-purpose translation tool based on the EXEC format (formally “exec.dat”) used in Karin Entertainment(Karin Château Noir Ω)'S developed from Marcus André’s LightStringEditor.
The main differences from the original are as follows:

* 1. Supports both the String Table and the MALIE LABEL section.
Unlike ordinary versions that only handle dialogue/narration strings, this one can also translate the MALIE LABEL area. Karin stores character names and branching-choice captions inside MALIE LABEL blocks within the EXEC file, so these can now be edited as well.

* 2. Includes MalieVM parser code analyzed by azure9.
Because MALIE LABEL sections belong to executable code regions of the running game, changing text there can alter the underlying bytecode. That may corrupt bytecode or offsets and cause in-game instability.
Therefore, this version uses an opcode parser to modify bytecode safely whenever translated strings change.

## Special Thanks
* Marcus André
* azure9
