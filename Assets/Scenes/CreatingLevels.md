# Creating Levels

## Step 1: Create Scene
Self explanatory. I make a folder called `Levels` to distinguish "level scenes" from main menu and other UI scenes

## Step 2: Create LevelDescriptor
Right click in the asset browser > Create > Level > LevelDescriptor
This asset is a description of your level and contains info about:
- Scene path (has to be the same as the scene asset filename)
- Level name (the displayed name of the level)
- Thumbnail
- Total number of coins in the level (this is temporary and will be removed in the next commit but you can set it for fun)

## Step 3: Add LevelDescriptor to level list
Open Assets/MainMenu scene and select the `GameManager`. Add a new entry in `Levels` and drag and drop your LevelDescriptor into the slot

## Step 4: Add UI elements to level
Create a UI Canvas in your scene

-----------------------------------
### __Optional__
you probably don't have to do it but i have my canvas rendering mode set to `Screen Space - Camera` and sorting layer to `UI`. Setting it to `Screen Space - Overlay` should work exactly the same

-----------------------------------

Drag and drop `Assets/Prefabs/UI/PauseMenu` and `Assets/Prefabs/UI/CoinCounter` onto the Canvas object

## Step 5: Add Checkpoints
Add `Assets/Prefabs/Checkpoints/CheckpointManager` to your scene. Drag and drop the Player GameObject into the `Player` field in the inspector.

Add `Assets/Prefabs/Checkpoints/Checkpoint` objects to the scene under the CheckpointManager object. Position them in your scene. Then add them to the `Checkpoints` List in the `CheckpointManager` __IN ORDER__. A checkpoint will only be triggered if it has a higher index in the List then the current active checkpoint (we can change this behaviour easily, i just thought it would make sense)

## Important information
Launch the game from the MainMenu scene. I know it's annoying. You can launch the game from your level but some things might crash. It shouldn't affect your local scripts in the level though, only the stuff that uses GameManager.Instance

I will be adding more stuff here as we go. Most notably setting up the `CoinManager` it's done
