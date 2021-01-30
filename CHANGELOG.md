# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [0.0.15]
### Changed
- Constructors of Props and Widgets are now public

### Fixed
- Vertical/Horizontal stretch now respect spaces of sibling and stretch as long as siblings have space

## [0.0.14]

### Added
- IWidget interface that represents a Widget with its props, children and rendering data 
- Widget, a simple concrete implementation of IWidget that other widgets can inherit from
- Prop collection with a dictionary
- IPropHolder to separate prop handling
- UserInteractionHandler to handle device input on widgets and calculate the interactions per frame
- InteractionUpdater used by the handler to get the next interactions for the widget and the interaction logic to run
- IProp base interface
- IApplicableProp for props that only have to ApplyOn widgets
- ICommandProp for props that hold logic to run later, like the interaction props (onEnter etc)
- OnUpdate command prop which is executed in the update method
- UpdateTree method in TreeVisitor that takes the OnUpdate props for every widget and executes them

### Changed
- Property renamed to Prop (it was confusing with C# property)
- Merged WidgetTree and Widget into IWidget
- Updated code and tests to integrate this change
- TreeVisitor in Utilities now is the only one that takes care of applying functions to IWidget trees.
- Abstracted Prop into interfaces with base interface IProp for other classes to use
- Applied event update for every ApplicableProp, now Flex works again because it uses the Row/Col applied event
- Draggable was reintegrated with the new Props collections for the OnPress/OnEnter/OnExit commands
- WTreeManager get the transition commands from the InteractionHandler and is responsible for executing them
- Text Widget now updates the space when the FontSize or the Font changes (in the setters), instead of the props
- JustifyAround with only one child puts it at the center instead of doing nothing. It makes more sense this way since it's supposed to put empty space around widgets
- IApplicableProps that need a Row or Column prop to work now throw IncompatibleWidgetException when applied on widgets without a Row nor Col prop
- Methods of the TreeVisitor are now public
- WTreeManager doesn't add an empty root anymore

### Removed
- Tree collection
- WidgetTree
- WidgetTreeVisitor
- Prop class

## [0.0.13]
### Added
- Documentation for public classes and methods
- Install Moq lib
- Many tests for the WidgetInteractionUpdater

### Changed
- Stretch property divided in HorizontalStretch and VerticalStretch and they work directly on the widgets space independently if row or column
- Changed FontManager name to FontStore
- Move FontStore to Utilities namespace
- Make Widgets.Update public
- Decouple MouseDevice and WidgetInteractionUpdater by moving mouse tracking from the updater into the mouse device class. The updater only deals with IDevice now.
- Update tests converting Fake classes to Moq 

## [0.0.12]

### Added
- A csproj file in the src folder to build nuget package without the tests and other stuff
- A Program class with a main method in the tests folder so that building and testing the project works (for some reason running the tests was giving missing main method error)
- ShaderDB initialization in HUD (giving it a reference to the graphicsDevice) so it can use it to load shaders
- WForestFactory class as an entry point for the lib. Used to create WTreeManager (previous hud class)

### Changed
- Turned project into a class lib project so it can be packaged into a nuget package and installed in monogame projects
- Widgets Factory for the ImageButton now takes a texture2d directly and doesn't use the assetloader to load a texture.
- ShaderDB loads the shaders from scratch, without the AssetLoader
- Widgets factory renamed to WidgetFactory
- Properties factory renamed to PropertyFactory
- Widgets constructors made internal so they can only be instanced via WidgetFactory
- Properties constructors changed to internal
- WidgetTree AddProperty renamed to WithProperty which returns its WidgetTree instance so it can be concatenated
- HUD changed name to WTreeManager
- Moved factories folder outside UI
- All properties have internal constructor, they can be created only through the factory
- Margin struct renamed to MarginValues
- Internal DrawAndPostDraw method in Widget to handle drawing and modifiers separately from Draw virtual method
- MouseDevice was made into a singleton since only one instance is needed for draggables

### Removed
- Program and Game1 classes 
- AssetLoader and AssetLoaderNotInitialized exception

## [0.0.11]
### Added 
- Resize method to HUD to handle resizing of screen

### Changed
- Constructor of HUD now receives space coordinates and size for the root widget
- First properties application in HUD constructor is through the Resize method

### Fixed
- In TreeVisitor.GetLowestNodeThatHolds was reversing in place the order of children of a widget node
## [0.0.10]
### Added
- Installed NuGet package FontStashSharp for text rendering
- Font class to deal with DynamicSpriteFont
- FontManager to load and use fonts
- Text Widget: simple widget to display text. It holds the text, the font and the font size.
  On creation it calculates the size it takes on screen.
- FontSize property to change font size of a text widget. It also recalculates the size of the text widget.
- FontFamily property to change font in a text widget. It also recalculates the size of the text widget.
- Tests on the new properties

### Changed
- SpriteButtonTests now is WidgetsTests to hold tests on multiple widgets, since they are few
- Border property priority was set to 10 to have it as last property to be applied

## [0.0.9]
### Added
- Draggable property to drag widgets within the parent space
- FixX and FixY properties, to use with draggable, to limit dragging on the x and/or y axis
- IDevice interface to abstract over devices used and a MouseDevice subclass to get mouse pos, used with Draggable

### Changed
- Moved the interaction with widget system into a state machine used by the widgets themselves
- Changed the onEnter/onExit/onPress/onRelease actions to a list of action

## [0.0.8]
### Added
- Basic Block widget to have colored rectangle textures
- Color property to change color of any widget with texture

### Changed
- Some optimization for the Rounded shader
- Primitives static method for creating textures


## [0.0.7]
### Added
- Border property to add a simple border around the widget (mainly used for debugging)
- First prototype of the Rounded property, a shader that rounds the corners of a texture. Used to round widgets.
- Readme.md
- This changelog
- Contributing.md

## [0.0.6]
### Added
- Margin property to add margin.
- Row and column properties to tranform widgets (mainly for Container) as a row or column
- Justify properties to work with row col (center, end, spacebetween, spacearound)
- Item properties to work with row col (center, base)

### Changed
- Widget base class now has a Margin field to get the margin values
- Center property becomes JustifyCenter

## [0.0.5]
### Added
- Action properties: onEnter, onExit, onPress, onRelease. 
To add custom logic for these events through property as well as overriding the methods from the widget base class.

## [0.0.4]
### Added
- Base layout functionality
- GridHelper and utils to for the layout systems to use
- Center property to center any widget in the middle of the parent widget

## [0.0.3]
### Added
- Container widget: an empty widget to use as a parent container
- ImageButton widget that holds a texture2D for normal drawing and hover/pressed textures

### Changed
- Widget base class now holds interaction with mouse methods

## [0.0.2]
### Added
- Classes to apply functions to the tree of widgets 
- Base TreeVisitor class
- WidgetTree visitor to apply properties to the entire tree and draw it

## [0.0.1]
### Added
- Created project with Monogame cross-platform OpenGL template
- Widget base class that functions as a simple data class with a draw method
- Generic tree collection
- WidgetTree class as a specialized tree for widget, it holds properties of a widget
- Property abstract class with an ApplyOn method to modify widgets
